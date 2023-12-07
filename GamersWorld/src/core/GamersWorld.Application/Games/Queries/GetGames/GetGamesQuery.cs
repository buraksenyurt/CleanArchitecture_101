using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Queries.GetGames;

public class GetGamesQuery
    : IRequest<GamesViewModel>
{
}

public class GetGamesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetGamesQuery, GamesViewModel>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<GamesViewModel> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        GamesViewModel gamesViewModel = new()
        {
            GameList = await 
                _context
                    .Games
                    .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                    .OrderBy(g => g.Title)                    
                    .ToListAsync(cancellationToken)
        };
        return gamesViewModel;
    }
}