using UpRise.Data.Interfaces;
using UpRise.Data.Providers;
using UpRise.Services;
using UpRise.Services.FAQ;
using UpRise.Services.Interfaces;
using UpRise.Services.Interfaces.Security;
using UpRise.Web.Core.Services;

namespace UpRise.Web.Api.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // The are a number of differe Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();
            services.AddSingleton<IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );
            services.AddSingleton<IIdentityProvider<int>, WebAuthenticationService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Do NOT REMOVE this line below.
            // Edits to the IUserService are OK 
            services.AddSingleton<IUserService, UserService>();


            //Add Singletons
            //services.AddSingleton<IFooInterface, FooService>();
            services.AddSingleton<IFAQService, FAQService>();




        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}