using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Api.Utility
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterServices(this IServiceCollection services, Assembly assembly, Func<Type, ServiceLifetime> actionLifeTime)
        {
            // Get class library
            var serviceLibraries = assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces()
                .Any(i => i.Name.Contains("Service")) && x.Namespace.Contains(".Services.Services")
                     && !string.IsNullOrEmpty(x.Name) && x.Name.Contains("Service"))
                .ToList();

            if (serviceLibraries.Any() && serviceLibraries.Count > 0)
            {
                for (int index = 0; index < serviceLibraries.Count; index++)
                {
                    Type serviceType = serviceLibraries[index];
                    var interfaceType = serviceType.GetInterfaces().FirstOrDefault();
                    ServiceLifetime lifetime = actionLifeTime?.Invoke(serviceType) ?? ServiceLifetime.Transient;
                    services.Add(new ServiceDescriptor(interfaceType, serviceType, lifetime));
                }
            }
        }
        public static void RegisterRepository(this IServiceCollection services, Assembly assembly)
        {
            // Get class library
            var repositoryLibraries = assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Any()
                    && x.Namespace.Contains(".Repository.Repository")
                    && !x.FullName.Contains("BaseRepository")
                    && !x.Name.Contains("<"))
                .ToList();

            if (repositoryLibraries.Any() && repositoryLibraries.Count > 0)
            {
                for (int index = 0; index < repositoryLibraries.Count; index++)
                {
                    Type repositoryType = repositoryLibraries[index];
                    var interfaceType = repositoryType.GetInterfaces()
                        .FirstOrDefault(x => !x.Name.Contains("IBaseRepository")
                        && x.Name.Contains(repositoryType.Name));
                    if (interfaceType != null && interfaceType.IsGenericType)
                    {
                        Type genericTypeDefinition = interfaceType.GetGenericTypeDefinition();

                        Type[] typeArguments = interfaceType.GetGenericArguments();

                        if (typeArguments.Length != 1)
                        {
                            continue;
                        }
                        interfaceType = genericTypeDefinition.MakeGenericType(typeArguments);
                        services.AddTransient(genericTypeDefinition, repositoryType);
                    }
                    else
                    {
                        services.AddTransient(interfaceType, repositoryType);
                    }
                }
            }
        }
    }
}
