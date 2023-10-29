namespace InstTracker.Data.Model
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<InstanceHistory> InstancesHistory { get; set; }
    }
}
