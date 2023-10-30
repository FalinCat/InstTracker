using InstTracker.Data.Model;
using System.Linq.Expressions;

namespace InstTracker.Services.Interfaces
{
    public interface IInstanceService
    {
        Task<string> AddInstance(string name, string schedule);

        Task<Instance> Find(Expression<Func<Instance, bool>> predicate);

        Task<List<Instance>> GetAllInstancesAsync();

        Task<string> RemoveInstance(Expression<Func<Instance, bool>> predicate);
        Task<string> SetHiddenState(string instanceName, bool newState);
    }
}