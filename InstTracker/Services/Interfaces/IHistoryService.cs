using InstTracker.Data.Model;

namespace InstTracker.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<List<Instance>> GetAllUndoneInstancesForCharacter(string character);

        Task<string> MarkInstanceAsDone(string characterName, string instanceName);
    }
}