using CampaignBudgetOptimizer.Models;

namespace CampaignBudgetOptimizer.Services
{
    public class BudgetCalculator
    {
        public double CalculateOptimalNewAdBudget(CampaignModel model)
        {
            double epsilon = 0.01; // Tolerance for the binary search
            double lowerBound = 0; // Lower bound of the binary search
            double upperBound = model.TotalAdSpend; // Upper bound of the binary search

            while (upperBound - lowerBound > epsilon)
            {
                double mid = (lowerBound + upperBound) / 2.0;

                // Calculate total ad spend including the new ad
                double totalAdSpend = model.AdBudgets.Values.Sum() + mid;

                // Calculate total cost including fees and fixed costs
                double totalCost = totalAdSpend + (model.AgencyFeePercentage * totalAdSpend)
                                   + (model.ToolFeePercentage * totalAdSpend)
                                   + model.AgencyHours;

                if (totalCost > model.TotalAdSpend)
                {
                    upperBound = mid; // If total cost exceeds the budget, decrease the budget
                }
                else
                {
                    lowerBound = mid; // Otherwise, increase the budget
                }
            }

            return (lowerBound + upperBound) / 2.0; // Return the optimized budget for the new ad
        }
    }
}
