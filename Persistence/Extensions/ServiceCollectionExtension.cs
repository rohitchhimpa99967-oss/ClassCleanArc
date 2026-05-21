using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataContexts;
using Persistence.Extensions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext(configuration);
        service.AddRepository();
    }

    public static void AddDbContext(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
    }

    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork,UnitOfWork>();
    }
}
