﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UpRise.Models.AppSettings;
using UpRise.Web.Models.Responses;

namespace UpRise.Web.Api.Controllers.Temp
{
    /// <summary>
    /// This controller is not required for the application to work. 
    /// You could remove it but it serves as a simple entry point for development
    /// </summary>
    [Route("api/ping")]
    [ApiController]
    public class PingApiController : BaseApiController
    {
        private AppKeys _appKeys;
        public PingApiController(IOptions<AppKeys> appKeys, ILogger<PingApiController> logger) : base(logger)
        {
            _appKeys = appKeys.Value;
        }

        [HttpGet()]
        [AllowAnonymous]
        public ActionResult<ItemResponse<object>> Ping()
        {
            Logger.LogInformation("Ping endpoint firing");

            ItemResponse<object> response = new ItemResponse<object>();

            response.Item = DateTime.Now.Ticks;

            return Ok200(response);
        }
    }
}