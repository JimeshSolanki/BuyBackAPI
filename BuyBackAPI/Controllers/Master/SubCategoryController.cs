using BuyBackAPI.Models;
using BuyBackAPI.Models.Master;
using BuyBackAPI.Repository.MasterDBClient;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BuyBackAPI.Controllers.Master
{
    [ApiController]
    [Route("api/subcategory")]
    public class SubCategoryController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        #region Get List of Sub Category
        [HttpPost]
        [Route("list")]
        public IActionResult GetAllSubCategory()
        {
            var data = DbClientFactory<SubCategoryDBClient>.instance.GetAllSubCategories(GetConnectionString());

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

        #region Get Sub Category Details By ID
        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetCategoryDetailsById(Request request)
        {
            if (IsValidId(request.Id))
            {
                var Id = ToInt(request.Id);

                var data = DbClientFactory<SubCategoryDBClient>.instance.GetSubCategoryById(GetConnectionString(), Id);

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

        #region Add / Update / Delete Sub Category Details
        [HttpPost]
        [Route("manage")]
        public IActionResult ManageSubCategory(SubCategoryModel request)
        {
            var subcategory = new SubCategoryModel();
            string res = "";

            if (IsValidId(ToStr(request.Id.ToString())))
            {
                var Id = ToInt(request.Id);
                subcategory = DbClientFactory<SubCategoryDBClient>.instance.GetSubCategoryById(GetConnectionString(), Id);

                if (subcategory != null)
                {
                    subcategory.Id = Id;
                    subcategory.CategoryId = request.CategoryId ?? subcategory.CategoryId;
                    subcategory.SubCategoryName = request.SubCategoryName ?? subcategory.SubCategoryName;
                }
            }
            else
            {
                subcategory.Id = 0;
                subcategory.SubCategoryName = ToStr(request.SubCategoryName);
            }

            if (subcategory != null)
            {
                res = DbClientFactory<SubCategoryDBClient>.instance.ManageSubCategory(GetConnectionString(), subcategory);

                if (res == "C200")
                {
                    Count = 1;
                    if (subcategory.Id == 0)
                    {
                        Message = AppConstant.INSERT_MESSAGE;
                    }
                    else
                    {
                        Message = AppConstant.UPDATE_MESSAGE;
                    }
                    response = BuildResponse(AppConstant.STATUS_SUCCESS, Count, Message, null, null);
                }
                else if (res == "C201")
                {
                    Message = "Sub Category already exist.";
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
    }
}