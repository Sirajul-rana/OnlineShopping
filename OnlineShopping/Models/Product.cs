using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Product
    {
        public Product()
        {
        }
        [Key]
        public int Product_Id { get; set; }
        [Required]
        public String Product_name { get; set; }
        [Required]
        public float Product_price { get; set; }
        [Required]
        public int Product_vat { get; set; }
        [Required]
        public int Product_discount { get; set; }
        [Required]
        public String Product_picture_path { get; set; }
        [Required]
        public int Product_quantity { get; set; }
        [Required]
        public string Product_description { get; set; }


        public int Size_Id { get; set; }

        public int Sub_Category_Id { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Order> Orders { get; set; }
    }
}