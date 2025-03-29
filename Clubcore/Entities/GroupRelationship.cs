namespace Clubcore.Entities
{
    public class GroupRelationship
    {
        public int ParentGroupId { get; set; }
        public Group ParentGroup { get; set; }

        public int ChildGroupId { get; set; }
        public Group ChildGroup { get; set; }
    }
}
