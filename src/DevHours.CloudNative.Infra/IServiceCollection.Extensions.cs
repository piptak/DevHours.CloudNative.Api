using DevHours.CloudNative.Core.Repositories;
using DevHours.CloudNative.DataAccess;
using DevHours.CloudNative.Domain;
using DevHours.CloudNative.Infra.Repositories;
using DevHours.CloudNative.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevHours.CloudNative.Infra
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudNativeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HotelContext>(o =>
            {
                var hotelConnectionString = configuration.GetConnectionString("HotelDbConnection");

                if (string.IsNullOrWhiteSpace(hotelConnectionString))
                {
                    o.UseInMemoryDatabase("hoteldb");
                }
                else
                {
                    o.UseNpgsql(connectionString: hotelConnectionString);
                }
            });

            services.AddScoped<Core.Repositories.Read.IRoomBookingRepository, Repositories.Read.RoomBookingRepository>();
            services.AddScoped<Core.Repositories.Write.IRoomBookingRepository, Repositories.Write.RoomBookingRepository>();

            services.AddScoped<IBlobRepository<string>>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new RoomImagesRepository(
                    configuration.GetConnectionString("Images"),
                    "images"
                );
            });

            return services;
        }
    }
}