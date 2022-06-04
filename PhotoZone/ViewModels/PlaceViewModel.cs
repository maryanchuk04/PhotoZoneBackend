namespace PhotoZone.ViewModels;

public class PlaceViewModel
{
    public string Title { get; set; }

    public Guid Owner { get; set; }

    public string MainImage { get; set; }

    public List<string> Images { get; set; }

    public double Rating { get; set; }

    public LocationViewModel Location { get; set; }

    public string Description { get; set; }
}