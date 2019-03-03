using BuyBackAPI.Models;
using BuyBackAPI.Models.Master;
using BuyBackAPI.Repository.Master;
using BuyBackAPI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BuyBackAPI.Controllers.Master
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : BaseController
    {
        Response response = null;
        string Message = null;
        int Count = 0;

        #region Get List of Product
        [HttpPost]
        [Route("list")]
        public IActionResult GetAllProducts()
        {
            var data = DbClientFactory<ProductDBClient>.instance.GetAllProducts(GetConnectionString());

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

        #region Get Product Details By ID
        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetProductDetailsById(Request request)
        {
            if (IsValidId(request.Id))
            {
                var Id = ToInt(request.Id);

                var data = DbClientFactory<ProductDBClient>.instance.GetProductById(GetConnectionString(), Id);

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

        #region Add / Update / Delete Product Details
        [HttpPost]
        [Route("manage")]
        public IActionResult ManageProduct(ProductModel request)
        {
            var product = new ProductModel();
            string res = "";

            if (IsValidId(ToStr(request.Id.ToString())))
            {
                var Id = ToInt(request.Id);
                product = DbClientFactory<ProductDBClient>.instance.GetProductById(GetConnectionString(), Id);

                if (product != null)
                {
                    product.Id = Id;
                    product.ProductName = request.ProductName ?? product.ProductName;
                }
            }
            else
            {
                product.Id = 0;
                product.ProductName = ToStr(request.ProductName);
            }

            if (product != null)
            {
                res = DbClientFactory<ProductDBClient>.instance.ManageProduct(GetConnectionString(), product);

                if (res == "C201")
                {
                    Message = "Product already exist.";
                    response = BuildResponse(AppConstant.STATUS_FAILED, Count, Message, null, null);
                }
                else
                {
                    Count = 1;
                    if (product.Id == 0)
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