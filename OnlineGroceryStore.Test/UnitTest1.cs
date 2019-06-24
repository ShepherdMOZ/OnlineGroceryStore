using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using OnlineGroceryStore;
using OnlineGroceryStore.Models;
using OnlineGroceryStore.ViewModels;
using OnlineGroceryStore.Helpers;
namespace Tests
{
    /// This is the test cases for the HelperMethod
    public class Tests
    {
        private OnlineGroceryStoreContext _context;

        private List<PackBreakdownViewModel> TestOnePackConfig;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            
            
            try
            {

                var options = new DbContextOptionsBuilder<OnlineGroceryStoreContext>()
                 .UseSqlite(connection)
                 .Options;

                using (var _context = new OnlineGroceryStoreContext(options))
                { 
                    _context.Database.EnsureCreated();
                    InventorySeedData.PopulateData(_context);
                    List<InventoryPackingConfigure> aa = _context.InventoryPackingConfigure.ToList();
                    TestOnePackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 2,
                            packingID = 4,
                            inventoryPackingConfigure = _context.InventoryPackingConfigure.Where(x => x.packingID == 3).First(),
                        });
                    TestOnePackConfig.Add(
                       new PackBreakdownViewModel
                       {
                           packQuantity = 2,
                           packingID = 3,
                           inventoryPackingConfigure = _context.InventoryPackingConfigure.Where(x => x.packingID == 3).First(),
                       });

                    Assert.Equals(PackageSelectionHelper.GetPackBreakdown(2, 28, _context), TestOnePackConfig);
                }


            }
            finally
            {
                connection.Close();
            }        
        }
    }
}