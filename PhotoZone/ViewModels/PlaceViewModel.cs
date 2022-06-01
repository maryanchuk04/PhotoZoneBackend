namespace PhotoZone.ViewModels;

public class PlaceViewModel
{
    public string Title { get; set; }

    public string Owner { get; set; }

    public string MainImage { get; set; }

    public IEnumerable<string> Images { get; set; }

    public double Rating { get; set; }

    public LocationViewModel Location { get; set; }

    public string Description { get; set; }

    public List<string> Comments { get; set; }
}