using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class User
    {
        public User()
        {

        }
        [Key]
        public int User_Id { get; set; }
        public String User_name { get; set; }
        public String User_email { get; set; }
        public String User_contact { get; set; }
        public String User_password { get; set; }
        public String User_gender { get; set; }
        public String User_type { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Delivery> Deliveries { get; set; }
        public List<Purches> Purcheses { get; set; }


    }
}