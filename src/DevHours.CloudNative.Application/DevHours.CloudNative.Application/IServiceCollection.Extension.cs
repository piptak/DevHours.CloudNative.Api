using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevHours.CloudNative.Application
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCQS(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
