using NVPCalculatorApi.Models;
using NVPCalculatorApi.Services.Implementations;
using NVPCalculatorApi.Services.Interfaces;

namespace NVPCalculatorApi.Tests
{
    public class NpvCalculatorServiceTests
    {
        private readonly INpvCalculatorService _service;

        public NpvCalculatorServiceTests()
        {
            _service = new NpvCalculatorService();
        }

        [Fact]
        public async Task CalculateNpvAsync_ReturnsCorrectResults()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double> { -1000, 300, 420, 680 },
                //new List<double> { 1000, 1000, 1000 },
                LowerBoundDiscountRate = 1.0,
                UpperBoundDiscountRate = 2.0,
                DiscountRateIncrement = 1.0
            };

            var result = await _service.CalculateNpv(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1.0, result[0].DiscountRate);
            Assert.Equal(2.0, result[1].DiscountRate);
        }

        [Fact]
        public async Task CalculateNpvAsync_ZeroCashFlows_ReturnsZeroNpvs()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double> { 0, 0, 0 },
                LowerBoundDiscountRate = 1.0,
                UpperBoundDiscountRate = 2.0,
                DiscountRateIncrement = 1.0
            };

            var results = await _service.CalculateNpv(request);

            foreach (var result in results)
            {
                Assert.Equal(0.0, result.Npv);
            }
        }

        [Fact]
        public async Task CalculateNpvAsync_NegativeCashFlows_ReturnsNegativeNpvs()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double> { -1000, -500, -300 },
                LowerBoundDiscountRate = 5.0,
                UpperBoundDiscountRate = 5.0,
                DiscountRateIncrement = 0.5
            };

            var results = await _service.CalculateNpv(request);

            Assert.Single(results);
            Assert.True(results[0].Npv < 0);
        }

        [Fact]
        public async Task CalculateNpvAsync_HandlesSingleDiscountRate()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double> { 500, 500 },
                LowerBoundDiscountRate = 10.0,
                UpperBoundDiscountRate = 10.0,
                DiscountRateIncrement = 0.25
            };

            var results = await _service.CalculateNpv(request);

            Assert.Single(results);
            Assert.Equal(10.0, results[0].DiscountRate);
        }

        [Fact]
        public async Task CalculateNpvAsync_SmallIncrement_ProducesManyResults()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double> { 1000, 1000 },
                LowerBoundDiscountRate = 1.0,
                UpperBoundDiscountRate = 2.0,
                DiscountRateIncrement = 0.1
            };

            var results = await _service.CalculateNpv(request);

            Assert.Equal(10, results.Count); // Rates: 1.0, 1.1, ..., 2.0
        }

        [Fact]
        public async Task CalculateNpvAsync_EmptyCashFlows_ReturnsEmptyResult()
        {
            var request = new NpvInputDto
            {
                CashFlows = new List<double>(), // No cash flows
                LowerBoundDiscountRate = 1.0,
                UpperBoundDiscountRate = 2.0,
                DiscountRateIncrement = 0.5
            };

            var results = await _service.CalculateNpv(request);

            foreach (var result in results)
            {
                Assert.Equal(0.0, result.Npv);
            }
        }

    }
}