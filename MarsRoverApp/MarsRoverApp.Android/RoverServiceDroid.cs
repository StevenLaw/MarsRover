using MarsRoverApp.Droid.WebReference;
using MarsRoverApp.WebService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(MarsRoverApp.Droid.RoverServiceDroid))]
namespace MarsRoverApp.Droid
{
    public class RoverServiceDroid : IRoverService
    {
        private readonly RoverService _roverService;

        public string Uri { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public RoverServiceDroid()
        {
            _roverService = new RoverService
            {
                // Prevent emulator from failing to find webservice on localhost
                Url = "http://10.0.2.2:44383/Webservices/RoverService.asmx"
            };
        }

        public Task<List<History>> GetHistory()
        {
            var tcs = new TaskCompletionSource<List<History>>();

            _roverService.GetHistoryCompleted += (_, e) =>
            {
                if (e.Error != null)
                {
                    Success = false;
                    ErrorMessage = e.Error.Message;
                    tcs.TrySetResult(new List<History>());
                }
                else if (e.Cancelled)
                {
                    Success = false;
                    ErrorMessage = "Get History canceled";
                    tcs.TrySetCanceled();
                }
                else
                {
                    Success = true;
                    var list = e.Result.Select(i => new History { Input = i.Input, Command = i.Command }).ToList();
                    tcs.TrySetResult(list);
                }
            };
            _roverService.GetHistoryAsync();

            return tcs.Task;
        }

        public Task<string> SendCommand(string command)
        {
            var tcs = new TaskCompletionSource<string>();

            _roverService.SendCommandCompleted += (_, e) =>
            {
                if (e.Error != null)
                {
                    Success = false;
                    ErrorMessage = e.Error.Message;
                    tcs.TrySetResult("");
                }
                else if (e.Cancelled)
                {
                    Success = false;
                    ErrorMessage = "Send Command canceled";
                    tcs.TrySetCanceled();
                }
                else
                {
                    Success = true;
                    tcs.TrySetResult(e.Result);
                }

            };
            _roverService.SendCommandAsync(command);

            return tcs.Task;
        }

        public Task<string> SetupPlateau(int xCoord, int yCoord)
        {
            var tcs = new TaskCompletionSource<string>();

            _roverService.SetupPlateauCompleted += (_, e) =>
            { 
                if (e.Error != null)
                {
                    Success = false;
                    ErrorMessage = e.Error.Message;
                    tcs.TrySetResult("");
                }
                else if (e.Cancelled)
                {
                    Success = false;
                    ErrorMessage = "Setup Plateau canceled";
                    tcs.TrySetCanceled();
                }
                else
                {
                    Success = true;
                    tcs.TrySetResult(e.Result);
                }
            };
            _roverService.SetupPlateauAsync(xCoord, yCoord);

            return tcs.Task;
        }

        public Task<string> SetupRover(int xCoord, int yCoord, char heading)
        {
            var tcs = new TaskCompletionSource<string>();

            _roverService.SetupRoverCompleted += (_, e) =>
            {
                if (e.Error != null)
                {
                    Success = false;
                    ErrorMessage = e.Error.Message;
                    tcs.TrySetResult("");
                }
                else if (e.Cancelled)
                {
                    Success = false;
                    ErrorMessage = "SetupRover canceled";
                    tcs.TrySetCanceled();
                }
                else
                {
                    Success = true;
                    tcs.TrySetResult(e.Result);
                }
            };
            _roverService.SetupRoverAsync(xCoord, yCoord, heading);

            return tcs.Task;
        }
    }
}