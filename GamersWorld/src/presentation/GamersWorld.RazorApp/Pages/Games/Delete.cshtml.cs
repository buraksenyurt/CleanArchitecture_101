using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Dtos.Games;
using GamersWorld.Application.Games.Commands.DeleteGame;
using GamersWorld.Application.Games.Queries.GetGameById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamersWorld.RazorApp.Pages.Games;

public class DeleteModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    public GameDto? Game { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            Game = await _mediator.Send(new GetGameByIdQuery { GameId = id });
            return Page();
        }
        catch (GameNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load game: {ex.Message}";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            await _mediator.Send(new DeleteGameCommand { GameId = id });
            return RedirectToPage("./Index");
        }
        catch (GameNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to delete game: {ex.Message}";
            return Page();
        }
    }
}
