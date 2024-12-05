using FluentValidation;

namespace Application.Features.Categories.Update
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori ismi gereklidir")
                .Length(3, 10).WithMessage("Kategori ismi 3-10 karakter arasında olmalıdır");
        }
    }
}
