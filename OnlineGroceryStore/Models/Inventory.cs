using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineGroceryStore.Models
{
    public class Inventory
    {
        [Key]
        public int itemID { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        //related class navigation
        public List<InventoryPackingConfigure> packs { get; set; }

    }

    public class InventoryPackingConfigure {
        [Key]
        public int packingID { get; set; }
        public int packSize { get; set; }
        public double packPrice { get; set; }

        // Related class navigation
        public int itemID { get; set; }
        public Inventory inventory { get; set; }
    }
}
