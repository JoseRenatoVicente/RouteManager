using RouteManager.Domain.Core.Contracts;

namespace Teams.Domain.Commands.People.Update;

public sealed class UpdatePersonCommand : IBaseCommand
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}