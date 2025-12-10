using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> PostLogin([FromBody] UsuarioRegistroDTO loginDTO)
    {
        var token = await _authService.Authenticate(loginDTO.UserName, loginDTO.Password);

        if (token is null)
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        return Ok(token);
    }

    [HttpPost("renew/{refreshToken}")]
    public async Task<ActionResult<TokenDTO>> PostRefreshToken([FromRoute] string refreshToken)
    {
        var newToken = await _authService.RenewAccessToken(refreshToken);

        if (newToken is null)
        {
            return Unauthorized("Refresh token inválido.");
        }

        return Ok(newToken);
    }

    [HttpPost("revoke/{id}")]
    public async Task<ActionResult<string>> PostRevokeToken([FromRoute] int id)
    {
        bool result = await _authService.RevokeToken(id);

        if (!result)
        {
            return BadRequest("Não foi possível revogar o token.");
        }

        return Ok("Token revogado com sucesso.");
    }
}
