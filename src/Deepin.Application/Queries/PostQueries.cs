using AutoMapper;
using Deepin.Application.Models;
using Deepin.Domain.PostAggregates;
using Deepin.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Application.Queries;

public class PostQueries(IMapper mapper, ITagQueries tagQueries, ICategoryQueries categoryQueries, DeepinDbContext dbContext) : IPostQueries
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITagQueries _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
    private readonly ICategoryQueries _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    public async Task<PostDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.FindAsync([id], cancellationToken);
        if (post is null)
        {
            return null;
        }
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        var categories = await _categoryQueries.GetListAsync(cancellationToken);
        var postDto = _mapper.Map<PostDto>(post);
        postDto.Tags = tags?.Where(t => post.PostTags.Select(pt => pt.TagId).Contains(t.Id)).ToList() ?? [];
        postDto.Categories = categories?.Where(c => post.PostCategories.Select(pc => pc.CategoryId).Contains(c.Id)).ToList() ?? [];
        return postDto;
    }

    public async Task<PagedResult<PostDto>?> GetListAsync(PostPagedQuery pagedQuery, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Posts
            .Include(p => p.PostTags)
            .Include(p => p.PostCategories)
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt);

        var pagedPosts = query.Skip(pagedQuery.PageIndex * pagedQuery.PageSize).Take(pagedQuery.PageSize).ToList();
        var postDtos = _mapper.Map<IEnumerable<PostDto>>(pagedPosts);
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        var categories = await _categoryQueries.GetListAsync(cancellationToken);
        foreach (var postDto in postDtos)
        {
            postDto.Tags = tags?.Where(t => pagedPosts.SelectMany(p => p.PostTags).Select(pt => pt.TagId).Contains(t.Id)).ToList() ?? [];
            postDto.Categories = categories?.Where(c => pagedPosts.SelectMany(p => p.PostCategories).Select(pc => pc.CategoryId).Contains(c.Id)).ToList() ?? [];
        }
        return new PagedResult<PostDto>(pagedQuery.PageIndex, pagedQuery.PageSize, query.Count(), postDtos);
    }
}
public class PostPagedQuery : PagedQueryBase
{
    public string? UserId { get; set; }
    public PostStatus? PostStatus { get; set; }
}
public class PostDto
{
    public int Id { get; set; }
    public bool IsPublic { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public PostStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public List<TagDto> Tags { get; set; } = [];
    public List<CategoryDto> Categories { get; set; } = [];
}

