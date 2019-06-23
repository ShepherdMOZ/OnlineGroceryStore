using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGroceryStore.Models
{
    public static class InventorySeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new OnlineGroceryStoreContext(
                    serviceProvider.GetRequiredService<DbContextOptions<OnlineGroceryStoreContext>>()))
            {
                if (_context.Inventory.Any())
                {
                    return;
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Inventory.AddRange(
                        new Inventory
                        {
                            itemName = "Sliced Ham",
                            itemCode = "SH3",
                            stockLevel = 500,
                        },
                        new Inventory
                        {
                            itemName = "Yoghurt",
                            itemCode = "YT2",
                            stockLevel = 500,
                        },
                        new Inventory
                        {
                            itemName = "Toilet Rolls",
                            itemCode = "TR",
                            stockLevel = 500,
                        }
                    );
                    _context.SaveChanges();

                    var item_sh3 = _context.Inventory.Where(x => x.itemCode == "SH3").First();
                    _context.InventoryPackingConfigure.AddRange(
                        new InventoryPackingConfigure
                        {
                            inventory = item_sh3,
                            packSize = 3,
                            packPrice = 2.99
                        },
                        new InventoryPackingConfigure
                        {
                            inventory = item_sh3,
                            packSize = 5,
                            packPrice = 4.49
                        });

                    _context.SaveChanges();

                    var item_yt2 = _context.Inventory.Where(x => x.itemCode == "YT2").First();
                    _context.InventoryPackingConfigure.AddRange(
                        new InventoryPackingConfigure
                        {
                            inventory = item_yt2,
                            packSize = 4,
                            packPrice = 4.95
                        },
                        new InventoryPackingConfigure
                        {
                            inventory = item_yt2,
                            packSize = 10,
                            packPrice = 9.95
                        },
                        new InventoryPackingConfigure
                        {
                            inventory = item_yt2,
                            packSize = 16,
                            packPrice = 13.95
                        });

                    _context.SaveChanges();


                    var item_tr = _context.Inventory.Where(x => x.itemCode == "TR").First();
                    _context.InventoryPackingConfigure.AddRange(
                        new InventoryPackingConfigure
                        {
                            inventory = item_tr,
                            packSize = 3,
                            packPrice = 2.95
                        },
                        new InventoryPackingConfigure
                        {
                            inventory = item_tr,
                            packSize = 5,
                            packPrice = 4.45
                        },
                        new InventoryPackingConfigure
                        {
                            inventory = item_tr,
                            packSize = 9,
                            packPrice = 7.99
                        });

                    _context.SaveChanges();

                    transaction.Commit();
                }
                


            }
        }
    }
}
