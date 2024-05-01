using AutoMapper;
using GamersWorld.Application.Common.Mappings;
using GamersWorld.Domain.Entities;

namespace GamersWorld.Application.Dtos.Games;

public class GameDto
    : IMapFrom<Game>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Point { get; set; }
    public decimal ListPrice { get; set; }
    public short Status { get; set; }
    public bool IsArchived { get; set; }
    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<Game, GameDto>()
            .ForMember(
                dest => dest.Status
                , opt => opt.MapFrom(source => (short)source.Status)
        );
    }
}