using FluentValidation;
using GamersWorld.Application.Common.Interfaces;

namespace GamersWorld.Application.Games.Commands.UpdateGame;

public class UpdateGameCommandValidator
    : AbstractValidator<UpdateGameCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateGameCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .WithMessage("Title info required")
            .MaximumLength(50)
            .WithMessage("Invalid title. Too long!");

        RuleFor(v => v.Point).InclusiveBetween(0.0, 10.0).WithMessage("Invalid range!");
    }
}