namespace Deepin.Domain.Entities;
public class Tag : Entity<int>
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string? Description { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    protected Tag()
    {
        Name = string.Empty;
        Slug = string.Empty;
        CreatedBy = string.Empty;
    }
    public Tag(string userId, string name, string? description = null) : this()
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Tag name cannot be empty");
        Name = name;
        Slug = name.ToLowerInvariant().Replace(" ", "-");
        CreatedBy = userId;
        Description = description;
    }

    public void Update(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Tag name cannot be empty");

        Name = name;
        Slug = name.ToLowerInvariant().Replace(" ", "-");
        Description = description;
    }
}
