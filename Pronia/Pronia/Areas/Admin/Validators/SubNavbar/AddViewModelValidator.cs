using System;
using FluentValidation;
using Pronia.Areas.Admin.ViewModels.SubNavbar;

namespace Pronia.Areas.Admin.Validators.SubNavbar
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(n => n.Name)
                .NotNull()
                .WithMessage("Navbar's name cannot be empty!")
                .NotEmpty()
                .WithMessage("Navbar's name cannot be empty!")
                .MinimumLength(3)
                .WithMessage("Navbar's minimum length should be 3!")
                .MaximumLength(30)
                .WithMessage("Navbar's maximum length should be 30!");

            RuleFor(n => n.URL)
                .NotNull()
                .WithMessage("Navbar's URL cannot be empty!")
                .NotEmpty()
                .WithMessage("Navbar's URL cannot be empty!")
                .MinimumLength(10)
                .WithMessage("URL's minimum length should be 10!")
                .MaximumLength(40)
                .WithMessage("URL's maximum length should be 40!");
        }
    }
}