using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models
{
    public class User
    {
        public User()
        {

        }
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Name needed")]
        public string User_name { get; set; }

        [Required(ErrorMessage = "Email needed")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string User_email { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Not a valid Phone number")]
        public string User_contact { get; set; }

        [Required(ErrorMessage = "Username needed")]
        public string User_username { get; set; }

        [Required(ErrorMessage = "Password needed")]
        public string User_password { get; set; }

        [NotMapped]
        [Compare("User_password", ErrorMessage = "Password Dosen't match")]
        public string User_ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Select your gender")]
        public string User_gender { get; set; }

        public string User_type { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Delivery> Deliveries { get; set; }
        public List<Purches> Purcheses { get; set; }


    }
}