using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Order
    {
        public Order()
        {
            
        }
        [Key]
        public int Order_Id { get; set; }
        public int Quantity { get; set; }

        public int Product_Id { get; set; }
        public int Purches_Id { get; set; }
        
    }
}