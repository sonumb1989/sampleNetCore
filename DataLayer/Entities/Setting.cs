namespace DataLayer.Entities
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? TypeId { get; set; }
    }
}
