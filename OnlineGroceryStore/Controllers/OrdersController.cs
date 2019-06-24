using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using OnlineGroceryStore.Models;
using OnlineGroceryStore.ViewModels;
using OnlineGroceryStore.Helpers;
namespace OnlineGroceryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OnlineGroceryStoreContext _context;

        public OrdersController(OnlineGroceryStoreContext context)
        {
            _context = context;
        }

        [HttpPost("getpacks")]
        public async Task<ActionResult<PackQueryOutputViewModel>> GetPacksBreakDown([FromBody] CommandPromptInputViewModel _com)
        {
            var packBreakDown = new PackQueryOutputViewModel();
            
            var item = await _context.Inventory.Where(x => x.itemCode == _com.code).FirstOrDefaultAsync();

            if (item == null){
                return NotFound();
            }

            var packages = _context.InventoryPackingConfigure
            .Where(x => x.itemID == item.itemID)
            .ToList();

            if (!(packages.Count > 0))
            {
                return null;
            }

            var packs = PackageSelectionHelper.GetPackBreakdown(packages, _com.quantity);

            var pack_sums = PackageSelectionHelper.GetBreakDownSums(packs);
            packBreakDown.totalQuantity = pack_sums.Item1;
            packBreakDown.totalPrice = Math.Round(pack_sums.Item2, 2);
            packBreakDown.packBreakdowns = packs;

            return Ok(packBreakDown);
        }
    }

}