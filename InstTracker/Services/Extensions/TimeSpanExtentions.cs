namespace InstTracker.Services.Extensions
{
    public static class TimespanExtensions
    {
        public static string ToHumanReadableString(this TimeSpan t)
        {
            if (t.TotalSeconds <= 1)
            {
                return $@"{t:s\.ff} секунд";
            }
            if (t.TotalMinutes <= 1)
            {
                return $@"{t:%s} секунд";
            }
            if (t.TotalHours <= 1)
            {
                return $@"{t:%m} минут";
            }
            if (t.TotalDays <= 1)
            {
                return $@"{t:%h} часов";
            }

            return $@"{t:%d} дней";
        }
    }
}