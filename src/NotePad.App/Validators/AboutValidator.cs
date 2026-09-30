using FluentValidation;
using NotePad.App.Models;

namespace NotePad.App.Validators;

public class AboutValidator : AbstractValidator<About>
{
    public AboutValidator()
    {
        RuleFor(about => about.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(about => about.Version)
            .NotEmpty().WithMessage("Version is required.");

        RuleFor(about => about.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(about => about.MoreInfoUrl)
            .NotEmpty().WithMessage("URL is required.");
    }
}