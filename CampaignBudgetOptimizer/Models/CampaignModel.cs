namespace CampaignBudgetOptimizer.Models
{
    public class CampaignModel
    {
        public double TotalAdSpend { get; set; } // Total campaign budget Z
        public Dictionary<string, double> AdBudgets { get; set; } = new Dictionary<string, double>(); // Stores ad budgets dynamically
        public double AgencyFeePercentage { get; set; } // Agency fee Y1
        public double ToolFeePercentage { get; set; } // Tool fee Y2
        public double AgencyHours { get; set; } // Fixed cost for agency hours
    }
}