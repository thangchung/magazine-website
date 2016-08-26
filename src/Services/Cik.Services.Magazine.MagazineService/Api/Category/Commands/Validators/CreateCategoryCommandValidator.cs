using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Domain;
using FluentValidation;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>,
        ICommandValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty().Length(10);
        }
    }
}