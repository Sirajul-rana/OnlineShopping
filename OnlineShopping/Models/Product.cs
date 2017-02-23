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
        [Required(ErrorMessage = "Product name needed")]
        public String Product_name { get; set; }
        [Required(ErrorMessage = "Product price needed")]
        public float Product_price { get; set; }
        [Required(ErrorMessage = "Product vat needed")]
        public int Product_vat { get; set; }
        [Required(ErrorMessage = "Product discount needed")]
        public int Product_discount { get; set; }
        public String Product_picture_path { get; set; }
        [Required(ErrorMessage = "Product quantity needed")]
        public int Product_quantity { get; set; }
        [Required(ErrorMessage = "Product description needed")]
        [DataType(DataType.MultilineText)]
        public string Product_description { get; set; }

        [Required(ErrorMessage = "Nothing is selected")]
        public int Size_Id { get; set; }
        [Required(ErrorMessage = "Nothing is selected")]
        public int Sub_Category_Id { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Order> Orders { get; set; }
    }
}