namespace Weather_server.Services
{
    public class WeatherDTO
    {
        public string City { get; set; }
        public double Temp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime dateTime { get; set; }
        public WeatherDTO(string city, double temp, double lat, double lon,DateTime date)
        {
            City = city;
            Temp = temp;
            Latitude = lat;
            Longitude = lon;
            dateTime = date;
        }

    }
}
