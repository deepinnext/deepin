namespace Deepin.Domain.Entities;

public class FileObject : Entity<Guid>, IFileObject
{
    public string Path { get; set; }
    public string Name { get; set; }
    public string Format { get; set; }
    public long Length { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; private set; }

    public FileObject()
    {
        Path = string.Empty;
        Name = string.Empty;
        Format = string.Empty;
        CreatedBy = string.Empty;
    }
    public FileObject(string name, string format, long length, string createdBy) : this()
    {
        Name = name;
        Format = format;
        Length = length;
        CreatedBy = createdBy;
    }
}
public interface IFileObject
{
    string Name { get; }
    string Path { get; }
    string Format { get; }
    long Length { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
}
