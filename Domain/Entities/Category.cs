namespace Domain.Entities
{
    public class Category : BaseEntity, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<Product>? Products { get; set; }
    }
}
