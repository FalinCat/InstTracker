using InstTracker.Data;
using InstTracker.Data.Model;
using InstTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await _dbContext.Instances.OrderBy(i => i.Id).ToListAsync();
    }
}
