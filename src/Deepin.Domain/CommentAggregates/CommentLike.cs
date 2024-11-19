namespace Deepin.Domain.CommentAggregates;
public class CommentLike : Entity<int>
{
    public int CommentId { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    protected CommentLike() 
    {
        CreatedBy = string.Empty;
    }
    public CommentLike(string userId) : this()
    {
        CreatedBy = userId;
    }
}
