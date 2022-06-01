namespace PhotoZone.Core.Exceptions;

public class PhotoZoneException : Exception
{
    public PhotoZoneException()
    {
    }

    public PhotoZoneException(string message)
        : base(message)
    {
    }

    public PhotoZoneException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public PhotoZoneException(string message, Dictionary<string, string> customData)
        : base(message)
    {
        ValidationErrors = customData;
    }

    public Dictionary<string, string> ValidationErrors { get; set; }
}