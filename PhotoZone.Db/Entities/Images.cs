namespace PhotoZone.Entities;

public class Images
{
    public Guid Id { get; set; }

    public Guid? PlaceId { get; set; }

    public string Image { get; set; }
}