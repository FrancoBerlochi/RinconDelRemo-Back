using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeatherAsync();
        Task<List<ForecastDayDto>> GetWeeklyForecastAsync();
    }
}
