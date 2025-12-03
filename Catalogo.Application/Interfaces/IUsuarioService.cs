using Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetUsuarios();
    Task<UsuarioDTO> GetById(int id);
    Task<UsuarioDTO> Add(UsuarioRegistroDTO usuarioRegistroDTO);
    Task<UsuarioDTO> Update(UsuarioDTO usuarioDTO);
    Task<UsuarioDTO> Remove(int id);
}
