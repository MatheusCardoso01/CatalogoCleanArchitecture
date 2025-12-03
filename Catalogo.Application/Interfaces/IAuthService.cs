using Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDTO> Authenticate(string username, string password);
    Task<TokenDTO> RenewAccessToken(string refreshToken);
    Task<bool> RevokeToken(int usuarioID);
}
