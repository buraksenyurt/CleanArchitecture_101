using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Queries.GetGameById;

public class GetGameByIdQuery : IRequest<GameDto>
{
    public int GameId { get; set; }
}

public class GetGameByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetGameByIdQuery, GameDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<GameDto> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        var game = await _context
            .Games
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

        if (game is null)
            throw new GameNotFoundException(request.GameId);

        return game;
    }
}
