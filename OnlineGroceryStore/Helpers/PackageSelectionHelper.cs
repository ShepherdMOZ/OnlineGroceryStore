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
        public static ICollection<PackBreakdownViewModel> GetPackBreakdown(string itemID, int orderQuantity, OnlineGroceryStoreContext _context)
        {

            List<PackBreakdownViewModel> breakDowns = null;

            if (itemID.Length <=0 || orderQuantity <= 0)
            {
                return null;
            }

            var packages = _context.InventoryPackingConfigure
                .Where(x => x.itemID == itemID)
                .Select(x => new Tuple<int, int, double> (x.packingID, x.packSize, x.packPrice))
                .ToList();

            if (!(packages.Count() > 0))
            {
                return null;
            }

            PackingBreakdownRunner(packages, orderQuantity, ref breakDowns);
           

            return breakDowns;


        }

        // Algorithm recursion runner
        private static void PackingBreakdownRunner(List<Tuple<int, int, double>> packConfig, int orderQuantity, 
            ref List<PackBreakdownViewModel> breakDowns, List<Tuple<int, double>> currentBreakdown = null)
        {
            if ( GetCurrentBreakDownSums(currentBreakdown).Item1 >= orderQuantity)
            {
                // Exit: check if this configure is the best one -> ComparePackingConfiguration()

                return;
            }

            // 1. Iterating through all packs
            // 2. Choose one of packs, select the maximum quantity of packs for given condition


        }

        private static Tuple<int,double> GetCurrentBreakDownSums(List<Tuple<int, double>> currentBreakdown)
        {
            int quantity = 0;
            double price = 0.0;
            foreach (var breakdown in currentBreakdown)
            {
                quantity += breakdown.Item1;
                price = breakdown.Item1 * breakdown.Item2;
            }

            return new Tuple<int, double> (quantity, price) ;
        }

    }
}
