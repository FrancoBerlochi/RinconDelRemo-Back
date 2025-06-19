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
            Console.WriteLine($"API KEY cargada: {_apiKey ?? "NULL"}");
        }

        public async Task<WeatherDto> GetWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("La ciudad no puede estar vacía");

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=es";
            Console.WriteLine($"URL para OpenWeather: {url}");

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error OpenWeather: {response.StatusCode} - {errorContent}");
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
                Temperature = weatherData.Main.Temp
            };
        }

        private class MainInfo
        {
            public float Temp { get; set; }
        }

        private class WeatherInfo
        {
            public string Description { get; set; } = string.Empty;
        }

        private class OpenWeatherResponse
        {
            public string Name { get; set; } = string.Empty;
            public MainInfo Main { get; set; } = new MainInfo();
            public List<WeatherInfo> Weather { get; set; } = new List<WeatherInfo>();
        }

        
    }
}
