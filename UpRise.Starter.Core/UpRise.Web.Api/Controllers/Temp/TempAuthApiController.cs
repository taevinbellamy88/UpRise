﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UpRise.Models.Interfaces;
using UpRise.Services.Interfaces;
using UpRise.Services.Interfaces.Security;
using UpRise.Web.Core.Configs;
using UpRise.Web.Models.Responses;

namespace UpRise.Web.Api.Controllers.Temp
{
    [Route("api/temp/auth")]
    [ApiController]
    public class TempAuthApiController : BaseApiController
    {
        private IUserService _userService;
        private IAuthenticationService<int> _authService;
        IOptions<SecurityConfig> _options;

        public TempAuthApiController(IUserService userService
            , IAuthenticationService<int> authService
            , ILogger<TempAuthApiController> logger
            , IOptions<SecurityConfig> options) : base(logger)
        {
            _userService = userService;

            _authService = authService;
            _options = options;

        }

        [HttpGet("login/{userId:int}/{userName}/{role}")]
        [AllowAnonymous]
        public async Task<ActionResult<SuccessResponse>> LoginAsync(int userId, string userName, string role)
        {
            await _userService.LogInTest(userName + "@dispostable.com", "password", userId, new string[] { role });

            ItemResponse<object> response = new ItemResponse<object>();
            response.Item = _options;
            return Ok200(response);
        }

        [HttpGet("current")]
        public ActionResult<ItemResponse<IUserAuthData>> GetCurrrent()
        {
            IUserAuthData user = _authService.GetCurrentUser();
            ItemResponse<IUserAuthData> response = new ItemResponse<IUserAuthData>();
            response.Item = user;

            return Ok200(response);
        }

        [HttpPost("current")]
        public ActionResult<ItemResponse<IUserAuthData>> GetCurrrentPost()
        {
            IUserAuthData user = _authService.GetCurrentUser();
            ItemResponse<IUserAuthData> response = new ItemResponse<IUserAuthData>();
            response.Item = user;

            return Ok200(response);
        }

        [HttpGet("logout")]
        public async Task<ActionResult<SuccessResponse>> LogoutAsync()
        {
            await _authService.LogOutAsync();
            SuccessResponse response = new SuccessResponse();
            return Ok200(response);
        }


        #region - Examples of Authorization via Roles or AllowAnonymous
        [HttpGet("anyone")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> Get()
        {
            Logger.LogInformation($"Endpoing firing {ControllerContext.ActionDescriptor.ActionName} with anyone route");

            SuccessResponse response = new SuccessResponse();
            return Ok200(response);
        }

        [HttpGet("reject")]
        public ActionResult<SuccessResponse> Reject()
        {
            SuccessResponse response = new SuccessResponse();
            return Ok200(response);
        }

        [HttpGet("bloggers")]
        [Authorize(Roles = "Blogger")]
        public ActionResult<SuccessResponse> Bloggers()
        {
            SuccessResponse response = new SuccessResponse();
            return Ok200(response);
        }

        [HttpGet("super")]
        [Authorize(Roles = "SuperAdminBro")]
        public ActionResult<SuccessResponse> Super()
        {
            SuccessResponse response = new SuccessResponse();
            return Ok200(response);
        }
        #endregion
    }
}