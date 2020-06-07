using MarsRoverApp.WebService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.WebService
{
    public interface IRoverService
    {
        string Uri { get; set; }
        bool Success { get; set; }
        string ErrorMessage { get; set; }

        Task<List<History>> GetHistory();
        Task<string> SendCommand(string command);
        Task<string> SetupPlateau(int xCoord, int yCoord);
        Task<string> SetupRover(int xCoord, int yCoord, char heading);
    }
}
