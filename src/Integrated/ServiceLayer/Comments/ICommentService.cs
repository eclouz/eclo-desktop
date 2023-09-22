using Dtos.Comment;

namespace Integrated.ServiceLayer.Comments;

public interface ICommentService
{
    Task<bool> CreateComment(CommentDto commentDto, string token);
}
