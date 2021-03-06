﻿using BuyBackAPI.Models.Master;
using BuyBackAPI.Translator.MasterTranslator;
using BuyBackAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository.MasterDBClient
{
    public class SubCategoryDBClient
    {
        public List<SubCategoryModel> GetAllSubCategories(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<SubCategoryModel>>(connectionString, "getallsubcategory", x => x.TranslateAsSubCategoriesList());
        }

        public SubCategoryModel GetSubCategoryById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<SubCategoryModel>(connectionString, "getsubcategorybyid", x => x.TranslateSubCategory(), sqlParameters);
        }

        public string ManageSubCategory(string connectionString, SubCategoryModel subcategory)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, Int32.MaxValue)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",subcategory.Id),
                new SqlParameter("@catid",subcategory.CategoryId),
                new SqlParameter("@subcategoryname",subcategory.SubCategoryName),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "managesubcategory", parameter);
            return (string)outParam.Value;
        }
    }
}
