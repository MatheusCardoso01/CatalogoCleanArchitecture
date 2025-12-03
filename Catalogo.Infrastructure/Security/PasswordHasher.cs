using Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    { 
        using var sha256 = SHA256.Create();
        var hashedbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(hashedbytes);
    }

    public bool VerifyPassword(string storedHashedPassword, string providedPassword)
    {
        var hashedProvidedPassword = HashPassword(providedPassword);
        return hashedProvidedPassword == storedHashedPassword;
    }
}
