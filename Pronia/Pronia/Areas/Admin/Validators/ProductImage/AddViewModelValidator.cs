using System;
using FluentValidation;
using Pronia.Areas.Admin.ViewModels.ProductImage;
using Pronia.Contracts.ProductImage;
using Pronia.Validators;

namespace Pronia.Areas.Admin.Validators.ProductImage
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Image)
               .Cascade(CascadeMode.Stop)

               .NotNull()
               .WithMessage("Image can't be empty")

               .SetValidator(
                    new FileValidator(2, FileSizes.Mega,
                        FileExtensions.JPG.GetExtension(), FileExtensions.PNG.GetExtension())!);
        }
    }
}