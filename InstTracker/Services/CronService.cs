using CronExpressionDescriptor;
using InstTracker.Services.Interfaces;
using NCrontab;

namespace InstTracker.Services
{
    public class CronService : ICronService
    {
        public DateTime GetNextHit(string cron)
        {
            var nextUtcHit = CrontabSchedule
                .Parse(cron)
                .GetNextOccurrence(DateTime.UtcNow);
            var nextLocalTime = TimeZoneInfo.ConvertTimeFromUtc(nextUtcHit, TimeZoneInfo.Local);

            return nextLocalTime;
        }

        public DateTime GetLastHit(string cron)
        {
            var nextUTCTime = CrontabSchedule
                .Parse(cron)
                .GetNextOccurrence(DateTime.UtcNow);

            DateTime lastRun = nextUTCTime;

            for (int i = 1; i < 14; i++)
            {
                var lastHit = CrontabSchedule
                    .Parse(cron)
                    .GetNextOccurrence(DateTime.Now.AddDays(-i));

                if (lastHit < nextUTCTime)
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
                    Locale = "ru",
                })[18..];
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}