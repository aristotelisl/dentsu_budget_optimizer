using CampaignBudgetOptimizer.Models;
using CampaignBudgetOptimizer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CampaignBudgetOptimizer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BudgetCalculator _budgetCalculator;

        public IndexModel(BudgetCalculator budgetCalculator)
        {
            _budgetCalculator = budgetCalculator;
            Ads = new List<string>(); // Initialize the list of ads
        }

        [BindProperty]
        public CampaignModel Campaign { get; set; } = new CampaignModel(); // Initialize the Campaign model

        public List<string> Ads { get; set; } // List to hold the names of ads

        public void OnGet()
        {
            // Initialize default values for testing, you can remove or modify this
            Campaign = new CampaignModel
            {
                TotalAdSpend = 1000,
                AgencyFeePercentage = 0.1,
                ToolFeePercentage = 0.05,
                AgencyHours = 100
            };

            // Example ads for initialization, you can remove or modify this
            Campaign.AdBudgets["Ad1"] = 200;
            Campaign.AdBudgets["Ad2"] = 300;
            Campaign.AdBudgets["Ad3"] = 100;

            Ads = new List<string>(Campaign.AdBudgets.Keys); // Initialize the ads list based on Campaign model
        }

        public void OnPost()
        {
            // Update Ads list based on Campaign model ad budgets
            Ads = new List<string>(Campaign.AdBudgets.Keys);

            if (Campaign != null)
            {
                double optimalBudget = _budgetCalculator.CalculateOptimalNewAdBudget(Campaign);
                ViewData["OptimalBudget"] = optimalBudget;
            }
            else
            {
                // Error handling if necessary inputs are missing
                ViewData["OptimalBudget"] = "Error: Please provide all required fields.";
            }
        }
    }
}
