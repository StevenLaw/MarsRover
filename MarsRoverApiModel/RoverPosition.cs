using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverApiModel
{
    public class RoverPosition
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public char Heading { get; set; } = 'N';

        public override string ToString()
        {
            return $"{X} {Y} {Heading}";
        }
    }
}
