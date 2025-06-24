using NVPCalculatorApi.Models;

namespace NVPCalculatorApi.Services.Interfaces
{
    public interface INpvCalculatorService
    {
        Task<List<NpvResultDto>> CalculateNpv(NpvInputDto request);
    }
}
