using MarsRoverWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MarsRoverWebService.Webservices
{
    /// <summary>
    /// Summary description for RoverService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RoverService : System.Web.Services.WebService
    {
        private List<Record> history = new List<Record>();

        [WebMethod]
        public List<Record> GetHistory()
        {
            return history;
        }

        [WebMethod]
        public string SendCommand(string command)
        {
            return $"This is the command: {command}";
        }

        [WebMethod]
        public string SetupPlateau(int xCoord, int yCoord)
        {
            return $"Setup Plateau: ({xCoord}, {yCoord})";
        }

        [WebMethod]
        public string SetupRover(int xCoord, int yCoord, char heading)
        {
            return $"Setup Rover: ({xCoord}, {yCoord}, {heading})";
        }
    }
}
