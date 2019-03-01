using BuyBackAPI.Models;
using BuyBackAPI.Repository;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BuyBackAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        [HttpGet]
        [Route("List")]
        public Response GetAllUsers()
        {
            var data = DbClientFactory<UserDBClient>.instance.GetAllUsers(GetConnectionString());

            if (data != null && data.Count > 0)
            {
                Message = AppConstant.RECORD_FOUNT_MESSAGE;
                Count = data.Count;
                response = BuildResponse(AppConstant.STATUS_SUCCESS, Count, Message, data, null);
            }
            else
            {
                Message = AppConstant.RECORD_NOT_FOUND_MESSAGE;
                response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
            }

            return response;
        }

        [HttpGet]
        [Route("GetById")]
        public Response GetUserDetailsById(Request request)
        {
            if (IsValidId(request.Id))
            {
                var Id = ToInt(request.Id);

                var data = DbClientFactory<UserDBClient>.instance.GetUserById(GetConnectionString(), Id);

                if (data != null)
                {
                    Message = AppConstant.RECORD_FOUNT_MESSAGE;
                    Count = 1;
                    response = BuildResponse(AppConstant.STATUS_SUCCESS, Count, Message, data, null);
                }
                else
                {
                    Message = AppConstant.RECORD_NOT_FOUND_MESSAGE;
                    response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
                }
            }
            else
            {
                Message = AppConstant.INVALID_ID;
                response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
            }

            return response;
        }
    }
}