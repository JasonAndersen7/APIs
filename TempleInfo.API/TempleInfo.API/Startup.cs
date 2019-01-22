using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempleInfo.API.Entities;
using TempleInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace TempleInfo.API
{
    public class Startup
    {

        // public static IConfigurationRoot Configuration;
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            //builds a configuration with keys and values stored from source file
            
            Configuration = builder.Build();
        }

        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            //    .AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        //ignore and take naming strategy as they are handled in the class
            //        castedResolver.NamingStrategy = null;
            //    }
            //}

            //    );

#if DEBUG
            //refer to lifetime, works best for lightweight stateless requests 
            // services.AddTransient<IMailService, LocalMailService>();

            services.AddScoped<IMailService, LocalMailService>();

#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString =Startup.Configuration["connectionStrings:templeInfoDBConnectionString"];
            services.AddDbContext<TempleInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ITempleInfoRepository, TempleInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TempleInfoContext templeInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            templeInfoContext.EnsureSeedDataForContext();

            //used for responses that don't hahve a body
            app.UseStatusCodePages();

            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Temple, Models.TempleOnlyDTO>();
                cfg.CreateMap<Entities.Temple, Models.TempleDTO>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
                cfg.CreateMap<Models.PointOfInterestCreationDto, Entities.PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestUpdateDto, Entities.PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestDto, Entities.PointOfInterest>();

            });

            //app.Run(async (context) =>
            //{
            //    throw new Exception("Example exception");
            //});


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
