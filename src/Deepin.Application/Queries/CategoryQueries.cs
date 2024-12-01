using AutoMapper;
using Deepin.Application.Constants;
using Deepin.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Deepin.Application.Queries;

public class CategoryQueries(IDistributedCache cache, IMapper mapper, DeepinDbContext dbContext) : ICategoryQueries
{
    private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync([id], cancellationToken);
        return category is null ? null : _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>?> GetListAsync(CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.GetAllCategories(), async () =>
        {
            var categories = await _dbContext.Categories.OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.Id).ToListAsync(cancellationToken);
            return categories is null ? null : _mapper.Map<IEnumerable<CategoryDto>>(categories);
        });
    }
}
public class CategoryDto
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsBuiltIn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}