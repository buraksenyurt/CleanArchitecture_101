using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Domain.Entities;
using GamersWorld.Domain.Enums;
using MediatR;

namespace GamersWorld.Application.Games.Commands.CreateGame;

public class CreateGameCommand
    : IRequest<int>
{
    public string Title { get; set; }
    public short Status { get; set; }
    public double Point { get; set; }
    public decimal ListPrice { get; set; }
    public Guid ImageId { get; set; }
}

public class CreateGameCommandHandler(IApplicationDbContext context, IImageHandler imageHandler)
        : IRequestHandler<CreateGameCommand, int>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IImageHandler _imageHandler = imageHandler;

    public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var image = _imageHandler.LoadWithGuidAsync(request.ImageId);

        var newGame = new Game
        {
            Title = request.Title,
            Status = (Status)request.Status,
            Point = request.Point,
            ListPrice = request.ListPrice,
            Image = image
        };
        _context.Games.Add(newGame);

        await _context.SaveChangesAsync(cancellationToken);

        return newGame.Id;
    }
}