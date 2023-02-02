using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpRise.Models.Domain.Faq;
using UpRise.Models.Requests.FAQ;
using UpRise.Services.FAQ;
using UpRise.Services.Interfaces.Security;
using UpRise.Web.Models.Responses;

namespace UpRise.Web.Api.Controllers.FAQ
{
    [Route("api/user/faq")]
    [ApiController]
    public class FaqWebApiController : BaseApiController
    {
        private IFAQService _Service = null;
        private IAuthenticationService<int> _AuthenticationService = null;

        public FaqWebApiController(IFAQService service, ILogger<FaqWebApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _Service = service;
            _AuthenticationService = authService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<BaseFAQ>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                BaseFAQ faq = _Service.getById(id);
                if (faq == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found");
                }
                else
                {

                    response = new ItemResponse<BaseFAQ>() { Item = faq };

                }
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());

            }

            return StatusCode(iCode, response);
        }

        [HttpGet]
        public ActionResult<ItemsResponse<BaseFAQ>> Get()
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                List<BaseFAQ> list = _Service.getAll();

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<BaseFAQ> { Items = list };
                }
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());

            }


            return StatusCode(iCode, response);
        }

        [HttpPost]
        public ActionResult Add(FaqAddRequest model)
        {

            ObjectResult result = null;

            try
            {

                int userId = _AuthenticationService.GetCurrentUserId();
                int id = _Service.AddFaq(model, userId);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);

            }

            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, response);
            }


            return result;

        }

        [HttpPut("{id:int}")]
        public ActionResult< ItemResponse<int> > Update(FaqUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _AuthenticationService.GetCurrentUserId();
                _Service.Update(model, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode= 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }


        [HttpDelete("{id:int}")]
        public ActionResult<ItemResponse<int>> Delete(int id)
        {
            int iCode = 200;
            BaseResponse response = null;


            try
            {
                _Service.delete(id);
                response = new SuccessResponse();
            }

            catch (Exception ex1)
            {
                iCode= 500;
                base.Logger.LogError(ex1.ToString());
                response = new ErrorResponse(ex1.Message);

            }


            return StatusCode(iCode, response);
        }
    }
}
