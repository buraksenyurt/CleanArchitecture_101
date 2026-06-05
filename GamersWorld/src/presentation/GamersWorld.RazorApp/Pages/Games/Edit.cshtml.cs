using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Games.Commands.UpdateGame;
using GamersWorld.Application.Games.Queries.GetGameById;
using GamersWorld.RazorApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamersWorld.RazorApp.Pages.Games;

public class EditModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    [BindProperty]
    public UpdateGameRequest GameRequest { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var game = await _mediator.Send(new GetGameByIdQuery { GameId = id });

            GameRequest = new UpdateGameRequest
            {
                GameId = game.Id,
                Title = game.Title,
                Status = game.Status,
                Point = game.Point,
                ListPrice = game.ListPrice,
                IsArchived = game.IsArchived,
                ImageId = Guid.Empty
            };
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            await _mediator.Send(new UpdateGameCommand
            {
                GameId = GameRequest.GameId,
                Title = GameRequest.Title,
                Status = GameRequest.Status,
                Point = GameRequest.Point,
                ListPrice = GameRequest.ListPrice,
                IsArchived = GameRequest.IsArchived,
                ImageId = GameRequest.ImageId
            });
            return RedirectToPage("./Index");
        }
        catch (GameNotFoundException)
        {
            return NotFound();
        }
        catch (ValidationException ex)
        {
            foreach (var (field, messages) in ex.Errors)
                foreach (var msg in messages)
                    ModelState.AddModelError(field, msg);
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to update game: {ex.Message}";
            return Page();
        }
    }
}
