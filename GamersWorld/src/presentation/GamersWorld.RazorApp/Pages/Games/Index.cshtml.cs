using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Queries.GetGames;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamersWorld.RazorApp.Pages.Games;

public class IndexModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    public IList<GameDto> Games { get; set; } = [];
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetGamesQuery());
            Games = result.GameList;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load games: {ex.Message}";
        }
    }
}
