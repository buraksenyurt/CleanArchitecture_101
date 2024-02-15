using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Domain.Enums;
using MediatR;

namespace GamersWorld.Application.Games.Commands.UpdateGame;

public class UpdateGameCommand
    : IRequest<int>
{
    public int GameId { get; set; }
    public string Title { get; set; }
    public short Status { get; set; }
    public double Point { get; set; }
    public decimal ListPrice { get; set; }
    public Guid ImageId { get; set; }
}

public class UpdateGameCommandHandler(IApplicationDbContext context, IImageHandler imageHandler)
        : IRequestHandler<UpdateGameCommand, int>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IImageHandler _imageHandler = imageHandler;

    public async Task<int> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var image = await _imageHandler.LoadWithGuidAsync(request.ImageId);

        var gm = await _context.Games.FindAsync(request.GameId, cancellationToken);
        if (gm != null)
        {
            gm.Title = request.Title;
            gm.Status = (Status)request.Status;
            gm.Point = request.Point;
            gm.ListPrice = request.ListPrice;
            gm.Image = image.Content;

            await _context.SaveChangesAsync(cancellationToken);

            return request.GameId;
        }

        throw new GameNotFoundException(request.GameId);
    }
}