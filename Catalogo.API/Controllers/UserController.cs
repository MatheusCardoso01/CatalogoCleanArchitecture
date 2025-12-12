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

    [Authorize(Roles = "Admin")]
    [HttpGet("usuario/{id}")]
    public async Task<ActionResult<UsuarioDTO>> GetById([FromRoute] int id)
    {
        var usuario = await _usuarioService.GetById(id);

        if (usuario is null)
        {
            return NotFound($"Usuário com id {id} não encontrado.");
        }

        return Ok(usuario);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioDTO>> Put([FromRoute] int id, [FromBody] UsuarioDTO usuarioDTO)
    {
        if (id != usuarioDTO.Id || id <= 0)
        { 
            return BadRequest("Dados inválidos");
        }
        
        var usuarioAtualizado = await _usuarioService.Update(usuarioDTO);

        if (usuarioAtualizado is null)
        {
            return NotFound($"Usuário com id {id} não encontrado para atualização.");
        }

        return Ok(usuarioAtualizado);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UsuarioDTO>> Delete([FromRoute] int id)
    { 
        if (id <= 0)
            return BadRequest("Dados inválidos");

        var usuarioDeletado = await _usuarioService.Remove(id);

        if (usuarioDeletado is null)
        {
            return NotFound($"Usuário com id {id} não encontrado para exclusão.");
        }

        return Ok(usuarioDeletado);
    }

}
