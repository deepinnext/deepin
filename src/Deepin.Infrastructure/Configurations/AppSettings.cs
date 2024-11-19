using Deepin.Infrastructure.Constants;

namespace Deepin.Infrastructure.Configurations;
public class AppSettings(
    string dbConnection, 
    string dataFolder = AppDefaults.DATA_FOLDER, 
    FileStorageType fileStorageType = FileStorageType.FileSystem, 
    string? redisConnection = null)
{
    public string DbConnection { get; set; } = dbConnection;
    public string DataFolder { get; set; } = dataFolder;
    public string? RedisConnection { get; set; } = redisConnection;
    public bool UseRedisCache => !string.IsNullOrEmpty(RedisConnection);
    public FileStorageType FileStorageType { get; set; }
}

public enum FileStorageType
{
    FileSystem,
    AmazonS3,
    AzureBlobStorage
}