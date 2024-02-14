using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamersWorld.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Games.Queries.ExportGames;

public class ExportGamesQuery
    : IRequest<ExportGamesViewModel>
{
}
public class ExportGamesQueryHandler(IApplicationDbContext context, IMapper mapper, IExportBuilder csvBuilder)
        : IRequestHandler<ExportGamesQuery, ExportGamesViewModel>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IExportBuilder _csvBuilder = csvBuilder;

    public async Task<ExportGamesViewModel> Handle(ExportGamesQuery request, CancellationToken cancellationToken)
    {
        var result = new ExportGamesViewModel();

        var games = await _context.Games.ProjectTo<GameRecord>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        result.FileName = "Games.csv";
        result.ContentType = "text/csv";
        result.Content = _csvBuilder.BuildFile(games);

        return await Task.FromResult(result);
    }
}
