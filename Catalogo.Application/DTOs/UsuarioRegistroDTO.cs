using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.DTOs;

public class UsuarioRegistroDTO
{
    [Required(ErrorMessage = "UserName é obrigatório")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Senha deve ter entre 3 e 100 caracteres")]
    public string Password { get; set; }
}
