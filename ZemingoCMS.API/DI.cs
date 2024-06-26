using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Infastructure.Data.EF;

namespace ZemingoCMS.API
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(CommandResult).Assembly;

            var serviceDescriptors = assembly.DefinedTypes
                .Where(x => x.FullName.EndsWith("Handler")
                            && !x.IsInterface).ToArray();

            foreach (var serviceDescriptor in serviceDescriptors)
            {
                var handlerInterface = serviceDescriptor.GetInterfaces()
                    .Where(x => x.Name.Contains(serviceDescriptor.Name))
                    .FirstOrDefault()
                    ?? throw new ArgumentNullException("Could not register scoped interface for class: "
                        + serviceDescriptor.FullName);
                services.AddScoped(handlerInterface, serviceDescriptor);
            }

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<CMSDbContext>(options =>
            {
                options.UseInMemoryDatabase("CMSDb");
            });

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

            return services;
        }
    }
}
