using Identity.Domain.Contracts.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.v1.Roles.Delete;

public sealed class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly INotifier _notifier;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository, INotifier notifier)
    {
        _roleRepository = roleRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindAsync(roleFilter => roleFilter.Id == request.Id);

        if (role == null)
        {
            _notifier.Handle("Função não encontrada");
            return new Response();
        }

        await _roleRepository.RemoveAsync(role);

        return new Response();
    }
}