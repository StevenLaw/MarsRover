using MarsRoverApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MarsRoverApi.Controllers
{
    public class RoverController : ApiController
    {
        private List<HistoryRecord> _history = new List<HistoryRecord>()
        {
            new HistoryRecord { Command = "Test", Input = true },
            new HistoryRecord { Command = "Test 2", Input = false}
        };

        public List<HistoryRecord> Get()
        {
            return _history;
        }

        public string Post([FromBody] CommandBody body)
        {
            switch (body.Type)
            {
                case CommandType.Move:
                    if (ValidateMoveCommand(body.Command))
                        return $"Move Command: {body.Command}";
                    else
                        return $"Malformed move command: {body.Command}";
                case CommandType.SetupPlateau:
                    if (ValidatePlateauCommand(body.Command, out int xPlateau, out int y))
                        return $"Plateau Command: ({xPlateau}, {y})";
                    else
                        return $"Malformed Plateau command: {body.Command}";
                case CommandType.SetupRover:
                    if (ValidateRoverCommand(body.Command, out int xRover, out int yRover, out char heading))
                        return $"Rover Command: ({xRover}, {yRover}, {heading})";
                    else
                        return $"Malformed Rover command: {body.Command}";
                default:
                    return $"Bad Command Type: {body.Type}";
            }
        }

        private bool ValidateMoveCommand(string command)
        {
            return command.ToUpper().All(c => "LRM".Contains(c));
        }

        private bool ValidatePlateauCommand(string command, out int x, out int y)
        {
            var split = command.Split(' ');
            if (split.Length == 2 &&
                int.TryParse(split[0], out x) &&
                int.TryParse(split[1], out y))
            {
                return true;
            }
            else
            {
                x = 0;
                y = 0;
                return false;
            }
        }

        private bool ValidateRoverCommand(string command, out int x, out int y, out char h)
        {
            var split = command.Split(' ');
            if (split.Length == 3 &&
                int.TryParse(split[0], out x) &&
                int.TryParse(split[1], out y) &&
                split[2] is string &&
                split[2].Length >= 1 &&
                "NESW".Contains(split[2][0]))
            {
                h = split[2][0];
                return true;
            }
            else
            {
                x = 0;
                y = 0;
                h = 'N';
                return false;
            }
        }
    }
}
