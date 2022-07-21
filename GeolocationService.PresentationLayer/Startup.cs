using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeolocationServire.BusinessLogicLayer.Interfaces;
using GeolocationServire.BusinessLogicLayer.Services;
using System.IO;
using GeolocationService.PresentationLayer.Middlewares;

namespace GeolocationService.PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeolocationService.PresentationLayer", Version = "v1" });
            });

            services.AddSingleton<HttpClient>();
            services.AddTransient<IAddressToLocation, NominatimService>();
            services.AddTransient<ILocationToAddress, DadataService>();
           
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<RequestResponseConsoleLoggingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeolocationService.PresentationLayer v1"));
                
                //loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
