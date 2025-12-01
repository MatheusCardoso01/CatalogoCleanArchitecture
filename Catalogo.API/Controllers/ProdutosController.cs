using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtosService)
    {
        _produtoService = produtosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
    {
        var produtosDTO = await _produtoService.GetProdutos();

        if (produtosDTO is null || !produtosDTO.Any())
            return NotFound("Produtos não encontrados");

        return Ok(produtosDTO);
    }

    [HttpGet("{id:int}", Name = "ObterProdutos")]
    public async Task<ActionResult<ProdutoDTO>> GetById([FromRoute] int id)
    {
        var produtoDTO = await _produtoService.GetById(id);

        if (produtoDTO is null)
            return NotFound("Produto não encontrado");

        return Ok(produtoDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post([FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest("Dados inválidos");

        produtoDTO = await _produtoService.Add(produtoDTO);

        return new CreatedAtRouteResult(
            routeName: "ObterProdutos",
            routeValues: new { id = produtoDTO.Id },
            value: produtoDTO
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put([FromRoute] int id, [FromBody] ProdutoDTO produtoDTO)
    {
        if (id <= 0 || produtoDTO is null || id != produtoDTO.Id)
            return BadRequest("Dados inválidos");

        produtoDTO = await _produtoService.Update(produtoDTO);

        if (produtoDTO is null)
            return NotFound("Produto não encontrado");

        return Ok(produtoDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest("Dados inválidos");

        var produtoDTO = await _produtoService.Remove(id);

        if (produtoDTO is null)
            return NotFound("Produto não encontrado");

        return Ok(produtoDTO);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ProdutoPatchDTO> jsonPatchDTO)
    {
        if (jsonPatchDTO is null || id <= 0)
            return BadRequest("Dados inválidos");

        var produtoDTO = await _produtoService.GetById(id);

        if (produtoDTO is null)
            return NotFound("Produto não encontrado");

        var produtoPatchDTO = ConverteJsonPatchParaPatchDto(jsonPatchDTO, produtoDTO);

        produtoDTO = await _produtoService.Patch(id, produtoPatchDTO);

        return Ok(produtoDTO);
    }

    // métodos auxiliares

    private ProdutoPatchDTO ConverteJsonPatchParaPatchDto(JsonPatchDocument<ProdutoPatchDTO> jsonPatchDTO, ProdutoDTO produtoDTO)
    {
        var patchDTO = new ProdutoPatchDTO
        {
            Nome = produtoDTO.Nome,
            Descricao = produtoDTO.Descricao,
            Preco = produtoDTO.Preco,
            Imagemurl = produtoDTO.Imagemurl,
            Estoque = produtoDTO.Estoque
        };

        foreach (var operation in jsonPatchDTO.Operations)
        {
            var propertyName = operation.path.TrimStart('/').ToLower();

            if (operation.op.ToLower() == "replace" || operation.op.ToLower() == "add")
            {
                switch (propertyName)
                {
                    case "nome":
                        patchDTO.Nome = operation.value?.ToString();
                        break;
                    case "descricao":
                        patchDTO.Descricao = operation.value?.ToString();
                        break;
                    case "preco":
                        if (decimal.TryParse(operation.value?.ToString(), out var preco))
                            patchDTO.Preco = preco;
                        break;
                    case "imagemurl":
                        patchDTO.Imagemurl = operation.value?.ToString();
                        break;
                    case "estoque":
                        if (int.TryParse(operation.value?.ToString(), out var estoque))
                            patchDTO.Estoque = estoque;
                        break;
                }
            }

            if (operation.op.ToLower() == "remove")
            {
                switch (propertyName)
                {
                    case "nome":
                        patchDTO.Nome = null;
                        break;
                    case "descricao":
                        patchDTO.Descricao = null;
                        break;
                    case "preco":
                            patchDTO.Preco = default;
                        break;
                    case "imagemurl":
                        patchDTO.Imagemurl = null;
                        break;
                    case "estoque":
                            patchDTO.Estoque = default;
                        break;
                }
            }
        }

        return patchDTO;
    }
}
