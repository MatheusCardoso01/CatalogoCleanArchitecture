using AutoMapper;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Services;

public class AuthService : IAuthService
{
    private IJwtTokenService _jwtTokenService;
    private IPasswordHasher _passwordHasher;
    private IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public AuthService(IUsuarioRepository usuarioRepository, IMapper mapper, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var usuario = await _usuarioRepository.GetByUserNameAsync(username);

        if (usuario is null || !usuario.Ativo)
            return null;

        bool correctPassword = _passwordHasher.VerifyPassword(usuario.PasswordHash, password);

        if (!correctPassword)
            return null;

        string token = _jwtTokenService.GenerateAccessToken(usuario);
        string refreshToken = _jwtTokenService.GenerateRefreshToken();
        DateTime refreshTokenExpiryTime = _jwtTokenService.GetRefreshTokenExpiryTime();

        await _usuarioRepository.UpdateRefreshTokenAsync(usuario, refreshToken, refreshTokenExpiryTime);

        return token;
    }

    public async Task<string> RenewAccessToken(string refreshToken)
    {
        var usuario = await _usuarioRepository.GetByRefreshTokenAsync(refreshToken);

        if (usuario is null || !usuario.Ativo || usuario.RefreshTokenExpiryTime < DateTime.UtcNow)
            return null;

        var token = _jwtTokenService.GenerateAccessToken(usuario);

        return token;
    }

    public async Task<bool> RevokeToken(int usuarioID)
    {
        throw new NotImplementedException();
    }
}
