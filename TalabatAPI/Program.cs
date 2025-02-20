    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;
    using System.Text;
    using Talabat.Core.Entities.Identity;
    using Talabat.Core.Repository.Contract;
    using Talabat.Repository.Data;
    using Talabat.Repository.Data.Identity;
    using Talabat.Repository.Repositories;
    using TalabatAPI.Errors;
    using TalabatAPI.Extentions;
    using TalabatAPI.Helpers;
 
     var builder = WebApplication.CreateBuilder(args);
 
     #region Services(Depandancy injection)
     // Add services to the container.
 
     builder.Services.AddControllers();
 
     builder.Services.SwaggerServices();
 
 
     builder.Services.AddDbContext<StoreDbContext>(option =>
     option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
    builder.Services.AddDbContext<AppIdentityDbContext>(option =>
     option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString")));
    builder.Services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
    {
        var radisConnectionString = builder.Configuration.GetConnectionString("Redis");
        return ConnectionMultiplexer.Connect(radisConnectionString);
    });
     builder.Services.ApplicationServices();
    builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, Options =>
    {
             Options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidIssuer= builder.Configuration["JWT:Issure"],
                 ValidateAudience = true,
                 ValidAudience = builder.Configuration["JWT:Audience"]
                 ,ValidateLifetime = true,
                 ClockSkew=TimeSpan.Zero,
                 ValidateIssuerSigningKey=true,
                 IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"]??string.Empty))
             };
    });

    #endregion


    #region Middlewares

    var app = builder.Build();
 
     app.LoggerMiddleWare();
 
     // Configure the HTTP request pipeline.
     app.UseMiddleware<ExceptionMiddleware>();
 
     if (app.Environment.IsDevelopment())
     {
         app.Swaggermiddlewares();
     }

    app.UseStatusCodePagesWithReExecute("/errors/{0}");
     app.UseHttpsRedirection();
app.UseAuthentication();
     app.UseAuthorization();
     app.UseStaticFiles();
     app.MapControllers();
 
     app.Run();
 
     #endregion