using BuyBackAPI.Models.Master;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Translator.Master
{
    public static class ProductTranslator
    {
        public static ProductModel TranslateProduct(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
            }
            var product = new ProductModel();

            if (reader.IsColumnExists("id"))
            {
                product.Id = SqlHelper.GetNullableInt32(reader, "id");
            }

            if (reader.IsColumnExists("productname"))
            {
                product.ProductName = SqlHelper.GetNullableString(reader, "productname");
            }

            if (reader.IsColumnExists("description"))
            {
                product.Description = SqlHelper.GetNullableString(reader, "description");
            }

            if (reader.IsColumnExists("image"))
            {
                product.ImageName = SqlHelper.GetNullableString(reader, "image");
            }

            if (reader.IsColumnExists("price"))
            {
                product.Price = SqlHelper.GetNullableDecimal(reader, "price");
            }

            if (reader.IsColumnExists("categoryname"))
            {
                product.CategoryName = SqlHelper.GetNullableString(reader, "categoryname");
            }

            if (reader.IsColumnExists("subcategoryname"))
            {
                product.CategoryName = SqlHelper.GetNullableString(reader, "subcategoryname");
            }

            return product;
        }

        public static List<ProductModel> TranslateAsProductsList(this SqlDataReader reader)
        {
            var list = new List<ProductModel>();
            while (reader.Read())
            {
                list.Add(TranslateProduct(reader, true));
            }
            return list;
        }
    }
}
