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

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
    {
        var categorias = await _context.Categorias.ToListAsync();

        return categorias;
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

        return categoria;
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<Categoria> RemoveAsync(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }
}
