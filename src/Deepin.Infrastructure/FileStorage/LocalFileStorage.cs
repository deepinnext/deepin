using Deepin.Domain.Entities;
using Deepin.Infrastructure.Configurations;
using Deepin.Infrastructure.FileStorage;

namespace Deepin.Infrastructure.BlobStorage;

public class LocalFileObject : IFileObject
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Format { get; set; }
    public long Length { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public LocalFileObject(string path, FileInfo file)
    {
        Name = file.Name;
        Path = System.IO.Path.GetRelativePath(path, file.FullName);
        Length = file.Length;
        CreatedAt = file.CreationTimeUtc;
        UpdatedAt = file.LastWriteTimeUtc;
        Format = file.Extension.Replace(".", string.Empty).ToLower();
    }
}

public class LocalFileStorage(AppSettings appSettings) : IFileStorage
{
    private string GetFullPath(string relativePath)
    {
        return Path.Combine(appSettings.DataFolder, relativePath);
    }
    public async Task CreateAsync(IFileObject file, Stream stream)
    {
        var fullPath = GetFullPath(file.Path);
        var dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        var fileInfo = new FileInfo(fullPath);
        using var fs = fileInfo.Create();
        await stream.CopyToAsync(fs);
        await fs.FlushAsync();
    }
    public async Task DeleteAsync(IFileObject file)
    {
        var fullPath = GetFullPath(file.Path);
        var fileInfo = new FileInfo(fullPath);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        await Task.CompletedTask;
    }
    public async Task<Stream> GetStreamAsync(IFileObject file)
    {
        var fullPath = GetFullPath(file.Path);
        var fileInfo = new FileInfo(fullPath);
        if (!fileInfo.Exists)
            return Stream.Null;
        return await Task.FromResult(fileInfo.OpenRead());
    }

}
