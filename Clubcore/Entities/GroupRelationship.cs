namespace Clubcore.Entities
{
    public class GroupRelationship
    {
        public Guid ParentGroupId { get; set; }
        public Guid ChildGroupId { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = [];
    }
}
