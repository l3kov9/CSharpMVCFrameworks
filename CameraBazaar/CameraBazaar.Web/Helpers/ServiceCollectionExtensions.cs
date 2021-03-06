﻿namespace CameraBazaar.Web.Helpers
{
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(ICameraService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}
