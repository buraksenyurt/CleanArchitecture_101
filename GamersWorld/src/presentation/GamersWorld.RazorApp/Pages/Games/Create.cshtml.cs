using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Games.Commands.CreateGame;
using GamersWorld.RazorApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamersWorld.RazorApp.Pages.Games;

public class CreateModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    [BindProperty]
    public CreateGameRequest GameRequest { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            await _mediator.Send(new CreateGameCommand
            {
                Title = GameRequest.Title,
                Status = GameRequest.Status,
                Point = GameRequest.Point,
                ListPrice = GameRequest.ListPrice,
                ImageId = Guid.Empty
            });
            return RedirectToPage("./Index");
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
            ErrorMessage = $"Failed to create game: {ex.Message}";
            return Page();
        }
    }
}
