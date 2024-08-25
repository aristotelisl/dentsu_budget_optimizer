using CampaignBudgetOptimizer.Models;
using CampaignBudgetOptimizer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampaignBudgetOptimizer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BudgetCalculator _budgetCalculator;

        public IndexModel(BudgetCalculator budgetCalculator)
        {
            _budgetCalculator = budgetCalculator;
        }

        [BindProperty]
        public CampaignModel? Campaign { get; set; } // Note the nullable type

        [BindProperty]
        public string? AdToOptimize { get; set; } // Note the nullable type

        public void OnGet()
        {
            // Initialize default values if needed
            Campaign = new CampaignModel(); // Initialize Campaign
        }

        public void OnPost()
        {
            if (Campaign != null && AdToOptimize != null)
            {
                double optimalBudget = _budgetCalculator.CalculateOptimalAdBudget(Campaign, AdToOptimize);
                ViewData["OptimalBudget"] = optimalBudget;
            }
            else
            {
                // Handle the case where Campaign or AdToOptimize is null
                ViewData["OptimalBudget"] = "Error: Please provide all required fields.";
            }
        }
    }
}
