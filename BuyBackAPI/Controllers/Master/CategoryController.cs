using BuyBackAPI.Models;
using BuyBackAPI.Models.Master;
using BuyBackAPI.Repository.MasterDBClient;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BuyBackAPI.Controllers.Master
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        #region Get List of Category
        [HttpPost]
        [Route("list")]
        public IActionResult GetAllCategories()
        {
            var data = DbClientFactory<CategoryDBClient>.instance.GetAllCategories(GetConnectionString());

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

        #region Get Category Details By ID
        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetCategoryDetailsById(Request request)
        {
            if (IsValidId(request.Id))
            {
                var Id = ToInt(request.Id);

                var data = DbClientFactory<CategoryDBClient>.instance.GetCategoryById(GetConnectionString(), Id);

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

        #region Add / Update / Delete Category Details
        [HttpPost]
        [Route("manage")]
        public IActionResult ManageCategory(CategoryModel request)
        {
            var category = new CategoryModel();
            string res = "";

            if (IsValidId(ToStr(request.Id.ToString())))
            {
                var Id = ToInt(request.Id);
                category = DbClientFactory<CategoryDBClient>.instance.GetCategoryById(GetConnectionString(), Id);

                if (category != null)
                {
                    category.Id = Id;
                    category.CategoryName = request.CategoryName ?? category.CategoryName;
                }
            }
            else
            {
                category.Id = 0;
                category.CategoryName = ToStr(request.CategoryName);
            }

            if (category != null)
            {
                res = DbClientFactory<CategoryDBClient>.instance.ManageCategory(GetConnectionString(), category);

                if (res == "C201")
                {
                    Message = "Category already exist.";
                    response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
                }
                else
                {
                    Count = 1;
                    if (category.Id == 0)
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