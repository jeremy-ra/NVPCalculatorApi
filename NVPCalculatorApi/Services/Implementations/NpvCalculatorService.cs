using NVPCalculatorApi.Models;
using NVPCalculatorApi.Services.Interfaces;

namespace NVPCalculatorApi.Services.Implementations
{
    public class NpvCalculatorService : INpvCalculatorService
    {
        /// <summary>
        /// Returns a list of NPV results based on the provided cash flows and rate parameters.
        /// </summary>
        /// <param name="npvInputDto"></param>
        /// <returns></returns>
        public async Task<List<NpvResultDto>> CalculateNpv(NpvInputDto npvInputDto)
        {
            return await Task.Run(() =>
            {
                var results = new List<NpvResultDto>();

                for (double rate = npvInputDto.LowerBoundDiscountRate; rate <= npvInputDto.UpperBoundDiscountRate; rate += npvInputDto.DiscountRateIncrement)
                {
                    double npv = CalculateNpv(npvInputDto.CashFlows, rate);
                    results.Add(new NpvResultDto
                    {
                        Rate = Math.Round(rate, 2),
                        Npv = Math.Round(npv, 4)
                    });
                }

                return results;
            });
        }

        /// <summary>
        /// Calculates the Net Present Value (NPV) for a given set of cash flows and a discount rate.
        /// </summary>
        /// <param name="cashFlows"></param>
        /// <param name="discountRate"></param>
        /// <returns></returns>
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
