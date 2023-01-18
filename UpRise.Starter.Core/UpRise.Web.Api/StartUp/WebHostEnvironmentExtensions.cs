using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpRise.Web.Api.StartUp
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsDevelopment(this IWebHostEnvironment env)
        {
            return env.EnvironmentName.ToLower().Equals("development");
        }
    }
}
