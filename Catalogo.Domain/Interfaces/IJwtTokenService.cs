using Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Security;

public interface IJwtTokenService
{
    string GenerateAccessToken(Usuario usuario);
    string GenerateRefreshToken();
    DateTime GetRefreshTokenExpiryTime();
}
