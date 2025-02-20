using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;
using Talabat.Repository.Repositories;
using Talabat.Services;
using TalabatAPI.Errors;
using TalabatAPI.Helpers;

namespace TalabatAPI.Extentions
{
    public static class AddApplicationServices
    {
        public static  IServiceCollection ApplicationServices(this IServiceCollection services) 
        {
            services.AddSingleton<IResponseCachedServise, ResponseCachedServise>();
            services.AddScoped<IPaymentServices, PaymentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddLogging();
            services.Configure<ApiBehaviorOptions>(Option =>
            {
                Option.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                                             .SelectMany(v => v.Value.Errors)
                                                             .Select(e => e.ErrorMessage)
                                                             .ToList();
                    var response = new ApiResponseValidationError()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };

            });
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IOrderServices), typeof(OrderServices));
            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
           
            return services;
        }

        public static async Task< WebApplication >LoggerMiddleWare(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _DbContext = service.GetRequiredService<StoreDbContext>();
            var _IdentityDbContext = service.GetRequiredService<AppIdentityDbContext>();
            var _userManger = service.GetRequiredService<UserManager<AppUser>>();
            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await _DbContext.Database.MigrateAsync();
                await StoreContextSeedData.SeedingAsync(_DbContext);
                await _IdentityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeedData.SeedUserAsync(_userManger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Migration Error!");
            }
            return app;
        }

       
    }
}
