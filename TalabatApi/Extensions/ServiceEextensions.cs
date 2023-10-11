using CoreLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Data;
using RepositoryLayer;
using TalabatApi.Helper;
using TalabatApi.Errors;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using CoreLayer.Services;
using ServiceLayer;

namespace TalabatApi.Extensions
{
    public static class ServiceEextensions
    {
        
        public static IServiceCollection AddServices(this IServiceCollection services , IConfiguration config )
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Default"));
            });

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<ICachingService, CachingService>();



            services.AddAutoMapper(typeof(MappingProfiles));

            

            services.Configure<ApiBehaviorOptions>(options => {

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                          .SelectMany(P => P.Value.Errors)
                                                          .Select(E => E.ErrorMessage).ToArray();

                    var validationError = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationError);
                };



            });

            services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                return  ConnectionMultiplexer.Connect(config.GetConnectionString("Redis"));
            });
            return services;

            
        }

       
    }
}
