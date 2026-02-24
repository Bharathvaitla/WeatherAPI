using System.Text.Json;
using System.Data.SqlClient;
namespace Weather_server.Services
{
    public class WeatherService
    {
        private HttpClient _httpClient;
        private readonly string _connectionString = "Server=localhost;Database=WeatherDB;Integrated Security=True;";
        public WeatherService(HttpClient hC)
        {
            _httpClient = hC;
        }

        public async Task<WeatherDTO> GetWeatherAsync(string loc)
        {
            var response = await _httpClient.GetAsync($"https://api.weatherapi.com/v1/current.json?q={loc}&key=df1745f8c6cc4466bf545635232304");
            string Json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(Json);

            var location = doc.RootElement.GetProperty("location");
            var current = doc.RootElement.GetProperty("current");

            string city = location.GetProperty("name").GetString();
            double lat = location.GetProperty("lat").GetDouble();
            double lon = location.GetProperty("lon").GetDouble();
            double temp = current.GetProperty("temp_c").GetDouble();
            var date = DateTime.Now;
            // create DTO
            var weatherDto = new WeatherDTO(city, temp, lat, lon,date);

            await SaveWeatherAsync(weatherDto); 

            return weatherDto;
        }
        public async Task SaveWeatherAsync(WeatherDTO weather)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO Weather (City, Temp, Latitude, Longitude, dateTime)
                                 VALUES (@City, @Temp, @Latitude, @Longitude, @dateTime)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@City", weather.City);
                    cmd.Parameters.AddWithValue("@Temp", weather.Temp);
                    cmd.Parameters.AddWithValue("@Latitude", weather.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", weather.Longitude);
                    cmd.Parameters.AddWithValue("@dateTime", weather.dateTime);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<WeatherDTO>> GetWeatherHistoryAsync()
        {
            var weatherList = new List<WeatherDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT City, Temp, Latitude, Longitude, dateTime FROM Weather";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string city = reader.GetString(0);
                        double temp = reader.GetDouble(1);
                        double lat = reader.GetDouble(2);
                        double lon = reader.GetDouble(3);
                        DateTime date = reader.GetDateTime(4);

                        weatherList.Add(new WeatherDTO(city, temp, lat, lon, date));
                    }
                }
            }
            return weatherList;
        }

    }
}
