namespace InstTracker.Data.Model
{
    public class Instance
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Schedule { get; set; }

        public bool isHidden { get; set; } = false;

        public virtual List<InstanceHistory> InstancesHistory { get; set; } = new();
    }
}