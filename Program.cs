using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TelegramBot
{
    class Program
    {
        private static readonly string token = "7511970901:AAFKJAWDZZk6cS_6Uy7iDzudGVXSvGq1ZMw";
        private static ITelegramBotClient? client;

        static async Task Main()
        {
            client = new TelegramBotClient(token);
            client.StartReceiving(UpdateHandler, ErrorHandler);

            var bot = await client.GetMeAsync();
            Console.WriteLine($"{bot.FirstName} запущен!");

            await Task.Delay(-1);
        }

        private static async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                var message = update.Message;
                var user = message.From;

                if (message.Text != null)
                {
                    Console.WriteLine($"{user.FirstName} ({user.Id}) написал: {message.Text}");

                    if (message.Text.ToLower().Contains("погода"))
                    {
                        WeatherService weatherService = new WeatherService();
                        string weatherInfo = await weatherService.GetWeatherInfo();
                        await client.SendTextMessageAsync(message.Chat.Id,weatherInfo);
                        return;
                    }    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
