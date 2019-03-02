using BuyBackAPI.Models.Master;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Translator.MasterTranslator
{
    public static class CategoryTranslator
    {
        public static CategoryModel TranslateCategory(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
            }
            var category = new CategoryModel();

            if (reader.IsColumnExists("id"))
            {
                category.Id = SqlHelper.GetNullableInt32(reader, "id");
            }

            if (reader.IsColumnExists("categoryname"))
            {
                category.CategoryName = SqlHelper.GetNullableString(reader, "categoryname");
            }

            return category;
        }

        public static List<CategoryModel> TranslateAsCategoriesList(this SqlDataReader reader)
        {
            var list = new List<CategoryModel>();
            while (reader.Read())
            {
                list.Add(TranslateCategory(reader, true));
            }
            return list;
        }
    }
}
