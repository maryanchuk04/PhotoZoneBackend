using AutoMapper;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;

namespace PhotoZone.Services;

public class CommentService : BaseService<Comment>, ICommentService
{
    public CommentService(AppDbContext context, IMapper mapper = null) : base(context, mapper)
    {
    }

    public CommentDto WriteComment(Guid id, Guid userId, string text)
    {
        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            PlaceId = id,
            UserId = userId,
            CommentText = text
        };

        Insert(comment);
        Context.SaveChanges();

        var res = new CommentDto()
        {
            Id = comment.Id,
            PlaceId = comment.PlaceId,
            CommentText = comment.CommentText,
            UserId = comment.UserId
        };

        return res;
    }
}