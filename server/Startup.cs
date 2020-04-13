using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Server.Services.Interfaces;
using Server.Stores;
using Server.Stores.Interfaces;

namespace Server
{
   public class Startup
   {
      private readonly IConfiguration _configuration;
      public Startup(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddDbContextPool<Store>(
             options => options.UseMySql(_configuration["Database:MySQL"],
               server =>
               {
                  server.ServerVersion(
                     new ServerVersion(
                        new Version(8, 0, 18),
                        ServerType.MySql
                     )
                  );
                  server.CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                  server.CharSet(CharSet.Utf8Mb4);
                  server.MigrationsHistoryTable(
                     _configuration["Database:History"]
                  );
               }
            )
         );
         services.AddScoped<IUserStore, UserStore>();
         services.AddScoped<ILessonStore, LessonStore>();
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<ILessonService, LessonService>();
         services.AddAuthentication(
            option =>
            {
               option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
         ).AddJwtBearer(bearer =>
         {
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               RequireExpirationTime = true,
               ClockSkew = TimeSpan.Zero,
               ValidIssuer = _configuration["Token:Issuer"],
               ValidAudience = _configuration["Token:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Token:Secret"])
               )
            };
         });
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         //app.UseHttpsRedirection();

         app.UseRouting();

         app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
         );

         app.UseAuthentication();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            }
         );
      }
   }
}
