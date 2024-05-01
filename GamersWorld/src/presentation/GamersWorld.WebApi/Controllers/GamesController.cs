using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.Application.Games.Commands.DeleteGame;
using GamersWorld.Application.Games.Commands.MoveToArchiveGame;
using GamersWorld.Application.Games.Commands.UpdateGame;
using GamersWorld.Application.Games.Queries.ExportGames;
using GamersWorld.Application.Games.Queries.GetArchivedGames;
using GamersWorld.Application.Games.Queries.GetGames;
using GamersWorld.Application.Games.Queries.GetGamesByPaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamersWorld.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IMediator mediator, ILogger<GamesController> logger)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<GamesController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<GamesViewModel>> Get()
    {
        return await _mediator.Send(new GetGamesQuery());
    }

    [HttpGet("{pageNo}/{count}")]
    public async Task<ActionResult<GamesByPagingViewModel>> Get(int pageNo, int count)
    {
        GetGamesByPagingQuery query = new()
        {
            PageNo = pageNo,
            Count = count
        };
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateGameCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception excp)
        {
            return BadRequest(excp.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(new DeleteGameCommand { GameId = id });
            return NoContent();

        }
        catch (GameNotFoundException excp)
        {
            return NotFound(excp.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateGameCommand command)
    {
        try
        {
            _logger.LogInformation($"{id} is moving to archive");
            if (id != command.GameId)
                return BadRequest();

            await _mediator.Send(command);

        }
        catch (Exception excp)
        {
            return BadRequest(excp.Message);
        }
        return NoContent();
    }

    [HttpPut("archive/{id}")]
    public async Task<ActionResult> MoveToArchive(int id, MoveToArchiveGameCommand command)
    {
        try
        {
            if (id != command.GameId)
                return BadRequest();

            await _mediator.Send(command);

        }
        catch (Exception excp)
        {
            return BadRequest(excp.Message);
        }
        return NoContent();
    }

    [HttpGet("/export/csv")]
    public async Task<ActionResult<ExportGamesViewModel>> Export()
    {
        return await _mediator.Send(new ExportGamesQuery());
    }

    [HttpGet("archive")]
    public async Task<ActionResult<ArchivedGamesViewModel>> GetArchivedGames()
    {
        GetArchivedGamesQuery query = new();
        return await _mediator.Send(query);
    }
}