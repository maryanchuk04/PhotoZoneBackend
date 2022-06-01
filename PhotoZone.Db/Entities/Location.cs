namespace PhotoZone.Entities;

public class Location
{
    public Guid Id { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string LocationName { get; set; }
}