using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.Application.Games.Commands.DeleteGame;
using GamersWorld.Application.Games.Commands.UpdateGame;
using GamersWorld.Application.Games.Queries.GetGames;
using GamersWorld.Application.Games.Queries.GetGamesByPaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamersWorld.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

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
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteGameCommand { GameId = id });
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateGameCommand command)
    {
        if (id != command.GameId)
            return BadRequest();

        await _mediator.Send(command);

        return NoContent();
    }
}