using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetProdutosAsync()
    {
        var produtos = await _context.Produtos.ToListAsync();

        return produtos;
    }

    public async Task<Produto> GetByIdAsync(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        return produto;
    }

    public async Task<Produto> CreateAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<Produto> UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<Produto> RemoveAsync(Produto produto)
    {
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return produto;
    }
}
