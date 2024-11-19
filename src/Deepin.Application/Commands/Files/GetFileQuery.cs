using Deepin.Domain.Entities;
using MediatR;

namespace Deepin.Application.Commands.Files;

public record GetFileQuery(Guid Id) : IRequest<FileDTO?>;
public class FileDTO
{
    public Guid Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public long Length { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; private set; }

    public static FileDTO FromEntity(FileObject file) => new FileDTO
    {
        Id = file.Id,
        Path = file.Path,
        Name = file.Name,
        Format = file.Format,
        Length = file.Length,
        CreatedBy = file.CreatedBy,
        CreatedAt = file.CreatedAt,
        UpdatedAt = file.UpdatedAt
    };
}