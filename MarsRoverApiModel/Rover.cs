using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverApiModel
{
    public class Rover
    {
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;
        public char Heading { get; private set; } = 'N';
        public List<Tuple<int, int>> Path { get; private set; }

        public Rover() 
        {
            Path = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(X, Y)
            };
        }

        public Rover(int x, int y, char heading)
        {
            X = x;
            Y = y;
            Heading = heading;

            Path = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(X, Y)
            };
        }

        public override string ToString()
        {
            return $"{X} {Y} {Heading}";
        }

        /// <summary>
        /// Moves using the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// the command return value.
        /// </returns>
        public string Move(string command, Plateau plateau)
        {
            command = command.ToUpper();
            foreach (char c in command)
            {
                switch (c)
                {
                    case 'L':
                        Turn(left: true);
                        break;
                    case 'R':
                        Turn(left: false);
                        break;
                    case 'M':
                        Move(plateau);
                        break;
                }
            }
            
            return ToString();
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        private void Move(Plateau plateau)
        {
            switch (Heading)
            {
                case 'N':
                    if (plateau.UpperY > Y)
                        Y++;
                    break;
                case 'E':
                    if (plateau.UpperX > X)
                        X++;
                    break;
                case 'S':
                    if (Plateau.LOWER_Y < Y)
                        Y--;
                    break;
                case 'W':
                    if (Plateau.LOWER_X < X)
                        X--;
                    break;
            }
            Path.Add(new Tuple<int, int>(X, Y));
        }

        /// <summary>
        /// Turns.
        /// </summary>
        /// <param name="left">if set to <c>true</c> turn left otherwise turn right.</param>
        private void Turn(bool left)
        {
            switch (Heading)
            {
                case 'N':
                    if (left)
                        Heading = 'W';
                    else
                        Heading = 'E';
                    break;
                case 'E':
                    if (left)
                        Heading = 'N';
                    else
                        Heading = 'S';
                    break;
                case 'S':
                    if (left)
                        Heading = 'E';
                    else
                        Heading = 'W';
                    break;
                case 'W':
                    if (left)
                        Heading = 'S';
                    else
                        Heading = 'N';
                    break;
            }
        }
    }
}
