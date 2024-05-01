using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Queries.GetArchivedGames;

public class GetArchivedGamesQuery
    : IRequest<ArchivedGamesViewModel>
{
}

public class GetArchivedGamesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetArchivedGamesQuery, ArchivedGamesViewModel>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<ArchivedGamesViewModel> Handle(GetArchivedGamesQuery request, CancellationToken cancellationToken)
    {
        ArchivedGamesViewModel gamesViewModel = new()
        {
            GameList = await
                _context
                    .Games
                    .Where(g => g.IsArchived)
                    .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                    .OrderBy(g => g.Title)
                    .ToListAsync(cancellationToken)
        };
        return gamesViewModel;
    }
}