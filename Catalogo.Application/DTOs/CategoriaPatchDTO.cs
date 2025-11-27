using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.DTOs;

public class CategoriaPatchDTO
{
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; set; }

    [MinLength(5)]
    [MaxLength(250)]
    public string Imagemurl { get; set; }
}
