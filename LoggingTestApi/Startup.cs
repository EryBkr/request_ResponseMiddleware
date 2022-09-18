using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RequestResponseMiddleware.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTestApi
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

            services.AddControllers();

            //Log Factory testi yapacaðýmýz için öncelikle log servisimizi eklememiz gerekiyor
            services.AddLogging(conf => { conf.AddConsole(); });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoggingTestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingTestApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //Logging Library'imizi ekledik
            app.AddRequestResponseMiddleware(options =>
            {
                //Sistemin kendisinden ILoggerFactory'i talep ediyoruz
                options.UseLogger(app.ApplicationServices.GetRequiredService<ILoggerFactory>(),opt=> 
                {
                    opt.LogLevel = LogLevel.Error;
                    opt.LoggerCategoryName="My Application Category Name";
                    opt.LoggingFields.Add(RequestResponseMiddleware.Library.Models.LogFields.HostName);
                    opt.LoggingFields.Add(RequestResponseMiddleware.Library.Models.LogFields.Response);
                    opt.LoggingFields.Add(RequestResponseMiddleware.Library.Models.LogFields.Request);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
