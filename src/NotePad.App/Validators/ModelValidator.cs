using FluentValidation;
using FluentValidation.Results;

namespace NotePad.App.Validators;

public class ModelValidator
{
    public static ValidationResult Validate<T>(T model, AbstractValidator<T> validator)
    {
        return validator.Validate(model);
    }
}