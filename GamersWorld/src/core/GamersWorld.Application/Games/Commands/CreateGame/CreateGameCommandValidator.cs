using FluentValidation;
using GamersWorld.Application.Common.Interfaces;

namespace GamersWorld.Application.Games.Commands.CreateGame;

public class CreateGameCommandValidator
    : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator(IApplicationDbContext context)
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .WithMessage("Title info required")
            .MaximumLength(50)
            .WithMessage("Invalid title. Too long!");

        RuleFor(v => v.Point).InclusiveBetween(0.0, 10.0).WithMessage("Invalid range!");
    }
}