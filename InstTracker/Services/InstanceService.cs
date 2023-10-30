using InstTracker.Data;
using InstTracker.Data.Model;
using InstTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NCrontab;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace InstTracker.Services
{
    public class InstanceService : IInstanceService
    {
        private readonly AppDbContext _dbContext;

        public InstanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Instance>> GetAllInstancesAsync() =>
            await _dbContext.Instances.ToListAsync();

        public async Task<Instance> Find(Expression<Func<Instance, bool>> predicate) =>
            await _dbContext.Instances.FirstOrDefaultAsync(predicate);

        public async Task<string> SetHiddenState(string instanceName, bool newState)
        {
            var instance = await _dbContext.Instances.FirstOrDefaultAsync(i => i.Name == instanceName);
            if (instance == null)
            {
                return $"Инстанс с названием {instanceName} не найден";
            }

            instance.isHidden = newState;
            await _dbContext.SaveChangesAsync();

            return "Сделано";
        }

        public async Task<string> AddInstance(string name, string schedule)
        {
            try
            {
                var cronValid = CrontabSchedule.TryParse(schedule);
                if (cronValid == null)
                {
                    return "Расписание не валидно";
                }

                var isExist = await _dbContext.Instances.FirstOrDefaultAsync(i => i.Name == name);
                if (isExist != null)
                {
                    return $"Инстанс с названием {name} уже есть в базе";
                }

                var instance = new Instance { Name = name, Schedule = schedule };
                var id = await _dbContext.Instances.AddAsync(instance);
                await _dbContext.SaveChangesAsync();

                return $"Инстанс {instance.Name} добавлен";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> RemoveInstance(Expression<Func<Instance, bool>> predicate)
        {
            try
            {
                var instance = await _dbContext.Instances.FirstOrDefaultAsync(predicate);

                if (instance is null)
                {
                    return "Персонаж не найден";
                }

                _dbContext.Instances.Remove(instance);

                await _dbContext.SaveChangesAsync();

                return "Удалено";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}