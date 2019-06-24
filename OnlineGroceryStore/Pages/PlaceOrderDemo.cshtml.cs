using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineGroceryStore.Models;

namespace OnlineGroceryStore.Pages
{
    public class PlaceOrderDemoModel : PageModel
    {
        private readonly OnlineGroceryStore.Models.OnlineGroceryStoreContext _context;

        public PlaceOrderDemoModel(OnlineGroceryStore.Models.OnlineGroceryStoreContext context)
        {
            _context = context;
        }

        public IList<Inventory> Inventories { get; set; }

        public IList<InventoryPackingConfigure> InventoryPackingConfigure { get;set; }

        public async Task OnGetAsync()
        {
            Inventories = await _context.Inventory
                .Include(i => i.packs).ToListAsync();
        }
    }
}
