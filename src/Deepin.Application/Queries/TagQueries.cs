using AutoMapper;
using Deepin.Application.Constants;
using Deepin.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Deepin.Application.Queries;

public class TagQueries(IDistributedCache cache, IMapper mapper, DeepinDbContext dbContext) : ITagQueries
{
    private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<TagDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var tag = await _dbContext.Tags.FindAsync([id], cancellationToken);
        return tag is null ? null : _mapper.Map<TagDto>(tag);
    }

    public async Task<TagDto?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var tags = await GetListAsync(cancellationToken);
        return tags?.FirstOrDefault(x => x.Name == name);
    }

    public async Task<IEnumerable<TagDto>?> GetListAsync(CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.GetAllTags(), async () =>
        {
            var tags = await _dbContext.Tags.ToListAsync(cancellationToken);
            return tags is null ? null : _mapper.Map<IEnumerable<TagDto>>(tags);
        });
    }
}

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int PostCount { get; set; }
}
