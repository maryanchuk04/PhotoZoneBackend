using PhotoZone.Core.DTOs;

namespace PhotoZone.Core.IServices;

public interface ICommentService
{
    public CommentDto WriteComment(Guid id, Guid userId, string text);
}