using Identity.Domain.Contracts.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.v1.Users.Delete;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IAspNetUser _aspNetUser;
    private readonly IUserRepository _userRepository;
    private readonly INotifier _notifier;

    public DeleteUserCommandHandler(IAspNetUser aspNetUser, IUserRepository userRepository, INotifier notifier)
    {
        _aspNetUser = aspNetUser;
        _userRepository = userRepository;
        _notifier = notifier;
    }

    public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(userFilter => userFilter.Id == request.Id);
        user.Active = false;

        if (user.Id == _aspNetUser.GetUserId())
        {
            _notifier.Handle("Não é possivel excluir o proprio usuário logado nessa sessão");
            return new Response();
        }

        await _userRepository.UpdateAsync(user);

        return new Response();
    }
}