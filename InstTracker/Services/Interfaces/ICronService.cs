namespace InstTracker.Services.Interfaces
{
    public interface ICronService
    {
        string GetDescribedCronNextRun(string schedule);

        DateTime GetLastHit(string cron);

        DateTime GetNextHit(string cron);
    }
}