namespace Domain.Entities
{
    using Domain.Common;
    public class Article : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Contents { get; set; }
    }
}
