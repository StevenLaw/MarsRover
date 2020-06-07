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
        private const int PLATEAU_LOWER_X = 0;
        private const int PLATEAU_LOWER_Y = 0;
        private int plateauUpperX = 0;
        private int plateauUpperY = 0;

        private RoverPosition roverPosition = new RoverPosition();

        private List<HistoryRecord> history = new List<HistoryRecord>();

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
            plateauUpperX = 0;
            plateauUpperY = 0;
            roverPosition = new RoverPosition();
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
            command = command.ToUpper();
            foreach(char c in command)
            {
                switch(c)
                {
                    case 'L':
                        Turn(left: true);
                        break;
                    case 'R':
                        Turn(left: false);
                        break;
                    case 'M':
                        Move();
                        break;
                }
            }
            history.Add(new HistoryRecord
            {
                Command = command,
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = roverPosition.ToString(),
                Input = false
            });
            return roverPosition.ToString();
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        private void Move()
        {
            switch (roverPosition.Heading)
            {
                case 'N':
                    if (plateauUpperY > roverPosition.Y)
                        roverPosition.Y++;
                    break;
                case 'E':
                    if (plateauUpperX > roverPosition.X)
                        roverPosition.X++;
                    break;
                case 'S':
                    if (PLATEAU_LOWER_Y < roverPosition.Y)
                        roverPosition.Y--;
                    break;
                case 'W':
                    if (PLATEAU_LOWER_X < roverPosition.X)
                        roverPosition.X--;
                    break;
            }
        }

        /// <summary>
        /// Turns.
        /// </summary>
        /// <param name="left">if set to <c>true</c> turn left otherwise turn right.</param>
        private void Turn(bool left)
        {
            switch (roverPosition.Heading)
            {
                case 'N':
                    if (left)
                        roverPosition.Heading = 'W';
                    else
                        roverPosition.Heading = 'E';
                    break;
                case 'E':
                    if (left)
                        roverPosition.Heading = 'N';
                    else
                        roverPosition.Heading = 'S';
                    break;
                case 'S':
                    if (left)
                        roverPosition.Heading = 'E';
                    else
                        roverPosition.Heading = 'W';
                    break;
                case 'W':
                    if (left)
                        roverPosition.Heading = 'S';
                    else
                        roverPosition.Heading = 'N';
                    break;
            }
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
            plateauUpperX = x;
            plateauUpperY = y;

            string result = $"{PLATEAU_LOWER_X} {PLATEAU_LOWER_Y} -> {plateauUpperX} {plateauUpperY}";

            history.Add(new HistoryRecord
            {
                Command = $"{x} {y}",
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = result,
                Input = false
            });

            return $"{PLATEAU_LOWER_X} {PLATEAU_LOWER_Y} -> {plateauUpperX} {plateauUpperY}";
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
            roverPosition.X = x;
            roverPosition.Y = y;
            roverPosition.Heading = heading;

            history.Add(new HistoryRecord
            {
                Command = roverPosition.ToString(),
                Input = true
            });
            history.Add(new HistoryRecord
            {
                Command = roverPosition.ToString(),
                Input = false
            });

            return roverPosition.ToString();
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
