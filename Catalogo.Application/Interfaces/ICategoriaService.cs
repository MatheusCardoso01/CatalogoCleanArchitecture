using Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<CategoriaDTO> GetById(int id);
    Task Add(CategoriaDTO categoriaDto);
    Task<CategoriaDTO> Update(CategoriaDTO categoriaDto);
    Task<CategoriaDTO> Remove(int id);
    Task<CategoriaDTO?> Patch(int id, CategoriaPatchDTO categoriaPatchDTO);
}
