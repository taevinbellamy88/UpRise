using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace UpRise.StartUp
{
    public class SPA
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWhen(
                    context => !context.Request.Path.StartsWithSegments("/api"),
                    appBuilder => appBuilder.UseSpaStaticFiles());

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api"), UseSinglePageApplication);

            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }

            });
        }

        public static void UseSinglePageApplication(IApplicationBuilder app)
        {
            app.UseSpa(spa =>
            {

                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}