using System;
using Pronia.Database.Models;

namespace Pronia.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}