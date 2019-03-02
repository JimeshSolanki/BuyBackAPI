using BuyBackAPI.Models.Master;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Translator.MasterTranslator
{
    public static class SubCategoryTranslator
    {
        public static SubCategoryModel TranslateSubCategory(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
            }

            var subCategory = new SubCategoryModel();

            if (reader.IsColumnExists("id"))
            {
                subCategory.Id = SqlHelper.GetNullableInt32(reader, "id");
            }

            if (reader.IsColumnExists("catid"))
            {
                subCategory.CategoryId = SqlHelper.GetNullableInt32(reader, "catid");
            }

            if (reader.IsColumnExists("subcategoryname"))
            {
                subCategory.SubCategoryName = SqlHelper.GetNullableString(reader, "subcategoryname");
            }

            return subCategory;
        }

        public static List<SubCategoryModel> TranslateAsSubCategoriesList(this SqlDataReader reader)
        {
            var list = new List<SubCategoryModel>();
            while (reader.Read())
            {
                list.Add(TranslateSubCategory(reader, true));
            }
            return list;
        }
    }
}
