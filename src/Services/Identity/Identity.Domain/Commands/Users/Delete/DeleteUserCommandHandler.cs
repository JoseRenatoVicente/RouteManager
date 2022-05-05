using Identity.Domain.Contracts.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Users.Delete;

public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand>
{
    private readonly IAspNetUser _aspNetUser;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(INotifier notifier, IAspNetUser aspNetUser, IUserRepository userRepository) : base(notifier)
    {
        _aspNetUser = aspNetUser;
        _userRepository = userRepository;
    }

    public override async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(userFilter => userFilter.Id == request.Id);
        user.Active = false;

        if (user.Id == _aspNetUser.GetUserId())
        {
            Notification("Não é possivel excluir o proprio usuário logado nessa sessão");
            return new Response();
        }

        await _userRepository.UpdateAsync(user);

        return new Response();
    }
}