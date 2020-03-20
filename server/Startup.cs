using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Server.Services.Interfaces;
using Server.Stores;
using Server.Stores.Interfaces;

namespace Server
{
   public class Startup
   {
      public IConfiguration Configuration { get; }
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddCors();
         services.AddControllers();
         services.AddDbContextPool<Store>(
             options => options.UseMySql(
                 "Server=localhost;Database=Education;User=root;Password=root;",
                 server => server.ServerVersion(
                     new ServerVersion(
                         new Version(8, 0, 18),
                         ServerType.MySql
                     )
                 )
             )
         );

         services.AddScoped<IUserStore, UserStore>();

         services.AddScoped<IUserService, UserService>();
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
