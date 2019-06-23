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

            var packs = PackageSelectionHelper.GetPackBreakdown(item.itemID, _com.quantity, _context);

            var pack_sums = PackageSelectionHelper.GetBreakDownSums(packs);
            packBreakDown.totalPrice = pack_sums.Item2;
            packBreakDown.packBreakdowns = packs;

            return Ok(packBreakDown);
        }
    }

}