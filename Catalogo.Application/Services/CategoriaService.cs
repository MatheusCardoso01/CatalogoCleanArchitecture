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

public class CategoriaService : ICategoriaService
{
    private ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        var categoriaEntity = await _categoriaRepository.GetCategoriasAsync();

        return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriaEntity);
    }

    public async Task<CategoriaDTO> GetById(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);

        return _mapper.Map<CategoriaDTO>(categoriaEntity);
    }

    public async Task<CategoriaDTO> Add(CategoriaDTO categoriaDto)
    {
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDto);

        categoriaEntity = await _categoriaRepository.CreateAsync(categoriaEntity);

        return _mapper.Map<CategoriaDTO>(categoriaEntity);
    }

    public async Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(categoriaDTO.Id); 

        if (categoriaEntity is null)
            return null;

        categoriaEntity.Update(categoriaDTO.Nome, categoriaDTO.ImagemUrl);

        await _categoriaRepository.UpdateAsync(categoriaEntity);

        return categoriaDTO;
    }

    public async Task<CategoriaDTO> Remove(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);

        if (categoriaEntity is null)
            return null;

        await _categoriaRepository.RemoveAsync(categoriaEntity);

        return _mapper.Map<CategoriaDTO>(categoriaEntity);
    }

    public async Task<CategoriaDTO> Patch(int id, CategoriaPatchDTO categoriaPatchDTO)
    { 
        var categoria = await _categoriaRepository.GetByIdAsync(id);

        categoria = AplicarAlteracoes(categoria, categoriaPatchDTO);

        await _categoriaRepository.UpdateAsync(categoria);

        return _mapper.Map<CategoriaDTO>(categoria);
    }

    // métodos auxiliares

    public Categoria AplicarAlteracoes(Categoria categoria, CategoriaPatchDTO categoriaPatchDTO)
    {
        string novoNome;
        string novaImagemurl;

        if (categoriaPatchDTO.Nome != null)
        {
            novoNome = categoriaPatchDTO.Nome;
        }
        else
        {
            novoNome = categoria.Nome;
        }
        if (categoriaPatchDTO.ImagemUrl != null)
        {
            novaImagemurl = categoriaPatchDTO.ImagemUrl;
        }
        else
        {
            novaImagemurl = categoria.ImagemUrl;
        }

        categoria.Update(novoNome, novaImagemurl);

        return categoria;
    }
}
