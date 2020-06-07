using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverApiModel
{
    public class HistoryRecord
    {
        public bool Input { get; set; }
        public string Command { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is HistoryRecord hist)
                return hist.Input == Input && hist.Command == Command;
            return false;
        }

        public override int GetHashCode()
        {
            return Input.GetHashCode() ^ Command.GetHashCode();
        }

        public override string ToString()
        {
            string input = Input ? "Input" : "Output";
            return $"{input}: {Command}";
        }
    }
}
