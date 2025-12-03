using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Domain.Entities;

public class Usuario : Entity
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string PasswordHash { get; set; } = "";

    public string? Role { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public bool Ativo { get; set; } = true;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

}
