using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RequestResponseMiddleware.Library;
using RequestResponseMiddlewareFileLogger.Library;
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

            //File Logging Library'imizi ekledik
            app.AddRequestResponseFileLoggerMiddleware(options =>
            {
                //Action olarak ayarlarý alýyoruz,dikkat edersek new operasyonu yapmadýk, dll bu iþlemi kendisi yapacak
                options.FileDirectory = AppDomain.CurrentDomain.BaseDirectory;
                options.FileName = "Eray_Bakýr_Log";
                options.Extension = "txt";
                options.UseJsonFormat = false;
                options.ForceCreateDirectory = true;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
