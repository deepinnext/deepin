using AutoMapper;
using Deepin.Application.Models;
using Deepin.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Application.Queries;

public class PostQueries(IMapper mapper, ITagQueries tagQueries, DeepinDbContext dbContext) : IPostQueries
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITagQueries _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    public async Task<PostDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.FindAsync([id], cancellationToken);
        if (post is null)
        {
            return null;
        }
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        var postDto = _mapper.Map<PostDto>(post);
        postDto.Tags = tags?.Where(t => post.PostTags.Select(pt => pt.TagId).Contains(t.Id)).ToList() ?? [];
        return postDto;
    }

    public async Task<PagedResult<PostDto>?> GetListAsync(string userId, PostQuery postQuery, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Posts
            .Include(p => p.PostTags)
            .AsNoTracking();

        query = query.Where(p => p.CreatedBy == userId);
        if (postQuery.TagId > 0)
        {
            query = query.Where(p => p.PostTags.Select(pt => pt.TagId).Contains(postQuery.TagId));
        }
        var pagedPosts = query.Skip(postQuery.PageIndex * postQuery.PageSize).Take(postQuery.PageSize).ToList();
        var postDtos = _mapper.Map<IEnumerable<PostDto>>(pagedPosts);
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        foreach (var postDto in postDtos)
        {
            postDto.Tags = tags?.Where(t => pagedPosts.SelectMany(p => p.PostTags).Select(pt => pt.TagId).Contains(t.Id)).ToList() ?? [];
        }
        return new PagedResult<PostDto>(postQuery.PageIndex, postQuery.PageSize, query.Count(), postDtos);
    }
}
public class PostQuery : PagedQueryBase
{
    public int TagId { get; set; }
}
public class PostDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<TagDto> Tags { get; set; } = [];
}

