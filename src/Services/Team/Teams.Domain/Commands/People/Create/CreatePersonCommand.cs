using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.People.Create;

public sealed class CreatePersonCommand : IBaseCommand
{
    public string? Name { get; set; }
}