using System;
using Microsoft.EntityFrameworkCore;
using Pronia.Database;
using Pronia.Services.Abstracts;
using Pronia.Services.Concretes;

namespace Pronia.Infastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}