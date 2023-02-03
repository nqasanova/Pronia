using System;
using Pronia.Options;

namespace Pronia.Infastructure.Configurations
{
    public static class OptionConfigurations
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfigOptions>(configuration.GetSection(nameof(EmailConfigOptions)));
        }
    }
}