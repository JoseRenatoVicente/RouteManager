using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.Teams.Delete;

public sealed record DeleteTeamCommand(string Id) : IBaseCommand;