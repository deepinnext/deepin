namespace Deepin.Domain.Entities;
public class Category : Entity<int>
{
    public int ParentId { get; private set; }
    public string Name { get; private set; }
    public string? Icon { get; private set; }
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }
    public string CreatedBy { get; private set; }
    public bool IsBuiltIn { get; protected set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    protected Category()
    {
        Name = string.Empty;
        Icon = string.Empty;
        CreatedBy = string.Empty;
    }
    public Category(string userId, string name, string? icon, string? description = null, int displayOrder = 0, bool isBuiltIn = false) : this()
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty");

        CreatedBy = userId;
        Name = name;
        Icon = icon;
        Description = description;
        DisplayOrder = displayOrder;
        IsBuiltIn = isBuiltIn;
    }
    public void Update(string name, string? icon, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty");

        Name = name;
        Description = description;
        Icon = icon;
    }
    public void SetParent(Category? parent = null)
    {
        if (parent is null)
        {
            ParentId = 0;
        }
        else
        {
            if (parent.Id == this.Id)
                throw new DomainException("Category cannot be its own parent");

            ParentId = parent.Id;
        }
    }
    public void SetDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
            throw new DomainException("Sort order cannot be negative");

        DisplayOrder = displayOrder;
    }
}