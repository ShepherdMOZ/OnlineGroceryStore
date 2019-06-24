using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGroceryStore.Models;

namespace OnlineGroceryStore.ViewModels
{
    public class PackQueryOutputViewModel
    {
        public int totalQuantity { get; set; }
        public double totalPrice { get; set; }
        public ICollection<PackBreakdownViewModel> packBreakdowns { get; set; }
    }
    public class PackBreakdownViewModel
    {
        public int packQuantity { get; set; }

        public int packingID { get; set; }
        public virtual InventoryPackingConfigure inventoryPackingConfigure { get; set; }
    }
}
