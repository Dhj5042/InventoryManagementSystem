using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Api.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceLifetimeAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }

        public ServiceLifetimeAttribute(ServiceLifetime serviceLifetime)
        {
            LifeTime = serviceLifetime;
        }
    }
}
