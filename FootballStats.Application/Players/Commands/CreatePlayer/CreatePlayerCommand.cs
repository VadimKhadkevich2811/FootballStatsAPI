using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Domain.Enums;
using MediatR;

namespace FootballStats.Application.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : IRequest<Response<PlayerReadDto>>
{
    public string Name { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public int Age { get; set; }
    public string Nationality { get; set; } = default!;
    public PositionGroup Position { get; set; }
}