using BuyBackAPI.Models.Master;
using BuyBackAPI.Translator.MasterTranslator;
using BuyBackAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository.MasterDBClient
{
    public class CategoryDBClient
    {
        public List<CategoryModel> GetAllCategories(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<CategoryModel>>(connectionString, "getallcategory", x => x.TranslateAsCategoriesList());
        }

        public CategoryModel GetCategoryById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<CategoryModel>(connectionString, "getcategorybyid", x => x.TranslateCategory(), sqlParameters);
        }

        public string ManageCategory(string connectionString, CategoryModel category)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, Int32.MaxValue)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",category.Id),
                new SqlParameter("@categoryname",category.CategoryName),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "managecategory", parameter);
            return (string)outParam.Value;
        }
    }
}
