using DevHours.CloudNative.Api.Configuration;
using DevHours.CloudNative.Api.ErrorHandling.Extensions;
using DevHours.CloudNative.Api.JsonConverters;
using DevHours.CloudNative.Application;
using DevHours.CloudNative.Core;
using DevHours.CloudNative.DataAccess;
using DevHours.CloudNative.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;

namespace DevHours.CloudNative.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCloudNativeCore()
                    .AddCloudNativeInfrastructure(Configuration);

            services.AddControllers(options => options.EnableEndpointRouting = false)
                    .AddJsonOptions(o => 
                    {
                        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        o.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddErrorHandler();
            services.AddCQS();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var allowedOrigins = Configuration.GetSection("Cors").Get<CorsConfig>().AllowedOrigins;
            app.UseCors(options => options.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandler();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("DevHours Cloud Native Booking API"));
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<HotelContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
