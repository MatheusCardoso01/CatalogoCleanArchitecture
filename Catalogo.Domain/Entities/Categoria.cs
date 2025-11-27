using Catalogo.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Domain.Entities;

public sealed class Categoria : Entity
{
    // atributos
    public string Nome { get; private set; }
    public string Imagemurl { get; private set; }
    public ICollection<Produto> Produtos { get; set; }

    // construtores
    public Categoria(string nome, string imagemUrl)
    {
        ValidateDomain(nome, imagemUrl);
    }

    public Categoria(int id, string nome, string imagemUrl)
    {
        DomainExceptionValidation.When(id < 0, "valor de Id inválido");
        id = id;
        ValidateDomain(nome, imagemUrl);
    }

    // métodos
    private void ValidateDomain(string nome, string imagemUrl)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");

        DomainExceptionValidation.When(string.IsNullOrEmpty(imagemUrl), "Nome da iamgem inválido. O nome é obrigatório");

        DomainExceptionValidation.When(nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(imagemUrl.Length < 5, "O nome da imagem deve ter no mínimo 5 caracteres");

        Nome = nome;
        Imagemurl = imagemUrl;
    }
}
