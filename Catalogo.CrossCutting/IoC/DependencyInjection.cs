using Catalogo.Application.Interfaces;
using Catalogo.Application.Mappings;
using Catalogo.Application.Services;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 43))));

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();

        services.AddAutoMapper(config =>
        {
            config.AddProfile<DomainToDTOMappingProfile>();
        });

        return services;
    }
}
