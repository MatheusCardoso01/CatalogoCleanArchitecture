using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Interfaces;

public interface IAuthService
{
    Task<string> Authenticate(string username, string password);
    Task<string> RenewAccessToken(string refreshToken);
    Task<bool> RevokeToken(int usuarioID);
}
