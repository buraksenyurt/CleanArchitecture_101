using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Commands.MoveToArchiveGame;
using GamersWorld.Application.Games.Queries.GetArchivedGames;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamersWorld.RazorApp.Pages.Games;

public class ArchiveModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    public IList<GameDto> ArchivedGames { get; set; } = [];
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetArchivedGamesQuery());
            ArchivedGames = result.GameList;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load archived games: {ex.Message}";
        }
    }

    public async Task<IActionResult> OnPostMoveAsync(int id)
    {
        try
        {
            await _mediator.Send(new MoveToArchiveGameCommand { GameId = id });
            return RedirectToPage("./Archive");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to archive game: {ex.Message}";
            var result = await _mediator.Send(new GetArchivedGamesQuery());
            ArchivedGames = result.GameList;
            return Page();
        }
    }
}
