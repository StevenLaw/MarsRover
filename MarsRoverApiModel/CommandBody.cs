using System;

namespace MarsRoverApiModel
{
    public class CommandBody
    {
        public CommandType Type { get; set; }
        public string Command { get; set; }
    }
}
