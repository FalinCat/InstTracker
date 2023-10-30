using CronExpressionDescriptor;
using InstTracker.Services.Interfaces;
using NCrontab;

namespace InstTracker.Services
{
    public class CronService : ICronService
    {
        public DateTime GetNextHit(string cron) =>
            CrontabSchedule.Parse(cron).GetNextOccurrence(DateTime.Now);

        public DateTime GetLastHit(string cron)
        {
            var nextHit = CrontabSchedule
                .Parse(cron)
                .GetNextOccurrence(DateTime.Now);
            DateTime lastRun = nextHit;

            for (int i = 1; i < 14; i++)
            {
                var lastHit = CrontabSchedule
                    .Parse(cron)
                    .GetNextOccurrence(DateTime.Now.AddDays(-i));

                if (lastHit < nextHit)
                {
                    lastRun = lastHit; break;
                }
            }

            return lastRun;
        }

        public string GetDescribedCronNextRun(string schedule)
        {
            string result;
            try
            {
                result = ExpressionDescriptor.GetDescription(schedule, new Options()
                {
                    DayOfWeekStartIndexZero = true,
                    Use24HourTimeFormat = true,
                    Locale = "ru"
                }).Replace("только в", "");
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}