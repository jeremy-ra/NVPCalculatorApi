using Microsoft.AspNetCore.Mvc;
using NVPCalculatorApi.Models;
using NVPCalculatorApi.Services.Interfaces;

namespace NVPCalculatorApi.Controllers
{
    [Route("api/npv")]
    [ApiController]
    public class NpvController : ControllerBase
    {
        private readonly INpvCalculatorService _npvCalculatorService;
        private readonly ILogger<NpvController> _logger;

        public NpvController(INpvCalculatorService npvCalculatorService, ILogger<NpvController> logger)
        {
            _npvCalculatorService = npvCalculatorService;
            _logger = logger;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<List<NpvResultDto>>> Calculate([FromBody] NpvInputDto npvInputDto)
        {
            _logger.LogInformation("NPV input request received.");           

            try
            {
                // Run calculation asynchronously
                var results = await Task.Run(() =>
                    _npvCalculatorService.CalculateNpv(npvInputDto));

                _logger.LogInformation("NPV calculation completed with {Count} results.", results.Count);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating NPV.");               
                return StatusCode(500, "An error occurred while calculating NPV.");
            }
        }

    }
}
