using System;
using Microsoft.EntityFrameworkCore;
using Pronia.Database;
using static Pronia.Database.DataContext;

namespace Pronia.Infastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("NatavanMAC"));
            });
        }
    }
}