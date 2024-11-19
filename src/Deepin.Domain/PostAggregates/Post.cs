using Deepin.Domain.Entities;

namespace Deepin.Domain.PostAggregates;
public class Post : Entity<int>, IAggregateRoot
{
    private readonly List<PostTag> _postTags = [];
    private readonly List<PostCategory> _postCategories = [];
    public virtual ICollection<PostTag> PostTags => _postTags;
    public virtual ICollection<PostCategory> PostCategories => _postCategories;
    public PostStatus Status { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Content { get; private set; }
    public string Summary { get; private set; }
    public bool IsPublic { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    protected Post()
    {
        Title = string.Empty;
        Slug = string.Empty;
        Content = string.Empty;
        Summary = string.Empty;
        CreatedBy = string.Empty;
        Status = PostStatus.Draft;
    }
    public Post(string userId, string title, string content, string? summary = null) : this()
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Content cannot be empty");

        CreatedBy = userId;
        Title = title;
        Content = content;
        Summary = summary ?? content.Substring(0, Math.Min(content.Length, 200));
    }

    public void Update(string title, string content, string? summary = null, bool isPublic = false, bool isPublished = false)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Content cannot be empty");

        Title = title;
        Content = content;
        Summary = summary ?? content.Substring(0, Math.Min(content.Length, 200));
        IsPublic = isPublic;
        UpdatedAt = DateTime.UtcNow;
        if (isPublished)
        {
            Publish();
        }
        else
        {
            Unpublish();
        }
    }
    public void Publish()
    {
        if (Status != PostStatus.Published)
        {
            Status = PostStatus.Published;
            PublishedAt = DateTime.UtcNow;
        }
    }

    public void Unpublish()
    {
        if (Status == PostStatus.Published)
        {
            Status = PostStatus.Draft;
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public void UpdateTags(IEnumerable<int>? tagIds)
    {
        if (tagIds is null || !tagIds.Any())
        {
            _postTags.Clear();
            return;
        }
        var currentTagIds = _postTags.Select(x => x.TagId).ToList();
        var newTagIds = tagIds.ToList();
        var tagsToAdd = newTagIds.Except(currentTagIds);
        var tagsToRemove = currentTagIds.Except(newTagIds);

        tagsToAdd.ToList().ForEach(tagId =>
        {
            if (_postTags.All(x => x.TagId != tagId))
            {
                _postTags.Add(new PostTag(tagId));
            }
        });

        tagsToRemove.ToList().ForEach(tagId =>
        {
            var postTag = _postTags.FirstOrDefault(x => x.TagId == tagId);
            if (postTag is not null)
            {
                _postTags.Remove(postTag);
            }
        });
    }
    public void UpdateCategories(IEnumerable<int>? categoryIds)
    {
        if (categoryIds is null || !categoryIds.Any())
        {
            _postCategories.Clear();
            return;
        }
        var currentCategoryIds = _postCategories.Select(x => x.CategoryId).ToList();
        var newCategoryIds = categoryIds.ToList();
        var categoriesToAdd = newCategoryIds.Except(currentCategoryIds);
        var categoriesToRemove = currentCategoryIds.Except(newCategoryIds);

        categoriesToAdd.ToList().ForEach(categoryId =>
        {
            if (_postCategories.All(x => x.CategoryId != categoryId))
            {
                _postCategories.Add(new PostCategory(categoryId));
            }
        });

        categoriesToRemove.ToList().ForEach(categoryId =>
        {
            var postCategory = _postCategories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (postCategory is not null)
            {
                _postCategories.Remove(postCategory);
            }
        });
    }
    public void AddTag(int tagId)
    {
        if (_postTags.All(x => x.TagId != tagId))
        {
            _postTags.Add(new PostTag(tagId));
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveTag(int tagId)
    {
        var postTag = _postTags.FirstOrDefault(x => x.TagId == tagId);
        if (postTag is not null)
        {
            _postTags.Remove(postTag);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AddCategory(int categoryId)
    {
        if (_postCategories.All(x => x.CategoryId != categoryId))
        {
            _postCategories.Add(new PostCategory(categoryId));
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public void RemoveCategory(int categoryId)
    {
        var postCategory = _postCategories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (postCategory is not null)
        {
            _postCategories.Remove(postCategory);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
