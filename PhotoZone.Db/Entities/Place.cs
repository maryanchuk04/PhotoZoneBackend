namespace PhotoZone.Entities;

public class Place
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Owner { get; set; }

    public Guid? LocationId { get; set; }

    public string MainImage { get; set; }

    public double Rate { get; set; }

    public virtual Location Location { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Images> Images { get; set; }
}