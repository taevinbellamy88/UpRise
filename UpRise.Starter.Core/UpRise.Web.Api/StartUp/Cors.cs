using Microsoft.AspNetCore.Mvc;

namespace UpRise.Web.Api.StartUp
{
    public class Cors
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCors", builder =>
                {
                    builder

                    //.WithOrigins()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .SetIsOriginAllowed(delegate (string requestingOrigin)
                     {
                         return true;
                     }).Build();
                });
            });

            var optison = new MvcOptions();

            services.Configure<MvcOptions>(options =>
            {
                //options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllCors"));
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllCors");
        }
    }
}