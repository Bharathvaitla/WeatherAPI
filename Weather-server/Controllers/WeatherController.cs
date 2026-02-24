using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weather_server.Services;
namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        [Authorize]
        [HttpGet("{city}")]
        public async Task<IActionResult> Weather(string city)
        {
            try
            {
                var httpClient = new HttpClient();
                var weatherService = new WeatherService(httpClient);

                var data = await weatherService.GetWeatherAsync(city);
                return Ok(data);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
