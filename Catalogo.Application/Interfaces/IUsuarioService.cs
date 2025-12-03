using Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetUsuarios(int id);
    Task<UsuarioDTO> GetById(int id);
    Task<UsuarioDTO> Add(UsuarioDTO usuarioDto);
    Task<UsuarioDTO> Update(UsuarioDTO usuarioDto);
    Task<UsuarioDTO> Remove(int id);
}
