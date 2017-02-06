using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Category
    {
        public Category()
        {
            
        }
        [Key]
        public int Category_Id { get; set; }
        public String Category_name { get; set; }
        public List<Sub_Category> SubCategories { get; set; }

    }
}