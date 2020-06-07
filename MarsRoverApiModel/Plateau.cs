using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverApiModel
{
    public class Plateau
    {
        public const int LOWER_X = 0;
        public const int LOWER_Y = 0;
        public int UpperX { get; private set; } = 0;
        public int UpperY { get; private set; } = 0;

        public Plateau() {}

        public Plateau(int upperX, int upperY)
        {
            UpperX = upperX;
            UpperY = upperY;
        }

        public override string ToString()
        {
            return $"{LOWER_X} {LOWER_Y} -> {UpperX} {UpperY}";
        }
    }
}
