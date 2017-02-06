using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Comment
    {
        public Comment()
        {
            
        }
        [Key]
        public String Description { get; set; }
        public String Rating { get; set; }
        public Product Product_Id { get; set; }
        public User User_Id { get; set; }
    }
}