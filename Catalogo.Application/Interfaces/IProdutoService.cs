using Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDTO>> GetProdutos();
    Task<ProdutoDTO> GetById(int id);
    Task Add(ProdutoDTO produtoDto);
    Task<ProdutoDTO> Update(ProdutoDTO produtoDto);
    Task<ProdutoDTO> Remove(int id);
    Task<ProdutoDTO?> Patch(int id, ProdutoPatchDTO produtoPatchDTO);
}
