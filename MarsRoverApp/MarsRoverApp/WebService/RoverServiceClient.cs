using MarsRoverApiModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarsRoverApp.WebService
{
    public class RoverServiceClient
    {
        static string baseUri = "http://localhost:10602/api/rover/";

        public RoverServiceClient()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                baseUri = baseUri.Replace("localhost", "10.0.2.2");
            }
        }

        public async Task<List<HistoryRecord>> GetHistoryAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(baseUri);
                return JsonConvert.DeserializeObject<List<HistoryRecord>>(response);
            }
        }

        public async Task<string> SetupPlateauAsync(int x, int y)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        new CommandBody { Type = CommandType.SetupPlateau, Command = $"{x} {y}" }), 
                    Encoding.UTF8, 
                    "application/json");
                var response = await client.PostAsync(baseUri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task <string> SetupRoverAsync(int x, int y, char heading)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        new CommandBody { Type = CommandType.SetupRover, Command = $"{x} {y} {heading}" }),
                    Encoding.UTF8,
                    "application/json");
                var response = await client.PostAsync(baseUri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> MoveAsync(string command)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        new CommandBody { Type = CommandType.Move, Command = command }),
                    Encoding.UTF8,
                    "application/json");
                var response = await client.PostAsync(baseUri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public void UploadImage(string base64)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        new CommandBody { Type = CommandType.UploadImage, Command = base64 }),
                    Encoding.UTF8,
                    "application/json");
                client.PutAsync(baseUri, content);
            }
        }
    }
}
