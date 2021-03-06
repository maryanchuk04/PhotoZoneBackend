using PhotoZone.Entities;

namespace PhotoZone.Core.DTOs;

public class PlaceDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string MainImage { get; set; }

    public Guid OwnerId { get; set; }

    public double Rate { get; set; }

    public LocationDto Location { get; set; }

    public  ICollection<ImagesDto> Images { get; set; }

    public  ICollection<CommentDto> Comments { get; set; }

}