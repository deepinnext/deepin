using System;
using Deepin.Domain;
using Deepin.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Infrastructure.Repositories;


public class PostRepository(DeepinDbContext dbContext) : IPostRepository
{
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    public IUnitOfWork UnitOfWork => _dbContext;
    public async Task<Post?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts
            .Include(p => p.PostTags)
            .Include(p => p.PostCategories)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task AddAsync(Post post, CancellationToken cancellationToken = default)
    {
        await _dbContext.Posts.AddAsync(post, cancellationToken);
    }
    public void Update(Post post)
    {
        _dbContext.Entry(post).State = EntityState.Modified;
    }

    public void Delete(Post post)
    {
        _dbContext.Posts.Remove(post);
    }
}
