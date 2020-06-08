namespace MarsRoverApiModel
{ 
    public enum CommandType : byte
    {
        SetupPlateau = 0x1,
        SetupRover = 0x2,
        Move = 0x3,
        UploadImage = 0x4
    }
}