using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeatherAsync(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid=df1065d44cd019f14e7e13b2d0c1b75d");
            var weatherData = JsonConvert.DeserializeObject<Root>(response);
            return weatherData;
        }

        public static async Task<Root> GetWeatherByCityAsync(string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid=df1065d44cd019f14e7e13b2d0c1b75d");
            var weatherData = JsonConvert.DeserializeObject<Root>(response);
            return weatherData;
        }
    }
}
