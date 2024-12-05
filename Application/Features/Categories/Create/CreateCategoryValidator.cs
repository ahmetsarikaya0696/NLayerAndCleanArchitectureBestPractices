using FluentValidation;

namespace Application.Features.Categories.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori ismi gereklidir")
                .Length(3, 10).WithMessage("Kategori ismi 3-10 karakter arasında olmalıdır");
        }
    }
}
