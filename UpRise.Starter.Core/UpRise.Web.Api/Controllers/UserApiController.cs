using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpRise.Domain.User;
using UpRise.Interfaces;
using UpRise.Requests.User;
using UpRise.Web.Models.Responses;

namespace UpRise.Web.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        private IUserService _userService = null;
        private IAuthenticationService<int> _authService = null;
        public UserApiController(IUserService service, ILogger<IUserService> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _userService = service;
            _authService = authService;
        }

        [HttpPost()]
        [AllowAnonymous]
        public ActionResult<ItemResponse<int>> Add(UserAddRequest model)
        {

            ObjectResult? result = null;

            try
            {
                int id = _userService.Create(model);

                string token = Guid.NewGuid().ToString();

                _userService.InsertToken(token, id, 1);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }

            return result;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<SuccessResponse>> LoginAsync(UserLogin model)
        {
            int code = 200;
            BaseResponse response = null;
            bool success = false;

            try
            {
                success = await _userService.LogInAsync(model.Email, model.Password);

                response = (success == false) ? new ErrorResponse("Login Error.") : new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPut("confirm/{token}")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> ConfirmUser(string token)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _userService.ConfirmUser(token);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("current")]
        public ActionResult<ItemResponse<User>> CurrentUser()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                IUserAuthData user = _authService.GetCurrentUser();
                if (user == null)
                {
                    code = 404;
                    response = new ErrorResponse("User not found.");
                }
                else
                {
                    response = new ItemResponse<IUserAuthData>() { Item = user };
                }

            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);

        }

        [HttpGet("logout")]
        public async Task<ActionResult<SuccessResponse>> Logout()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                await _authService.LogOutAsync();
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }


        //[HttpPut("changepassword")]
        //[AllowAnonymous]
        //public ActionResult<SuccessResponse> ResetPassword(UserUpdatePasswordRequest model)
        //{
        //    int code = 200;
        //    BaseResponse response = null;
        //    model.Id = _userService.GetByEmail(model.Email);

        //    try
        //    {
        //        _userService.UpdatePassword(model);

        //        _userService.DeleteToken(model.Token);

        //        response = new SuccessResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        code = 500;
        //        base.Logger.LogError(ex.ToString());
        //        response = new ErrorResponse(ex.Message);
        //    }
        //    return StatusCode(code, response);
        //}

    }
}
