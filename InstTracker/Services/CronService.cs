using InstTracker.Services.Interfaces;

namespace InstTracker.Services
{
    internal class CronService : ICronService
    {
        public DateTime GetLastHit(string cron)
        {
            return DateTime.Now;
        }
    }
}
