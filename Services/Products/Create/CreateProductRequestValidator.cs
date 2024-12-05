﻿using FluentValidation;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi gereklidir")
                .Length(3, 10).WithMessage("Ürün ismi 3-10 karakter arasında olmalıdır");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adeti 1-100 arasında olmalıdır");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori Id 0'dan büyük olmalıdır");
        }
    }
}