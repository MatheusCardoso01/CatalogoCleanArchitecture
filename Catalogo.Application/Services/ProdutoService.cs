using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    public IProdutoRepository _produtoRepository;
    public readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    { 
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var produtoEntity = await _produtoRepository.GetProdutosAsync();

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtoEntity);
    }

    public async Task<ProdutoDTO> GetById(int id)
    {
        var produtoEntity = await _produtoRepository.GetByIdAsync(id);

        return _mapper.Map<ProdutoDTO>(produtoEntity);
    }

    public async Task Add(ProdutoDTO produtoDto)
    {
        var produtoEntity = _mapper.Map<Produto>(produtoDto);

        await _produtoRepository.CreateAsync(produtoEntity);
    }

    public async Task<ProdutoDTO> Update(ProdutoDTO produtoDTO)
    {
        var produtoEntity = await _produtoRepository.GetByIdAsync(produtoDTO.Id);

        if (produtoEntity is null)
            return null;

        produtoEntity.Update(produtoDTO.Nome, produtoDTO.Descricao, produtoDTO.Preco, produtoDTO.Imagemurl, 
            produtoDTO.Estoque, produtoDTO.DataCadastro, produtoDTO.CategoriaId);

        await _produtoRepository.UpdateAsync(produtoEntity);

        return produtoDTO;
    }

    public async Task<ProdutoDTO> Remove(int id)
    {
        var produtoEntity = await _produtoRepository.GetByIdAsync(id);

        if (produtoEntity is null)
            return null;

        await _produtoRepository.RemoveAsync(produtoEntity);

        return _mapper.Map<ProdutoDTO>(produtoEntity);
    }

    public async Task<ProdutoDTO> Patch(int id, ProdutoPatchDTO produtoPatchDTO)
    {
        var produtoEntity = await _produtoRepository.GetByIdAsync(id);

        produtoEntity = AplicarAlteracoes(produtoEntity, produtoPatchDTO);

        await _produtoRepository.UpdateAsync(produtoEntity);

        return _mapper.Map<ProdutoDTO>(produtoEntity);
    }

    // métodos auxiliares

    private Produto AplicarAlteracoes(Produto produtoEntity, ProdutoPatchDTO produtoPatchDTO)
    {
        string novoNome = produtoPatchDTO.Nome ?? produtoEntity.Nome;
        string novaDescricao = produtoPatchDTO.Descricao ?? produtoEntity.Descricao;
        string novaImagemUrl = produtoPatchDTO.Imagemurl ?? produtoEntity.Imagemurl;
        decimal novoPreco = produtoPatchDTO.Preco != null ? produtoPatchDTO.Preco : produtoEntity.Preco;
        int novoEstoque = produtoPatchDTO.Estoque != null ? produtoPatchDTO.Estoque : produtoEntity.Estoque;

        produtoEntity.Update(novoNome, novaDescricao, novoPreco, novaImagemUrl, novoEstoque, produtoEntity.DataCadastro, produtoEntity.CategoriaId);

        return produtoEntity;
    }
}
