using App.Repositories.Products;

namespace App.Repositories.Categories
{
    public class Category : BaseEntity, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<Product>? Products { get; set; }
    }
}
