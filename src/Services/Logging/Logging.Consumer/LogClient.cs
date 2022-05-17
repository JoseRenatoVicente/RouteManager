using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Logging.Consumer
{
    public static class LogClient
    {
        public static async Task Add(LogRequest log)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44309/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                await client.PostAsJsonAsync("/api/v1/Logs", log);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine("Message :{0} ", exception.Message);
            }
        }
    }
}
