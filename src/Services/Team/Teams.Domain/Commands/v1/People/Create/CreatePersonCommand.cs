using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.v1.People.Create;

public sealed record CreatePersonCommand(string Name) : IBaseCommand;