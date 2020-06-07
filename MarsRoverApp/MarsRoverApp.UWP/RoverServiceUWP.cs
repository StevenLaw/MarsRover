using MarsRoverApp.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(MarsRoverApp.UWP.RoverServiceUWP))]
namespace MarsRoverApp.UWP
{
    public class RoverServiceUWP : IRoverService
    {
        private readonly MarsRoverService.RoverServiceSoapClient _roverService;

        public string Uri { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public RoverServiceUWP()
        {
            _roverService = new MarsRoverService.RoverServiceSoapClient();
        }

        public async Task<List<History>> GetHistory()
        {
            try
            {
                var result = await _roverService.GetHistoryAsync();
                Success = true;
                var list = result.Select(i => new History { Command = i.Command, Input = i.Input }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
                return new List<History>();
            }
        }

        public async Task<string> SendCommand(string command)
        {
            try
            {
                var result = await _roverService.SendCommandAsync(command);
                Success = true;
                return result;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
                return "";
            }
        }

        public async Task<string> SetupPlateau(int xCoord, int yCoord)
        {
            try
            {
                var result = await _roverService.SetupPlateauAsync(xCoord, yCoord);
                Success = true;
                return result;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
                return "";
            }
        }

        public async Task<string> SetupRover(int xCoord, int yCoord, char heading)
        {
            try
            {
                var result = await _roverService.SetupRoverAsync(xCoord, yCoord, heading);
                Success = true;
                return result;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
                return "";
            }
        }
    }
}
