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

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Métodos de consulta (Query/Read)
    public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> GetByUserNameAsync(string userName)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _context.Usuarios.AnyAsync(u => u.UserName == userName || u.Email == userName);
    }

    // Métodos de comando (Command/Write)
    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario> RemoveAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    // Métodos específicos de Refresh Token
    public async Task UpdateRefreshTokenAsync(Usuario usuario, string refreshToken, DateTime expireTime)
    {
        if (usuario is not null)
        {
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = expireTime;

            await _context.SaveChangesAsync();
        }
    }

    public async Task RevokeRefreshTokenAsync(int usuarioID)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioID);

        if (usuario is not null)
        {
            usuario.RefreshToken = null;
            usuario.RefreshTokenExpiryTime = null;

            await _context.SaveChangesAsync();
        }
    }

}
