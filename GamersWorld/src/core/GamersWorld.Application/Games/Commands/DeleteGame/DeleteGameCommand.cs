using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Commands.DeleteGame;
public class DeleteGameCommand
    : IRequest
{
    public int GameId { get; set; }
}

public class DeleteGameCommandHandler
    : IRequestHandler<DeleteGameCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteGameCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.Games
              .Where(l => l.Id == request.GameId)
              .SingleOrDefaultAsync(cancellationToken);

        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }

        throw new GameNotFoundException(request.GameId);
    }
}