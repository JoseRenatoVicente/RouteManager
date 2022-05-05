using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using Identity.Domain.Validations.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Users.Update;

public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        var userBefore = await _userRepository.FindAsync(c => c.Id == user.Id);

        if (userBefore == null)
        {
            Notification("Usuário não encontrado");
            return new Response();
        }

        user.Password = userBefore.Password;
        user.PasswordSalt = userBefore.PasswordSalt;
        user.Role ??= userBefore.Role;

        if (await _userRepository.FindAsync(c =>
                (c.UserName == user.UserName || c.Email == user.Email)
                && c.Id != user.Id) != null)

        {
            Notification("Nome de usuário ou email indisponíveis");
            return new Response();
        }

        user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);

        if (!ExecuteValidation(new UserValidation(), user)) return new Response();

        await _userRepository.UpdateAsync(user);
        return new Response { Entity = user };
    }
}