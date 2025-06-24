namespace NVPCalculatorApi.Models
{
    public class NpvInputDto
    {
        public List<double> CashFlows { get; set; } = new List<double>();
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; } 
    }
}
