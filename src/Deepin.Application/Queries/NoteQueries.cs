using AutoMapper;
using Deepin.Application.Models;
using Deepin.Domain.NoteAggregates;
using Deepin.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Application.Queries;

public class NoteQueries(IMapper mapper, ITagQueries tagQueries, ICategoryQueries categoryQueries, DeepinDbContext dbContext) : INoteQueries
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITagQueries _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
    private readonly ICategoryQueries _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
    private readonly DeepinDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    public async Task<NoteDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var note = await _dbContext.Notes.FindAsync([id], cancellationToken);
        if (note is null)
        {
            return null;
        }
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        var categories = await _categoryQueries.GetListAsync(cancellationToken);
        var noteDto = _mapper.Map<NoteDto>(note);
        noteDto.Tags = tags?.Where(t => note.NoteTags.Select(nt => nt.TagId).Contains(t.Id)).ToList() ?? [];
        noteDto.Category = categories?.FirstOrDefault(c => c.Id == note.CategoryId);
        return noteDto;
    }

    public async Task<PagedResult<NoteDto>?> GetListAsync(string userId, NoteQuery noteQuery, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Notes.Include(n => n.NoteTags).AsNoTracking();

        query = query.Where(n => n.CreatedBy == userId);

        if (noteQuery.IsDeleted.HasValue)
        {
            query = query.Where(n => n.IsDeleted == noteQuery.IsDeleted.Value);
        }
        if (noteQuery.Status.HasValue)
        {
            query = query.Where(n => n.Status == noteQuery.Status.Value);
        }
        if (noteQuery.IsPublic.HasValue)
        {
            query = query.Where(n => n.IsPublic == noteQuery.IsPublic.Value);
        }
        if (noteQuery.CategoryId > 0)
        {
            query = query.Where(n => n.CategoryId == noteQuery.CategoryId);
        }
        var pagedNotes = query.Skip(noteQuery.PageIndex * noteQuery.PageSize).Take(noteQuery.PageSize)
        .OrderByDescending(s => s.UpdatedAt).ToList();
        var noteDtos = _mapper.Map<IEnumerable<NoteDto>>(pagedNotes);
        var tags = await _tagQueries.GetListAsync(cancellationToken);
        var categories = await _categoryQueries.GetListAsync(cancellationToken);
        foreach (var noteDto in noteDtos)
        {
            noteDto.Tags = tags?.Where(t => pagedNotes.SelectMany(n => n.NoteTags).Select(nt => nt.TagId).Contains(t.Id)).ToList() ?? [];
            noteDto.Category = categories?.FirstOrDefault(c => c.Id == noteDto.CategoryId);
        }
        return new PagedResult<NoteDto>(noteQuery.PageIndex, noteQuery.PageSize, query.Count(), noteDtos);
    }

}

public class NoteQuery : PagedQueryBase
{
    public NoteStatus? Status { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsPublic { get; set; }
    public int CategoryId { get; set; }
}
public class NoteDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public NoteStatus Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? CoverImageId { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public List<TagDto> Tags { get; set; } = [];
    public CategoryDto? Category { get; set; }
}

