using InstTracker.Data.Model;

namespace InstTracker.Services.Interfaces
{
    public interface IInstanceService
    {
        Task<List<Instance>> GetAllInstancesAsync();
    }
}