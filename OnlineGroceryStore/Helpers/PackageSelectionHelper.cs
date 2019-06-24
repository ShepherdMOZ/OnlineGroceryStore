using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGroceryStore.Models;
using OnlineGroceryStore.ViewModels;

namespace OnlineGroceryStore.Helpers
{
    public class PackageSelectionHelper
    {
        /// <summary>
        /// Generate a viable package breaking down for a given item and quantity demand
        /// </summary>
        /// <param name="itemID">The ID of looking items.</param>
        /// <param name="orderQuantity">The total quantity of the order.</param>
        /// <param name="_context">The DBcontext.</param>
        /// <returns>
        /// Return a viable packing breakdown list
        /// </returns>
        /// <remarks>
        /// For use with local systems
        /// </remarks>

        // Algorithm Entrypoint
        public static ICollection<PackBreakdownViewModel> GetPackBreakdown(List<InventoryPackingConfigure> packages, int orderQuantity)
        {

            List<PackBreakdownViewModel> bestBreakDowns = new List<PackBreakdownViewModel> ();

            if (orderQuantity <= 0)
            {
                return null;
            }

            PackingBreakdownRunner(packages, orderQuantity, new List<PackBreakdownViewModel>(), ref bestBreakDowns);

            return bestBreakDowns;
        }

        // Algorithm recursion runner
        private static void PackingBreakdownRunner(List<InventoryPackingConfigure> packConfig, int orderQuantity,
             List<PackBreakdownViewModel> breakDowns, ref List<PackBreakdownViewModel> bestBreakDowns)
        {

            Dictionary<int, int> MinPackSelections = new Dictionary<int, int>();
            MinPackSelections[0] = 0;

            int lastPackPos = 0;
            // 1. Iterating through all packs

            for (var i = 0; i < packConfig.Count; i++)
            {

                // use the maximum amount of the pack
                int packItemNum = packConfig[i].packSize;
                int maxPackCount = orderQuantity / packItemNum;

                // If selectable, this is added to the current selection
                if (maxPackCount > 0)
                {
                    // 2. Choose one of packs, iternating throught all possible pack picks
                    for (var currentQuantity = 1; currentQuantity <= orderQuantity; currentQuantity++)
                    {
                        

                        int lastQuantity = currentQuantity - packItemNum;
                        if (lastQuantity < 0)
                        {
                            continue;
                        }
                        var lastMinSelect = MinPackSelections.Where(x => x.Key == lastQuantity).FirstOrDefault();
                        var currentMinSelect = MinPackSelections.Where(x => x.Key == currentQuantity).FirstOrDefault();


                        if (lastQuantity == 0)
                        {
                            MinPackSelections[currentQuantity] = lastMinSelect.Value + 1;
                        }
                        else if (lastMinSelect.Value > 0 && ( lastMinSelect.Value + 1 < currentMinSelect.Value || currentMinSelect.Value == 0))
                        {
                            MinPackSelections[currentQuantity] = lastMinSelect.Value + 1;
                        }

                        if (currentQuantity == orderQuantity && lastMinSelect.Value >= 0)
                        {
                            lastPackPos = i;
                        }
                    }

                }
            }
            int remainOrder = orderQuantity;
            var breakDown = new PackBreakdownViewModel
            {
                packQuantity = 0,
                packingID = packConfig[lastPackPos].packingID,
                inventoryPackingConfigure = packConfig[lastPackPos]
            };
            for (var i = MinPackSelections[orderQuantity]; i > 0;)
            {
                if (lastPackPos < 0)
                {
                    break;
                }
                var currentPack = packConfig[lastPackPos];
                var prevMinSelect = MinPackSelections.Where(x => x.Key == remainOrder - currentPack.packSize).FirstOrDefault();
                if (remainOrder - currentPack.packSize < 0 || (prevMinSelect.Value <= 0 && remainOrder - currentPack.packSize != 0))
                {
                    lastPackPos -= 1;
                    breakDowns.Add(breakDown);
                    breakDown = new PackBreakdownViewModel
                    {
                        packQuantity = 0,
                        packingID = packConfig[lastPackPos].packingID,
                        inventoryPackingConfigure = packConfig[lastPackPos]
                    };
                    continue;
                }
                breakDown.packQuantity += 1;
                i--;
                remainOrder -= currentPack.packSize;
            }
            if (!breakDowns.Contains(breakDown))
            {
                breakDowns.Add(breakDown);
            }

            bestBreakDowns = breakDowns.ToList();

        }


        public static Tuple<int,double> GetBreakDownSums(ICollection<PackBreakdownViewModel> currentBreakdown)
        {
            int quantity = 0;
            double cost = 0.0;
            foreach (var breakdown in currentBreakdown)
            {
                quantity += breakdown.packQuantity * breakdown.inventoryPackingConfigure.packSize;
                cost += breakdown.inventoryPackingConfigure.packPrice * breakdown.packQuantity;
            }

            return new Tuple<int, double> (quantity, cost);
        }

    }
}
