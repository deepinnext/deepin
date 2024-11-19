namespace Deepin.Domain.Entities;
public class Category : Entity<int>
{
    public int ParentId { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public int Level { get; private set; } = 0;
    public string? Description { get; private set; }
    public string? Path { get; private set; }
    public int DisplayOrder { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    protected Category()
    {
        Name = string.Empty;
        Slug = string.Empty;
        CreatedBy = string.Empty;
    }
    public Category(string userId, string name, string? description = null) : this()
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty");

        CreatedBy = userId;
        Name = name;
        Description = description;
        Slug = name.ToLowerInvariant().Replace(" ", "-");
        Level = 0;
        Path = null;
    }
    public void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty");

        Name = name;
        Description = description;
        Slug = name.ToLowerInvariant().Replace(" ", "-");
    }
    public void SetParent(Category? parent = null)
    {
        if (parent is null)
        {
            ParentId = 0;
            Level = 0;
            Path = null;
        }
        else
        {
            if (parent.Id == this.Id)
                throw new DomainException("Category cannot be its own parent");

            ParentId = parent.Id;
            Level = parent.Level + 1;
            Path = parent.Path == null ? parent.Id.ToString() : $"{parent.Path}.{Id}";
        }
    }
    public void SetDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
            throw new DomainException("Sort order cannot be negative");

        DisplayOrder = displayOrder;
    }
}