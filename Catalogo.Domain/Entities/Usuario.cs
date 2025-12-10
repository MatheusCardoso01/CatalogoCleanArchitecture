using Catalogo.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Domain.Entities;

public class Usuario : Entity
{
    public string? UserName { get; private set; }

    public string? Email { get; private set; }

    public string PasswordHash { get; private set; } = "temporaria";

    public string? Role { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public bool Ativo { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpiryTime { get; private set; }


    private Usuario() { }

    public Usuario(string? userName, string? email, string passwordHash, string? role, DateTime dataCriacao, bool ativo)
    {
        ValidateDomain(userName, email, passwordHash, role, dataCriacao, ativo);
    }

    public void Update(string? userName, string? email, string passwordHash, string? role, DateTime dataCriacao, bool ativo)
    {
        ValidateDomain(userName, email, passwordHash, role, dataCriacao, ativo);
    }

    public void SetRefreshToken(string? refreshToken, DateTime? refreshTokenExpiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
    }

    private void ValidateDomain(string? userName, string? email, string passwordHash, string? role, DateTime dataCriacao, bool ativo)
    {
        DomainExceptionValidation.When(
            string.IsNullOrWhiteSpace(userName),
            "Nome do usuário é obrigatório");

        DomainExceptionValidation.When(
            userName?.Length < 3,
            "Nome do usuário deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(
            userName?.Length > 100,
            "Nome do usuário deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(
            string.IsNullOrWhiteSpace(email),
            "Email é obrigatório");

        DomainExceptionValidation.When(
            email?.Length > 200,
            "Email deve ter no máximo 200 caracteres");

        DomainExceptionValidation.When(
            !email?.Contains("@") ?? true,
            "Email deve ser válido");

        DomainExceptionValidation.When(
            string.IsNullOrWhiteSpace(passwordHash),
            "Senha é obrigatória");

        DomainExceptionValidation.When(
            string.IsNullOrWhiteSpace(role),
            "Role é obrigatória");

        DomainExceptionValidation.When(
            dataCriacao > DateTime.UtcNow,
            "Data de criação não pode ser uma data futura");

        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        DataCriacao = dataCriacao;
        Ativo = ativo;
    }
}
