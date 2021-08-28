using DevHours.CloudNative.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevHours.CloudNative.Core
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudNativeCore(this IServiceCollection services)
        {
            services.AddScoped<RoomImagesService>();
            return services;
        }
    }
}