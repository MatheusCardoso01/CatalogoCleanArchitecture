using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaPatchDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoPatchDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();        
        // REMOVIDO: CreateMap<Usuario, UsuarioRegistroDTO>().ReverseMap();
        
        // Mapeamento de UsuarioRegistroDTO -> Usuario COM VALIDAÇÃO
        CreateMap<UsuarioRegistroDTO, Usuario>()
            .ConstructUsing(dto => new Usuario(
                dto.UserName,
                dto.Email,
                dto.Password,
                "User",
                DateTime.UtcNow,
                true
            ));
    }
}
