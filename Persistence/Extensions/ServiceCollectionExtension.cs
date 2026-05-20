using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext(configuration);
    }

    public static void AddDbContext(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
    }
}
