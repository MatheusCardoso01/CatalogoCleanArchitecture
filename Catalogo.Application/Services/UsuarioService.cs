using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Services;

public class UsuarioService : IUsuarioService
{
    public Task<UsuarioDTO> Add(UsuarioDTO usuarioDto)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UsuarioDTO>> GetUsuarios(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDTO> Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDTO> Update(UsuarioDTO usuarioDto)
    {
        throw new NotImplementedException();
    }
}
