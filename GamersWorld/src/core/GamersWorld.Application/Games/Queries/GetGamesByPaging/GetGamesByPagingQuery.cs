using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Queries.GetGamesByPaging;

public class GetGamesByPagingQuery
    : IRequest<GamesByPagingViewModel>
{
    public int PageNo { get; set; }
    public int Count { get; set; }
}

public class GetGamesByPagingQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetGamesByPagingQuery, GamesByPagingViewModel>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<GamesByPagingViewModel> Handle(GetGamesByPagingQuery request, CancellationToken cancellationToken)
    {
        GamesByPagingViewModel gamesByPagingViewModel = new()
        {
            PageNo = request.PageNo,
            Count = request.Count,
            GameList = await
                _context
                    .Games
                    .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                    .Skip(request.PageNo)
                    .Take(request.Count)
                    .OrderBy(g => g.Title)
                    .ToListAsync(cancellationToken)
        };
        return gamesByPagingViewModel;
    }
}