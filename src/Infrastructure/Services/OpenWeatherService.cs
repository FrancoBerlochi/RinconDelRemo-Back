using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenWeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OpenWeather:ApiKey"];
            //Console.WriteLine($"API KEY cargada: {_apiKey ?? "NULL"}");
        }

        public async Task<WeatherDto> GetWeatherAsync()
        {

            var url = $"https://api.openweathermap.org/data/2.5/weather?q=Rosario&units=metric&lang=es&appid={_apiKey}";
            //Console.WriteLine($"URL para OpenWeather: {url}");

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine($"Error OpenWeather: {response.StatusCode} - {errorContent}");
                throw new Exception($"Error al llamar OpenWeather API: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(content, options);

            if (weatherData == null)
            {
                throw new Exception("Error deserializing weather data");
            }
            return new WeatherDto
            {
                City = weatherData.Name,
                Description = weatherData.Weather.FirstOrDefault()?.Description ?? "No description available",
                Temperature = weatherData.Main.Temp,
                WindSpeed = weatherData.Wind.Speed * 3.6f, // Convertir de m/s a km/h
                minTemperature = weatherData.Main.Temp_Min,
                maxTemperature = weatherData.Main.Temp_Max,
                Weather = weatherData.Weather.Select(w => w.Main).ToList()
            };
        }

        public async Task<List<ForecastDayDto>> GetWeeklyForecastAsync()
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q=Rosario&units=metric&lang=es&appid={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error en API: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(content, options)
                ?? throw new Exception("No se pudo deserializar el pronóstico");

            var agrupados = new Dictionary<string, ForecastDayDto>();

            foreach (var item in forecast.List)
            {
                var fecha = item.Dt_Txt.Split(' ')[0];

                if (!agrupados.ContainsKey(fecha))
                {
                    agrupados[fecha] = new ForecastDayDto
                    {
                        Fecha = fecha,
                        TempMin = item.Main.Temp_Min,
                        TempMax = item.Main.Temp_Max,
                        WeatherMain = item.Weather[0].Main,
                        WeatherDescription = item.Weather[0].Description,
                        WindSpeed = item.Wind.Speed * 3.6f // Convertir de m/s a km/h
                    };
                }
                else
                {
                    agrupados[fecha].TempMin = Math.Min(agrupados[fecha].TempMin, item.Main.Temp_Min);
                    agrupados[fecha].TempMax = Math.Max(agrupados[fecha].TempMax, item.Main.Temp_Max);
                }
            }

            var hoy = DateTime.UtcNow.ToString("yyyy-MM-dd");

            return agrupados.Values
                .Where(d => d.Fecha != hoy)
                .OrderBy(d => d.Fecha)
                .Take(5)
                .ToList();
        }


        private class MainInfo
        {
            public float Temp { get; set; }
            public float Temp_Min { get; set; }
            public float Temp_Max { get; set; }

        }

        private class WindInfo
        {
            public float Speed { get; set; }
        }


        private class WeatherInfo
        {
            public string Description { get; set; } = string.Empty;
            public string Main { get; set; } = string.Empty;
        }

        private class OpenWeatherResponse
        {
            public WindInfo Wind { get; set; } = new WindInfo();
            public MainInfo Main { get; set; } = new MainInfo();
            public List<WeatherInfo> Weather { get; set; } = new List<WeatherInfo>();
            public string Name { get; set; } = string.Empty;
        }

        private class OpenWeatherForecastResponse
        {
            public List<ForecastItem> List { get; set; } = new();
        }

        private class ForecastItem
        {
            public ForecastMain Main { get; set; } = new();
            public List<ForecastWeather> Weather { get; set; } = new();
            public ForecastWind Wind { get; set; } = new();
            public string Dt_Txt { get; set; } = string.Empty;
        }

        private class ForecastMain
        {
            public float Temp_Min { get; set; }
            public float Temp_Max { get; set; }
        }

        private class ForecastWeather
        {
            public string Main { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }

        private class ForecastWind
        {
            public float Speed { get; set; }
        }


    }
}
