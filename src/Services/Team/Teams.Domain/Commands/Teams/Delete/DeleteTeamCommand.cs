using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.Teams.Delete;

public sealed class DeleteTeamCommand : IBaseCommand
{
    public string? Id { get; set; }
}