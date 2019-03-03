using BuyBackAPI.Models;
using BuyBackAPI.Models.Master;
using BuyBackAPI.Repository.Master;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BuyBackAPI.Controllers.Master
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        #region Get List of Role
        [HttpPost]
        [Route("list")]
        public IActionResult GetAllRoles()
        {
            var data = DbClientFactory<RoleDBClient>.instance.GetAllRoles(GetConnectionString());

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

            return Ok(response);
        }
        #endregion

        #region Get Role Details By ID
        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetRoleDetailsById(Request request)
        {
            if (IsValidId(request.Id))
            {
                var Id = ToInt(request.Id);

                var data = DbClientFactory<RoleDBClient>.instance.GetRoleById(GetConnectionString(), Id);

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

            return Ok(response);
        }
        #endregion

        #region Add / Update / Delete Role Details
        [HttpPost]
        [Route("manage")]
        public IActionResult ManageRole(RoleModel request)
        {
            var role = new RoleModel();
            string res = "";

            if (IsValidId(ToStr(request.Id.ToString())))
            {
                var Id = ToInt(request.Id);
                role = DbClientFactory<RoleDBClient>.instance.GetRoleById(GetConnectionString(), Id);

                if (role != null)
                {
                    role.Id = Id;
                    role.RoleName = request.RoleName ?? role.RoleName;
                }
            }
            else
            {
                role.Id = 0;
                role.RoleName = ToStr(request.RoleName);
            }

            if (role != null)
            {
                res = DbClientFactory<RoleDBClient>.instance.ManageRole(GetConnectionString(), role);

                if (res == "C201")
                {
                    Message = "Role already exist.";
                    response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
                }
                else
                {
                    Count = 1;
                    if (role.Id == 0)
                    {
                        Message = AppConstant.INSERT_MESSAGE;
                    }
                    else
                    {
                        Message = AppConstant.UPDATE_MESSAGE;
                    }
                    response = BuildResponse(AppConstant.STATUS_SUCCESS, Count, Message, res, null);
                }
            }
            else
            {
                Message = AppConstant.INVALID_ID;
                response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
            }
            return Ok(response);
        }
        #endregion
    }
}