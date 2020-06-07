namespace MarsRoverWebService.Models
{
    public enum Direction
    {
        North = 'N',
        East = 'E',
        South = 'S',
        West = 'W'
    }
    public class RoverPostion
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Direction Heading { get; set; }
        public override string ToString()
        {
            return $"{XCoord} {YCoord} {(char)Heading}";
        }
    }
}