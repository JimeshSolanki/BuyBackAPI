using BuyBackAPI.Models.Master;
using BuyBackAPI.Translator.MasterTranslator;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository.MasterDBClient
{
    public class SubCategoryDBClient
    {
        public List<SubCategoryModel> GetAllCategories(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<SubCategoryModel>>(connectionString, "GetAllSubCategories", x => x.TranslateAsSubCategoriesList());
        }

        public SubCategoryModel GetCategoryById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<SubCategoryModel>(connectionString, "GetSubCategoryById", x => x.TranslateSubCategory(), sqlParameters);
        }

        public string ManageCategory(string connectionString, SubCategoryModel subcategory)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, 20)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",subcategory.Id),
                new SqlParameter("@catid",subcategory.CategoryId),
                new SqlParameter("@name",subcategory.SubCategoryName),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "SaveSubCategory", parameter);
            return (string)outParam.Value;
        }
    }
}
