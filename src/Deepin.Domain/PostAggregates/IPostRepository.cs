namespace Deepin.Domain.PostAggregates;

public interface IPostRepository : IRepository<Post>
{
    Task AddAsync(Post post, CancellationToken cancellationToken = default);
    Task<Post?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Update(Post post);
    void Delete(Post post);
}
