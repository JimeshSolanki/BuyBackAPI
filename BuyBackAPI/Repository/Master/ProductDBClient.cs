using BuyBackAPI.Models.Master;
using BuyBackAPI.Translator.Master;
using BuyBackAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository.Master
{
    public class ProductDBClient
    {
        public List<ProductModel> GetAllProducts(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<ProductModel>>(connectionString, "getallproduct", x => x.TranslateAsProductsList());
        }

        public ProductModel GetProductById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<ProductModel>(connectionString, "getproductbyid", x => x.TranslateProduct(), sqlParameters);
        }

        public string ManageProduct(string connectionString, ProductModel product)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, Int32.MaxValue)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",product.Id),
                new SqlParameter("@productname",product.ProductName),
                new SqlParameter("@description",product.Description),
                new SqlParameter("@subcatid",product.SubCatId),
                new SqlParameter("@imagename",product.ImageName),
                new SqlParameter("@price",product.Price),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "manageproduct", parameter);
            return (string)outParam.Value;
        }
    }
}
