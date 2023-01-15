﻿using Microsoft.Extensions.Logging.Console;

namespace UpRise
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .ConfigureAppConfiguration(ConfigConfiguration)
                    .ConfigureLogging(ConfigureLogging)
                    .UseStartup<Startup>();

                });
        }



        private static void ConfigureLogging(WebHostBuilderContext ctx, ILoggingBuilder logging)
        {
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));

            logging.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.ColorBehavior = LoggerColorBehavior.Disabled;
            });

            logging.AddDebug();
        }

        private static void ConfigConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            IConfigurationBuilder root = config.SetBasePath(ctx.HostingEnvironment.ContentRootPath);

            //the settings in the env settings will override the appsettings.json values, recursively at the key level.
            // where the key could be nested. this would allow very fine tuned control over the settings
            IConfigurationBuilder appSettings = root.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string jsonFileName = $"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json";
            IConfigurationBuilder envSettings = appSettings
                .AddJsonFile(jsonFileName, optional: true, reloadOnChange: true);
        }




    }
}