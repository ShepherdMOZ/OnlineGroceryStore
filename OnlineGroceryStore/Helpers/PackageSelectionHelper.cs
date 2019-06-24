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



            PackingBreakdownRunner(packages, orderQuantity, new List<InventoryPackingConfigure> (), new List<PackBreakdownViewModel>(), ref bestBreakDowns);

            return bestBreakDowns;
        }

        // Algorithm recursion runner
        private static void PackingBreakdownRunner(List<InventoryPackingConfigure> packConfig, int orderQuantity, List<InventoryPackingConfigure> usedConfig, 
             List<PackBreakdownViewModel> breakDowns, ref List<PackBreakdownViewModel> bestBreakDowns)
        {
            // Exit - 1: find a combination that can fulfilled all the [orderQuality]
            if (orderQuantity <= 0 || PackVolumeOvershoot(orderQuantity,packConfig))
            {
                // check if this configure is the best one 
                ComparePackingConfiguration(ref bestBreakDowns, breakDowns);
                return;
            } else if (usedConfig.Count == packConfig.Count)
            {
                
            }


            // 1. Iterating through all packs
            // 2. Choose one of packs, select the maximum quantity of packs for given condition
            for (var i = 0; i<packConfig.Count; i++)
            {
                if (usedConfig.Contains(packConfig[i])){
                    continue;
                }
                // use the maximum amount of the pack
                int packItemNum = packConfig[i].packSize;
                int packCount = orderQuantity / packItemNum;
                int remainQuantity = orderQuantity % packItemNum;

                // If selectable, this is added to the current selection
                if ( packCount > 0)
                {
                    var newPack = new PackBreakdownViewModel{
                        packQuantity = packCount,
                        packingID = packConfig[i].packingID,
                        inventoryPackingConfigure = packConfig[i],
                        
                    };
                    breakDowns.Add(newPack);
                    usedConfig.Add(packConfig[i]);
                    PackingBreakdownRunner(packConfig, remainQuantity,usedConfig, breakDowns, ref bestBreakDowns);
                    usedConfig.Remove(packConfig[i]);
                    breakDowns.RemoveAt(breakDowns.Count - 1);
                }
            }
        }

        private static bool PackVolumeOvershoot(int orderQuantity, List<InventoryPackingConfigure> packConfig)
        {
            bool overshoot = true;

            foreach (var config in packConfig)
            {
                if (orderQuantity >= config.packSize)
                {
                    return false;
                }
            }

            return overshoot;
        }

        private static void ComparePackingConfiguration(ref List<PackBreakdownViewModel> bestBreakDowns,
            List<PackBreakdownViewModel> newBreakDowns)
        {
            // Replace if new breakdowns covered more item
            if (GetBreakDownSums(newBreakDowns).Item1 >= GetBreakDownSums(bestBreakDowns).Item1)
            {
                bestBreakDowns = newBreakDowns.ToList();
                // Replace breakdown if cost if cheaper or current best in null

            }
            else if (GetBreakDownSums(newBreakDowns).Item1 == GetBreakDownSums(bestBreakDowns).Item1)
            {
                if (GetBreakDownSums(newBreakDowns).Item2 > 0 && GetBreakDownSums(newBreakDowns).Item2 < GetBreakDownSums(bestBreakDowns).Item2)
                {
                    bestBreakDowns = newBreakDowns.ToList();
                }

            }
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
