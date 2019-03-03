using System;

namespace BuyBackAPI.Models.Master
{
    public class ProductModel
    {
        public int? Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int? SubCatId { get; set; }

        public string ImageName { get; set; }

        public Decimal? Price { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }
    }
}
