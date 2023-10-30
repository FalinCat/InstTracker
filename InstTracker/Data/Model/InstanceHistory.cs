namespace InstTracker.Data.Model
{
    public class InstanceHistory
    {
        public int Id { get; set; }

        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public int InstanceId { get; set; }
        public virtual Instance Instance { get; set; }

        public DateTime DateDone { get; set; }
    }
}