using Deepin.Domain.NoteAggregates;

namespace Deepin.Domain.PageAggregates;

public class Note : Entity<int>, IAggregateRoot
{
    private readonly List<NoteTag> _noteTags = [];
    public virtual ICollection<NoteTag> NoteTags => _noteTags;
    public int CategoryId { get; private set; }
    public NoteStatus Status { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Description { get; private set; }
    public Guid? CoverImageId { get; private set; }
    public string CreatedBy { get; private set; }
    public bool IsPublic { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    protected Note()
    {
        Title = string.Empty;
        Description = string.Empty;
        Content = string.Empty;
        CreatedBy = string.Empty;
        Status = NoteStatus.Draft;
    }
    public Note(string userId, int categoryId, string title, string content, bool isPublic = false, string? description = null, Guid? coverImageId = null) : this()
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Content cannot be empty");
        CategoryId = categoryId;
        Title = title;
        Content = content;
        Description = description ?? string.Empty;
        CoverImageId = coverImageId;
        IsPublic = isPublic;
        CreatedBy = userId;
    }

    public void Update(string title, string content, bool isPublic = false, string? description = null, Guid? coverImageId = null, bool? isPublished = false)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Content cannot be empty");

        Title = title;
        Content = content;
        Description = description ?? string.Empty;
        CoverImageId = coverImageId;
        IsPublic = isPublic;
        UpdatedAt = DateTime.UtcNow;
        if (isPublished.HasValue)
        {
            if (isPublished.Value)
            {
                Publish();
            }
            else
            {
                Unpublish();
            }
        }
    }
    public void ChangeCategory(int categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Publish()
    {
        if (Status != NoteStatus.Published)
        {
            Status = NoteStatus.Published;
            PublishedAt = DateTime.UtcNow;
        }
    }

    public void Unpublish()
    {
        if (Status == NoteStatus.Published)
        {
            Status = NoteStatus.Draft;
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public void ClearTags()
    {
        _noteTags.Clear();
    }
    public void AddTag(int tagId)
    {
        if (_noteTags.All(x => x.TagId != tagId))
        {
            _noteTags.Add(new NoteTag(tagId));
        }
    }

    public void Delete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
    public void Restore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
    public void Archive()
    {
        if (Status == NoteStatus.Published)
        {
            Status = NoteStatus.Archived;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
