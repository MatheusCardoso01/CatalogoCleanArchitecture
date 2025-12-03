using Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetUsuariosAsync();
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuarioAtualizado, Usuario usuarioExistente);
    Task<Usuario> RemoveAsync(Usuario usuario);
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByUserNameAsync(string userName);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> UserNameExistsAsync(string userName);
    Task<Usuario> GetByRefreshTokenAsync(string refreshToken);
    Task UpdateRefreshTokenAsync(Usuario usuario, string refreshToken, DateTime expireTime);
    Task RevokeRefreshTokenAsync(int usuarioID);
}
