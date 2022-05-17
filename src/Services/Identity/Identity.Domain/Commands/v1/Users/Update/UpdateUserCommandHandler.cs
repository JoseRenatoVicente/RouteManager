using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.v1.Users.Update;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public UpdateUserCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper, INotifier notifier)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        var userBefore = await _userRepository.FindAsync(c => c.Id == user.Id);

        if (userBefore == null)
        {
            _notifier.Handle("Usuário não encontrado");
            return new Response();
        }

        user.Password = userBefore.Password;
        user.PasswordSalt = userBefore.PasswordSalt;
        user.Role ??= userBefore.Role;

        if (await _userRepository.FindAsync(c =>
                (c.UserName == user.UserName || c.Email == user.Email)
                && c.Id != user.Id) != null)

        {
            _notifier.Handle("Nome de usuário ou email indisponíveis");
            return new Response();
        }

        user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role!.Id);

        await _userRepository.UpdateAsync(user);
        return new Response { Content = user };
    }
}