using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class WeatherService
    {
        private static readonly string apiKey = "b2feb79cb307f3c4f040517310e224cc";
        private static readonly string City = "Berdsk";
        
        public async Task<string> GetWeatherInfo()
        {
            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={apiKey}&units=metric";
            string response = await client.GetStringAsync(url);

            var json = JObject.Parse(response);
            string temp = json["main"]["temp"].ToString();
            string feels = json["main"]["feels_like"].ToString();
            string weather = json["weather"][0]["main"].ToString();

            return $"Погода в Бердске: {temp}°C - {weather} \n Ощущается как: {feels}°C";
        }
    }
}
