using InstTracker.Data;
using InstTracker.Data.Model;
using InstTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InstTracker.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICronService _cronService;

        public HistoryService(AppDbContext dbContext, ICronService cronService)
        {
            _dbContext = dbContext;
            _cronService = cronService;
        }

        public async Task<List<Instance>> GetAllUndoneInstancesForCharacter(string characterName)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Name == characterName);

            if (character == null)
            {
                throw new Exception($"Персонаж {characterName} не найден");
            }

            List<Instance> undoneInstances = new List<Instance>();
            foreach (var inst in await _dbContext.Instances.AsNoTracking().ToListAsync())
            {
                var lastRun = await _dbContext.InstancesHistory
                    .Where(hist => hist.InstanceId == inst.Id && hist.CharacterId == character.Id)
                    .OrderByDescending(x => x.DateDone)
                    .Select(hist => hist.DateDone)
                    .FirstOrDefaultAsync();

                var lastHit = _cronService.GetLastHit(inst.Schedule);

                if (lastRun < lastHit)
                {
                    undoneInstances.Add(inst);
                }
            }

            return undoneInstances.OrderBy(x => _cronService.GetNextHit(x.Schedule)).ToList();
        }

        public async Task<string> MarkInstanceAsDone(string characterName, string instanceName)
        {
            var characterEntity = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Name == characterName);

            if (characterEntity == null)
            {
                return "Персонаж не найден";
            }

            var instanceEntity = await _dbContext.Instances.FirstOrDefaultAsync(i => i.Name == instanceName);
            if (instanceEntity == null)
            {
                return "Инстанс не найден";
            }

            try
            {
                characterEntity.InstancesHistory.Add(new InstanceHistory()
                { CharacterId = characterEntity.Id, InstanceId = instanceEntity.Id, DateDone = DateTime.Now });

                await _dbContext.SaveChangesAsync();
                return "пройдено";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}