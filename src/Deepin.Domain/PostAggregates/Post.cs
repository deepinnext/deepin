namespace Deepin.Domain.PostAggregates;
public class Post : Entity<int>, IAggregateRoot
{
    private readonly List<PostTag> _postTags = [];
    public virtual ICollection<PostTag> PostTags => _postTags;
    public string Content { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Post()
    {
        Content = string.Empty;
        CreatedBy = string.Empty;
    }
    public Post(string content, string createdBy)
    {
        Content = content;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
    }
    public void Update(string content)
    {
        Content = content;
        UpdatedAt = DateTime.Now;
    }
    public void ClearTags()
    {
        _postTags.Clear();
    }
    public void AddTag(int tagId)
    {
        if (_postTags.All(x => x.TagId != tagId))
        {
            _postTags.Add(new PostTag(tagId));
        }
    }
}
