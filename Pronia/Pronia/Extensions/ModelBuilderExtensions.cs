using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Pronia.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConfigurationsFromAssembly<T>(this ModelBuilder modelBuilder, Func<Type, bool> predicate = null)
        {
            return modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(T)), predicate);
        }
    }
}