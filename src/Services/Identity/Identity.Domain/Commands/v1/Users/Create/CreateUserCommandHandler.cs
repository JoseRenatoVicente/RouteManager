using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Domain.Commands.v1.Users.Create;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public CreateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, INotifier notifier)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role!.Id);

        if (await _userRepository.FindAsync(c => c.UserName == user.UserName || c.Email == user.Email) != null)
        {
            _notifier.Handle("Nome de usuário ou email indisponíveis");
            return new Response();
        }


        var passwordResult = await CreatePasswordHashAsync(user.Password!);

        user.Password = passwordResult.passwordHash;
        user.PasswordSalt = passwordResult.passwordSalt;

        await _userRepository.AddAsync(user);
        return new Response { Content = user };
    }

    private async Task<(string passwordSalt, string passwordHash)> CreatePasswordHashAsync(string password)
    {
        using var hmac = new HMACSHA256();
        return (Convert.ToBase64String(hmac.Key),
            Convert.ToBase64String(await hmac.ComputeHashAsync(
                new MemoryStream(Encoding.ASCII.GetBytes(password)
                ))));
    }


}