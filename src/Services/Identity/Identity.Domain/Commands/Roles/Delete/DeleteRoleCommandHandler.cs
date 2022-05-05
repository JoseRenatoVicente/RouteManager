using Identity.Domain.Contracts.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Roles.Delete;

public class DeleteRoleCommandHandler : CommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository, INotifier notifier) : base(notifier)
    {
        _roleRepository = roleRepository;
    }

    public override async Task<Response> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindAsync(roleFilter => roleFilter.Id == request.Id);

        if (role == null)
        {
            Notification("Função não encontrada");
            return new Response();
        }

        await _roleRepository.RemoveAsync(role);

        return new Response();
    }
}