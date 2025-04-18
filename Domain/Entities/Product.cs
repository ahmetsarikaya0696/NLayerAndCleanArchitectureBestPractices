﻿namespace Domain.Entities
{
    public class Product : BaseEntity, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
