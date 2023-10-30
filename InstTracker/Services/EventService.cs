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
            List<string> eventSchedules = default!;
            string name = eventType switch
            {
                EventType.TvT => "TvT",
                EventType.Massacre => "Мясорубка",
                EventType.Elpi => "Нашествие Эльпи",
                EventType.SPC => "Счастливые Полчаса",
                EventType.Tovy => "Сокровища Тову",
                _ => string.Empty,
            };

            if (!_cache.TryGetValue(name, out eventSchedules))
            {
                eventSchedules = await _dbContext.Events
                                        .Where(x => x.Name == name)
                                        .Select(x => x.Schedule)
                                        .ToListAsync();
                if (eventSchedules is not null)
                {
                    _cache.Set(name, eventSchedules);
                }
            }

            DateTime nextHit = DateTime.MaxValue;
            foreach (var schedule in eventSchedules)
            {
                var nextUTCTime = CrontabSchedule
                    .Parse(schedule)
                    .GetNextOccurrence(DateTime.UtcNow);

                var localNextHitTime = TimeZoneInfo.ConvertTimeFromUtc(nextUTCTime, TimeZoneInfo.Local);

                if (localNextHitTime < nextHit)
                {
                    nextHit = localNextHitTime;
                }
            }

            TimeSpan timeRemaining = TimeSpan.FromTicks(nextHit.Ticks - DateTime.Now.Ticks);

            var eventInfo = new EventCardInfo
            {
                Name = name,
                NextHit = nextHit,
                TimeRemaining = timeRemaining
            };

            return eventInfo;
        }

        public async Task<DateTime> GetNextEventTime(EventType eventType)
        {
            List<string> eventSchedules = eventType switch
            {
                EventType.TvT => await _dbContext.Events
                                        .Where(x => x.Name == "TvT")
                                        .Select(x => x.Schedule)
                                        .ToListAsync(),
                EventType.Massacre => await _dbContext.Events
                                        .Where(x => x.Name == "Мясорубка")
                                        .Select(x => x.Schedule)
                                        .ToListAsync(),
                EventType.Elpi => await _dbContext.Events
                                        .Where(x => x.Name == "Нашествие Эльпи")
                                        .Select(x => x.Schedule)
                                        .ToListAsync(),
                EventType.SPC => await _dbContext.Events
                                        .Where(x => x.Name == "Счастливые Полчаса")
                                        .Select(x => x.Schedule)
                                        .ToListAsync(),
                EventType.Tovy => await _dbContext.Events
                                        .Where(x => x.Name == "Сокровища Тову")
                                        .Select(x => x.Schedule)
                                        .ToListAsync(),
                _ => new List<string>(),
            };

            DateTime nextRun = DateTime.MaxValue;
            foreach (var schedule in eventSchedules)
            {
                var next = CrontabSchedule.Parse(schedule).GetNextOccurrence(DateTime.Now);
                if (next < nextRun)
                {
                    nextRun = next;
                }
            }

            return nextRun;
        }
    }
}