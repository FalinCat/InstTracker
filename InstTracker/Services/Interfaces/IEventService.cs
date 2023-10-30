using InstTracker.Data.Model;

namespace InstTracker.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventCardInfo> GetEventInfo(EventType eventType);
        Task<DateTime> GetNextEventTime(EventType eventType);
    }
}