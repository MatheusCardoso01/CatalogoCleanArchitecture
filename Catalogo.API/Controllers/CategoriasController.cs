using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
    { 
        var categoriasDTO = await _categoriaService.GetCategorias();

        if (categoriasDTO == null || !categoriasDTO.Any())
        {
            return NotFound("Nenhuma categoria encontrada.");
        }

        return Ok(categoriasDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategorias")]
    public async Task<ActionResult<CategoriaDTO>> GetById(int id)
    { 
        var categoriaDTO = await _categoriaService.GetById(id);

        if (categoriaDTO == null)
        {
            return NotFound("Categoria não encontrada.");
        }

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest("Dados inválidos.");

        await _categoriaService.Add(categoriaDTO);

        return new CreatedAtRouteResult(
            routeName: "ObterCategorias",
            routeValues: new { id = categoriaDTO.Id },
            value: categoriaDTO
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put([FromRoute] int id, [FromBody] CategoriaDTO categoriaDTO)
    {
        if (id <= 0 || categoriaDTO is null || id != categoriaDTO.Id)
        { 
            return BadRequest("Dados inválidos.");
        }

        categoriaDTO = await _categoriaService.Update(categoriaDTO);

        if (categoriaDTO is null)
            return NotFound("Categoria não encontrada.");

        return Ok(categoriaDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Delete([FromRoute] int id)
    { 
        var categoriaDTO = await _categoriaService.Remove(id);

        if (categoriaDTO is null)
            return NotFound("Categoria não encontrada.");

        return Ok(categoriaDTO);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<CategoriaPatchDTO> jsonPatchDTO)
    { 
        if (jsonPatchDTO is null || id <= 0)
            return BadRequest("Dados inválidos.");

        var categoriaDTO = await _categoriaService.GetById(id);

        if (categoriaDTO is null)
            return NotFound("Categoria não encontrada.");

        var categoriaPatchDTO = ConverteJsonPatchParaPatchDto(jsonPatchDTO, categoriaDTO);

        var categoriaDTOAtualizada = await _categoriaService.Patch(id, categoriaPatchDTO);

        return Ok(categoriaDTOAtualizada);

    }

    // métodos auxiliares

    private CategoriaPatchDTO ConverteJsonPatchParaPatchDto(JsonPatchDocument<CategoriaPatchDTO> jsonPatchDTO, CategoriaDTO categoriaDTO)
    {
        var patchDTO = new CategoriaPatchDTO
        {
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl
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
                    case "imagemurl":
                        patchDTO.ImagemUrl = operation.value?.ToString();
                        break;
                }
            }
            else if (operation.op.ToLower() == "remove")
            {
                switch (propertyName)
                {
                    case "nome":
                        patchDTO.Nome = null;
                        break;
                    case "imagemurl":
                        patchDTO.ImagemUrl = null;
                        break;
                }
            }
        }

        return patchDTO;
    }

}
