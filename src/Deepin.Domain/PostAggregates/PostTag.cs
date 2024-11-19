namespace Deepin.Domain.PostAggregates;
public class PostTag : Entity
{
    public int PostId { get; set; }
    public int TagId { get; set; }
    public PostTag(int tagId)
    {
        TagId = tagId;
    }
}
