namespace Deepin.Domain.PostAggregates;
public class PostCategory : Entity
{
    public int PostId { get; set; }
    public int CategoryId { get; set; }
    public PostCategory(int categoryId)
    {
        CategoryId = categoryId;
    }
}
