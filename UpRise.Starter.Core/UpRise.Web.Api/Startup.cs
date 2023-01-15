﻿using UpRise.AppSettings;
using UpRise.StartUp;
using UpRise.Web.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace UpRise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [JsonProperty("configuration")]
        [JsonPropertyName("configuration")]
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMemoryCache();

            ConfigureAppSettings(services);

            DependencyInjection.ConfigureServices(services, Configuration);

            Cors.ConfigureServices(services);

            Authentication.ConfigureServices(services, Configuration);

            MVC.ConfigureServices(services);

            SPA.ConfigureServices(services);
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<SecurityConfig>(Configuration.GetSection("SecurityConfig"));
            services.Configure<JsonWebTokenConfig>(Configuration.GetSection("JsonWebTokenConfig"));
            services.Configure<AppKeys>(Configuration.GetSection("AppKeys"));
            services.Configure<AWSStorageConfig>(Configuration.GetSection("AWSStorageConfig"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //per https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.1&tabs=visual-studio#routing-startup-code
            // static files should be called before UseRouting
            StaticFiles.Configure(app, env);

            app.UseRouting();
            Cors.Configure(app, env);
            Authentication.Configure(app, env);


            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                //endpoints.MapHub<ChatHub>("/chathub");
            });


            if (!env.IsDevelopment())
            {
                var certificate = new X509Certificate2("");
                var serverOptions = new KestrelServerOptions();
                serverOptions.Listen(IPAddress.Any, 443, listenOptions =>
                {
                    listenOptions.UseHttps(certificate);
                });
                app.UseHttpsRedirection();
            }
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseHsts();


            MVC.Configure(app, env);

            SPA.Configure(app, env);

        }
    }
}