using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UserController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registro")]
    public async Task<ActionResult<UsuarioDTO>> PostNewUser([FromBody] UsuarioRegistroDTO registroDTO)
    {
        UsuarioDTO newUser = await _usuarioService.Add(registroDTO);

        return CreatedAtAction(nameof(PostNewUser), new { id = newUser.Id }, newUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("usuarios")]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
    { 
        var usuarios = await _usuarioService.GetUsuarios();

        if (usuarios is null)
        {
            return NotFound("Nenhum usuário encontrado.");
        }

        return Ok(usuarios);
    }

}
