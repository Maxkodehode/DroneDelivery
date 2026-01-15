using System.Text.Json;
using Spectre.Console;

namespace DroneDelivery
{
    public class WeatherResponse
    {
        public List<WeatherObservation>? weatherObservations { get; set; }
    }

    public class WeatherObservation
    {
        public string? stationName { get; set; }
        public string? temperature { get; set; }
        public string? windSpeed { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Checkpoint
    {
        public string? Name { get; set; }

        public double Wind { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        //Returns true if the weather is good
        public bool IsSafe => Wind < 12;
    }

    public class ControlTower
    {
        public async Task<List<Checkpoint>> GetDroneRoute()
        {
            var rawObservations = await GetCleanData();

            List<Checkpoint> route = new List<Checkpoint>();

            foreach (var obs in rawObservations)
            {
                route.Add(
                    new Checkpoint
                    {
                        Name = obs.stationName,
                        Wind = double.TryParse(obs.windSpeed, out var w) ? w : 0,
                        Lat = obs.lat,
                        Lng = obs.lng,
                    }
                );
            }

            return route;
        }

        private async Task<string> FetchRawWeatherData()
        {
            using var client = new HttpClient();

            // Random latitude variable
            double randomLat = new Random().NextDouble() * (45.0 - 35.0) + 35.0;

            string url =
                $"http://api.geonames.org/weatherJSON?north={randomLat + 2}&south={randomLat - 2}&east=-22.4&west=55.2&username=Maxkodehode";
            client.Timeout = TimeSpan.FromSeconds(5);

            try
            {
                return await client.GetStringAsync(url);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]Tower Connection Error: {e.Message}[/]");
                return "{\"weatherObservations\": []}";
            }
        }

        public async Task<List<WeatherObservation>> GetCleanData()
        {
            string rawJson = await FetchRawWeatherData();

            var result = JsonSerializer.Deserialize<WeatherResponse>(rawJson);

            return result?.weatherObservations ?? new List<WeatherObservation>();
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Radius of the earth in km
            double dLat = (lat2 - lat1) * (Math.PI / 180);
            double dLon = (lon2 - lon1) * (Math.PI / 180);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                + Math.Cos(lat1 * (Math.PI / 180))
                    * Math.Cos(lat2 * (Math.PI / 180))
                    * Math.Sin(dLon / 2)
                    * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in km
        }
    }
}
