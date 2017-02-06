using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Service
    {
        public Service()
        {
            
        }
        [Key]
        public int Service_Id { get; set; }
        public String Service_area { get; set; }
        public float price { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}