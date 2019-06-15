using BuyBackAPI.Models;
using BuyBackAPI.Repository;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BuyBackAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        #region Get List of User
        [HttpGet]
        [Route("list")]
        public IActionResult GetAllUsers()
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

            return Ok(response);
        }
        #endregion

        #region Get User Details By ID
        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetUserDetailsById(Request request)
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

            return Ok(response);
        }
        #endregion

        #region Add / Update / Delete User Details
        [HttpPost]
        [Route("manage")]
        public IActionResult ManageUser(UserModel request)
        {
            UserModel user = new UserModel();
            string res = "";

            if (IsValidId(Convert.ToString(request.Id)))
            {
                var Id = ToInt(request.Id);
                user = DbClientFactory<UserDBClient>.instance.GetUserById(GetConnectionString(), Id);

                if (user != null)
                {
                    user.Id = Id;
                    user.Username = request.Username ?? user.Username;
                    user.Password = request.Password ?? user.Password;
                    user.Firstname = request.Firstname ?? user.Firstname;
                    user.Middlename = request.Middlename ?? user.Middlename;
                    user.Lastname = request.Lastname ?? user.Lastname;
                    user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
                    user.Age = request.Age ?? user.Age;
                    user.Gender = request.Gender ?? user.Gender;
                    user.Mobileno = request.Mobileno ?? user.Mobileno;
                    user.Emailaddress = request.Emailaddress ?? user.Emailaddress;
                    user.Bio = request.Bio ?? user.Bio;
                    user.Roleid = request.Roleid ?? user.Roleid;
                    user.IsLocked = request.IsLocked ?? user.IsLocked;
                    user.ProfileImage = request.ProfileImage ?? user.ProfileImage;
                }
            }
            else
            {
                user.Id = 0;
                user.Username = ToStr(request.Username);
                user.Password = ToStr(request.Password);
                user.Firstname = ToStr(request.Firstname);
                user.Middlename = ToStr(request.Middlename);
                user.Lastname = ToStr(request.Lastname);
                user.DateOfBirth = request.DateOfBirth;
                user.Age = request.Age ?? 0;
                user.Gender = ToStr(request.Gender);
                user.Mobileno = ToStr(request.Mobileno);
                user.Emailaddress = ToStr(request.Emailaddress);
                user.Bio = ToStr(request.Bio);
                user.Roleid = ToInt(request.Roleid);
                user.IsLocked = ToInt(request.IsLocked);
                user.ProfileImage = ToStr(request.ProfileImage);
            }

            if (user != null)
            {
                res = DbClientFactory<UserDBClient>.instance.ManageUser(GetConnectionString(), user);

                if (res == "C201")
                {
                    Message = "Username already exist.";
                    response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
                }
                else
                {
                    Count = 1;
                    if (user.Id == 0)
                    {
                        Message = AppConstant.INSERT_MESSAGE;
                    }
                    else
                    {
                        Message = AppConstant.UPDATE_MESSAGE;
                    }
                    response = BuildResponse(AppConstant.STATUS_SUCCESS, Count, Message, null, null);
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