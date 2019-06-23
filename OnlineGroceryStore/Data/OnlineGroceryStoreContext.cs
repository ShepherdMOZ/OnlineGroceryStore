using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGroceryStore.Models;

namespace OnlineGroceryStore.Models
{
    public class OnlineGroceryStoreContext : DbContext
    {
        public OnlineGroceryStoreContext (DbContextOptions<OnlineGroceryStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryPackingConfigure> InventoryPackingConfigure { get; set; }
    }
}
