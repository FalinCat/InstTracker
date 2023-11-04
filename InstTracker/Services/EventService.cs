using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstTracker.Data;
using InstTracker.Data.Model;
using InstTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NCrontab;

namespace InstTracker.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _dbContext;
        private IMemoryCache _cache;

        public EventService(AppDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<EventCardInfo> GetEventInfo(EventType eventType)
        {
            string? eventSchedule = string.Empty;

            var name = eventType switch
            {
                EventType.MajesticTvT => "Majestic TvT",
                EventType.ImperialTvT => "Imperial TvT",
                EventType.RoyalTvT => "Royal TvT",
                EventType.Massacre => "Мясорубка",
                EventType.Elpi => "Нашествие Эльпи",
                EventType.SPC => "Счастливые Полчаса",
                EventType.Tovy => "Сокровища Тову",
                _ => string.Empty,
            };

            if (!_cache.TryGetValue(name, out eventSchedule))
            {
                eventSchedule = await _dbContext.Events
                                        .Where(x => x.Name == name)
                                        .Select(x => x.Schedule)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
                if (eventSchedule is not null)
                {
                    _cache.Set(name, eventSchedule);
                }
            }

            var nextUTCTime = CrontabSchedule
                .Parse(eventSchedule)
                .GetNextOccurrence(DateTime.UtcNow);

            var localNextHitTime = TimeZoneInfo.ConvertTimeFromUtc(nextUTCTime, TimeZoneInfo.Local);

            TimeSpan timeRemaining = TimeSpan.FromTicks(localNextHitTime.Ticks - DateTime.Now.Ticks);

            var eventInfo = new EventCardInfo
            {
                Name = name,
                NextHit = localNextHitTime,
                TimeRemaining = timeRemaining
            };

            return eventInfo;
        }
    }
}