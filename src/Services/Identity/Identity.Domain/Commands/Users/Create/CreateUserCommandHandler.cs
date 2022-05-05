using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using Identity.Domain.Validations.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Users.Create;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(INotifier notifier, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper) : base(notifier)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role!.Id);
        if (!ExecuteValidation(new UserValidation(), user)) return new Response();

        if (await _userRepository.FindAsync(c => c.UserName == user.UserName || c.Email == user.Email) != null)
        {
            Notification("Nome de usuário ou email indisponíveis");
            return null;
        }


        var passwordResult = await CreatePasswordHashAsync(user.Password!);

        user.Password = passwordResult.passwordHash;
        user.PasswordSalt = passwordResult.passwordSalt;

         await _userRepository.AddAsync(user);
        return new Response { Entity = user };
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