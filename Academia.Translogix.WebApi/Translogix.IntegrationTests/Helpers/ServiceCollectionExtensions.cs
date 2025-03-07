using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Translogix.IntegrationTests.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveService<T>(this IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
            if (descriptor != null) services.Remove(descriptor);
        }

        public static void RemoveDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var descriptors = services.Where(d => d.ServiceType == typeof(TContext) ||
                                                 d.ServiceType == typeof(DbContextOptions<TContext>))
                                     .ToList();
            foreach (var descriptor in descriptors) services.Remove(descriptor);
        }

        // Nuevo método para remover múltiples servicios según un predicado
        public static void RemoveAll(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate)
        {
            var descriptorsToRemove = services.Where(predicate).ToList();
            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }
        }
    }
}
