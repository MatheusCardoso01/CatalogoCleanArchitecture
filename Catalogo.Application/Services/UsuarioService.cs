using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Services;

public class UsuarioService : IUsuarioService
{
    private IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private IPasswordHasher _passwordHasher;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDTO> Add(UsuarioRegistroDTO usuarioRegistroDTO)
    {
        usuarioRegistroDTO.Password = _passwordHasher.HashPassword(usuarioRegistroDTO.Password);

        var usuarioEntity = _mapper.Map<Usuario>(usuarioRegistroDTO);

        await VerifyData(usuarioEntity);

        usuarioEntity = await _usuarioRepository.CreateAsync(usuarioEntity);

        return _mapper.Map<UsuarioDTO>(usuarioEntity);
    }

    public async Task<UsuarioDTO> GetById(int id)
    {
        var usuarioEntity = await _usuarioRepository.GetByIdAsync(id);

        if (usuarioEntity is null)
            return null;

        return _mapper.Map<UsuarioDTO>(usuarioEntity);
    }

    public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
    {
        var usuariosEntity = await _usuarioRepository.GetUsuariosAsync();

        if (usuariosEntity is null || !usuariosEntity.Any())
            return Enumerable.Empty<UsuarioDTO>();

        return _mapper.Map<IEnumerable<UsuarioDTO>>(usuariosEntity);
    }

    public async Task<UsuarioDTO> Update(UsuarioDTO usuarioDTO)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(usuarioDTO.Id);

        if (usuarioExistente is null)
            return null;

        var usuarioEntity = _mapper.Map<Usuario>(usuarioDTO);

        await VerifyData(usuarioEntity);

        usuarioEntity = await _usuarioRepository.UpdateAsync(usuarioEntity, usuarioExistente);

        return usuarioDTO;
    }

    public async Task<UsuarioDTO> Remove(int id)
    {
        var usuarioEntity = await _usuarioRepository.GetByIdAsync(id);

        if (usuarioEntity is null)
            return null;

        usuarioEntity = await _usuarioRepository.RemoveAsync(usuarioEntity);

        return _mapper.Map<UsuarioDTO>(usuarioEntity);
    }

    // métodos auxiliares de validação

    private async Task VerifyData(Usuario usuarioEntity)
    {
        var otherUserSameEmail = await _usuarioRepository.GetByEmailAsync(usuarioEntity.Email);

        if (otherUserSameEmail is not null && otherUserSameEmail.Id != usuarioEntity.Id)
            throw new InvalidOperationException("Email já cadastrado");

        var otherUserSameName = await _usuarioRepository.GetByUserNameAsync(usuarioEntity.UserName);

        if (otherUserSameName is not null && otherUserSameName.Id != usuarioEntity.Id)
            throw new InvalidOperationException("Nome já cadastrado");
    }

}
