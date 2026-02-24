using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weather_server.Services;

namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserHistoryController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public UserHistoryController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        
        [Authorize]
        [HttpGet("GetWeatherHistory")]
        public async Task<IActionResult> GetWeatherHistory()
        {
            var history = await _weatherService.GetWeatherHistoryAsync();
            return Ok(history);
        }
    }
}
