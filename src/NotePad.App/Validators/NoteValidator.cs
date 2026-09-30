using FluentValidation;
using NotePad.App.Models;

namespace NotePad.App.Validators;

public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        RuleFor(note => note.Filename)
            .NotEmpty().WithMessage("Filename cannot be empty.");

        RuleFor(note => note.Text)
            .NotEmpty().WithMessage("Note text cannot be empty.");
    }
}