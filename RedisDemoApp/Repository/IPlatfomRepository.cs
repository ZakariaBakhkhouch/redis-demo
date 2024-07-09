using RedisDemoApp.Models;

namespace RedisDemoApp.Repository
{
    public interface IPlatfomRepository
    {
        Task AddPlatform(Platform platform);
        Task<Platform> GetPlatform(Guid id);
        Task<bool> DeletePlatform(Guid id);
        Task<bool> UpdatePlatform(Platform platform, Guid id);
        Task<List<Platform>> GetAllPlatforms();
    }
}
