using NVPCalculatorApi.Models;
using NVPCalculatorApi.Services.Interfaces;

namespace NVPCalculatorApi.Services.Implementations
{
    public class NpvCalculatorService : INpvCalculatorService
    {
        public async Task<List<NpvResultDto>> CalculateNpv(NpvInputDto npvInputDto)
        {
            // Validate input, avoid blocking with Task.Run for CPU-bound calculation
            return await Task.Run(() =>
            {
                var results = new List<NpvResultDto>();

                for (double rate = npvInputDto.LowerBoundDiscountRate; rate <= npvInputDto.UpperBoundDiscountRate; rate += npvInputDto.DiscountRateIncrement)
                {
                    double npv = CalculateNpv(npvInputDto.CashFlows, rate);
                    results.Add(new NpvResultDto
                    {
                        DiscountRate = Math.Round(rate, 2),
                        Npv = Math.Round(npv, 4)
                    });
                }

                return results;
            });
        }

        private double CalculateNpv(List<double> cashFlows, double discountRate)
        {
            double npv = 0.0;
            for (int t = 0; t < cashFlows.Count; t++)
            {
                npv += cashFlows[t] / Math.Pow(1 + discountRate, t + 1);
            }
            return npv;
        }
    }
}
