﻿using System.Runtime.Serialization;

namespace BuyBackAPI.Models.Master
{
    public class SubCategoryModel
    {
        public int? Id { get; set; }

        public int? CategoryId { get; set; }

        public string SubCategoryName { get; set; }
    }
}
