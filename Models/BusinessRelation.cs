namespace Xo_Test.Models
{
    public class BusinessRelation
    {
        public int RelationId { get; set; }
        public int BusinessId1 { get; set; }
        public int BusinessId2 { get; set; }
        public string RelationType { get; set; }

        public Business Business1 { get; set; }
        public Business Business2 { get; set; }
    }
}
