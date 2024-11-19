namespace Deepin.Domain.CommentAggregates;
public class Comment : Entity<int>, IAggregateRoot
{
    private readonly List<CommentLike> _commentLikes = new List<CommentLike>();
    public virtual ICollection<CommentLike> CommentLikes => _commentLikes;
    protected Comment()
    {
        Content = string.Empty;
        CreatedBy = string.Empty;
    }
    public Comment(int postId, int parentId, string content, string userId) : this()
    {
        PostId = postId;
        ParentId = parentId;
        Content = content;
        CreatedBy = userId;
    }
    public int PostId { get; private set; }
    public int ParentId { get; private set; }
    public string Content { get; private set; }
    public bool IsApproved { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public void Like(string userId)
    {
        _commentLikes.Add(new CommentLike(userId));
    }
}
