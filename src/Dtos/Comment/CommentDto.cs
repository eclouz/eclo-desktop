namespace Dtos.Comment;

public class CommentDto
{
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string Comment { get; set; } = String.Empty;
    public bool IsEdited { get; set; }
}
