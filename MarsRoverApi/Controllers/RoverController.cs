using MarsRoverApiModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace MarsRoverApi.Controllers
{
    public class RoverController : ApiController
    {
        private static Plateau plateau = new Plateau();
        private static Rover rover = new Rover();

        private static List<HistoryRecord> history = new List<HistoryRecord>();

        /// <summary>
        /// Get route "api/rover"
        /// </summary>
        /// <returns>
        /// the history list.
        /// </returns>
        public List<HistoryRecord> Get()
        {
            return history;
        }

        /// <summary>
        /// Get route "api/rover/{id}"
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// the history item at the index, null if the index is out of range.
        /// </returns>
        public HistoryRecord Get(int index)
        {
            if (index < history.Count)
                return history[index];
            return null;
        }

        /// <summary>
        /// Post route "api/rover"
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>
        /// the result of the commmand sent.
        /// </returns>
        public string Post([FromBody] CommandBody body)
        {
            switch (body.Type)
            {
                case CommandType.Move:
                    if (ValidateMoveCommand(body.Command))
                        return Move(body.Command);
                    else
                        return $"Malformed move command: {body.Command}";
                case CommandType.SetupPlateau:
                    if (ValidatePlateauCommand(body.Command, out int xPlateau, out int yPlateau))
                        return SetupPlateau(xPlateau, yPlateau);
                    else
                        return $"Malformed Plateau command: {body.Command}";
                case CommandType.SetupRover:
                    if (ValidateRoverCommand(body.Command, out int xRover, out int yRover, out char heading))
                        return SetupRover(xRover, yRover, heading);
                    else
                        return $"Malformed Rover command: {body.Command}";
                default:
                    return $"Bad Command Type: {body.Type}";
            }
        }

        /// <summary>
        /// Resets plateau, rover postion, and history.
        /// </summary>
        public void Delete()
        {
            plateau = new Plateau();
            rover = new Rover();
            history.Clear();
        }

        /// <summary>
        /// Moves using the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// the command return value.
        /// </returns>
        private string Move(string command)
        {
            rover.Move(command, plateau);
            history.Add(new HistoryRecord
            {
                Command = command,
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = rover.ToString(),
                Input = false
            });
            return rover.ToString();
        }

        /// <summary>
        /// Setups the plateau.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// the command return value.
        /// </returns>
        private string SetupPlateau(int x, int y)
        {
            plateau = new Plateau(x, y);


            history.Add(new HistoryRecord
            {
                Command = $"{x} {y}",
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = plateau.ToString(),
                Input = false
            });

            return plateau.ToString();
        }

        /// <summary>
        /// Setups the rover.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="heading">The heading.</param>
        /// <returns>
        /// the command return value.
        /// </returns>
        private string SetupRover(int x, int y, char heading)
        {
            rover = new Rover(x, y, heading);

            history.Add(new HistoryRecord
            {
                Command = rover.ToString(),
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = rover.ToString(),
                Input = false
            });

            return rover.ToString();
        }

        /// <summary>
        /// Validates the move command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// true if the command validated.
        /// </returns>
        private bool ValidateMoveCommand(string command)
        {
            return command.ToUpper().All(c => "LRM".Contains(c));
        }

        /// <summary>
        /// Validates the plateau command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// true if the command validated.
        /// </returns>
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

        /// <summary>
        /// Validates the rover command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="h">The h.</param>
        /// <returns>
        /// true if the command validated.
        /// </returns>
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
