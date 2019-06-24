using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGroceryStore.Helpers;
using OnlineGroceryStore.Models;
using OnlineGroceryStore.ViewModels;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private Inventory _inventory;
        private List<InventoryPackingConfigure> _inventoryPackingConfigure;
        private ICollection<PackBreakdownViewModel> _testPackConfig;


        [TestMethod]
        public void TestMethod1()
        {
            _inventory = new Inventory
            {
                itemName = "Sliced Ham",
                itemCode = "SH3",
            };

            _inventoryPackingConfigure = new List<InventoryPackingConfigure>();
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 1, 
                inventory = _inventory,
                packSize = 3,
                packPrice = 2.99
            });
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 2,
                inventory = _inventory,
                packSize = 5,
                packPrice = 4.49
            });
            _testPackConfig = new List<PackBreakdownViewModel>();
            _testPackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 2,
                            packingID = 2,
                            inventoryPackingConfigure = _inventoryPackingConfigure.Where(x => x.packingID == 2).First(),
                        });
           var result = PackageSelectionHelper.GetPackBreakdown(_inventoryPackingConfigure, 10);
           Assert.AreEqual(PackageSelectionHelper.GetBreakDownSums(result), PackageSelectionHelper.GetBreakDownSums(_testPackConfig));

        }

        [TestMethod]
        public void TestMethod2()
        {
            _inventory = new Inventory
            {
                itemName = "Yoghurt",
                itemCode = "YT2",
            };

            _inventoryPackingConfigure = new List<InventoryPackingConfigure>();
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 1,
                inventory = _inventory,
                packSize = 4,
                packPrice = 4.95
            });
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 2,
                inventory = _inventory,
                packSize = 10,
                packPrice = 9.95
            });
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 2,
                inventory = _inventory,
                packSize = 15,
                packPrice = 13.95
            });
            _testPackConfig = new List<PackBreakdownViewModel>();
            _testPackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 2,
                            packingID = 2,
                            inventoryPackingConfigure = _inventoryPackingConfigure.Where(x => x.packingID == 2).First(),
                        });
            _testPackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 2,
                            packingID = 2,
                            inventoryPackingConfigure = _inventoryPackingConfigure.Where(x => x.packingID == 1).First(),
                        });
            var result = PackageSelectionHelper.GetPackBreakdown(_inventoryPackingConfigure, 28);
            Assert.AreEqual(PackageSelectionHelper.GetBreakDownSums(result), PackageSelectionHelper.GetBreakDownSums(_testPackConfig));

        }

        [TestMethod]
        public void TestMethod3()
        {
            _inventory = new Inventory
            {
                itemName = "Toilet Rolls",
                itemCode = "TR",
            };

            _inventoryPackingConfigure = new List<InventoryPackingConfigure>();
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 1,
                inventory = _inventory,
                packSize = 3,
                packPrice = 2.95
            });
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 2,
                inventory = _inventory,
                packSize = 5,
                packPrice = 4.45
            });
            _inventoryPackingConfigure.Add(new InventoryPackingConfigure
            {
                packingID = 3,
                inventory = _inventory,
                packSize = 9,
                packPrice = 7.99
            });
            _testPackConfig = new List<PackBreakdownViewModel>();
            _testPackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 1,
                            packingID = 3,
                            inventoryPackingConfigure = _inventoryPackingConfigure.Where(x => x.packingID == 3).First(),
                        });
            _testPackConfig.Add(
                        new PackBreakdownViewModel
                        {
                            packQuantity = 1,
                            packingID = 1,
                            inventoryPackingConfigure = _inventoryPackingConfigure.Where(x => x.packingID == 1).First(),
                        });
            var result = PackageSelectionHelper.GetPackBreakdown(_inventoryPackingConfigure, 12);
            Assert.AreEqual(PackageSelectionHelper.GetBreakDownSums(result), PackageSelectionHelper.GetBreakDownSums(_testPackConfig));

        }
    }
}
