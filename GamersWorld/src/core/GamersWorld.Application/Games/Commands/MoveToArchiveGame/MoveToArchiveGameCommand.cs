using GamersWorld.Application.Common.Interfaces;
using MediatR;

namespace GamersWorld.Application.Games.Commands.MoveToArchiveGame;

public class MoveToArchiveGameCommand
    : IRequest<int>
{
    public int GameId { get; set; }
}

public class MoveToArchiveGameCommandHandler(IArchiveService context)
        : IRequestHandler<MoveToArchiveGameCommand, int>
{
    private readonly IArchiveService _context = context;

    public async Task<int> Handle(MoveToArchiveGameCommand request, CancellationToken cancellationToken)
    {
        var updated = await _context.MoveAsync(request.GameId);
        return updated;
    }
}