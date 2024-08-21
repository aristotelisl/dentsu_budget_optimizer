using CampaignBudgetOptimizer.Models;

namespace CampaignBudgetOptimizer.Services
{
    public class BudgetCalculator
    {
        public double CalculateOptimalAdBudget(CampaignModel model, string adToOptimize)
        {
            double epsilon = 0.01;
            double lowerBound = 0;
            double upperBound = model.TotalAdSpend;

            while (upperBound - lowerBound > epsilon)
            {
                double mid = (lowerBound + upperBound) / 2.0;
                model.Ad1Budget = adToOptimize == "Ad1" ? mid : model.Ad1Budget;
                model.Ad2Budget = adToOptimize == "Ad2" ? mid : model.Ad2Budget;
                model.Ad3Budget = adToOptimize == "Ad3" ? mid : model.Ad3Budget;

                double totalAdSpend = model.Ad1Budget + model.Ad2Budget + model.Ad3Budget;
                double totalCost = totalAdSpend + (model.AgencyFeePercentage * totalAdSpend)
                                   + (model.ToolFeePercentage * (model.Ad1Budget + model.Ad2Budget + model.Ad3Budget))
                                   + model.AgencyHours;

                if (totalCost > model.TotalAdSpend)
                {
                    upperBound = mid;
                }
                else
                {
                    lowerBound = mid;
                }
            }

            return (lowerBound + upperBound) / 2.0;
        }
    }
}