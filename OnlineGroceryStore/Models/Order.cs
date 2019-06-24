using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineGroceryStore.Models
{
    public class Order
    {
        [Key]
        public string orderID { get; set; }
        public ICollection<OrderItem> items { get; set; }
    }

    public class OrderItem
    {
        [Key]
        public int recordID { get; set; }
        public int quantity { get; set; }

        //Related Tables

        public int itemID { get; set; }
        public virtual Inventory inventory { get; set; }

        public string orderID { get; set; }
        public virtual Order order { get; set; }
    }
}
