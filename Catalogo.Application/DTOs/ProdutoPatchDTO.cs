using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.DTOs;

public class ProdutoPatchDTO
{
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; set; }

    [MinLength(5)]
    [MaxLength(200)]
    public string Descricao { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    public decimal Preco { get; set; }

    [MaxLength(250)]
    public string Imagemurl { get; set; }

    [Range(1, 9999)]
    public int Estoque { get; set; }
}
