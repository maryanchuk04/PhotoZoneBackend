namespace PhotoZone.Entities;

public class Comment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid PlaceId { get; set; }

    public string CommentText { get; set; }
}