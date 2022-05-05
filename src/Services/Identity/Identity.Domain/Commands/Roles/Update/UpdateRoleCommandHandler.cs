using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using Identity.Domain.Validations.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Roles.Update;

public class UpdateRoleCommandHandler : CommandHandler<UpdateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request);

        if (!ExecuteValidation(new RoleValidation(), role)) return new Response();

        var roleBefore = await _roleRepository.FindAllAsync(roleFilter => roleFilter.Id == role.Id);

        if (roleBefore == null)
        {
            Notification("Função não encontrada");
            return new Response();
        }

        var filterDefinition = Builders<User>.Filter.Eq(p => p.Role!.Id, role.Id);
        var updateDefinition = Builders<User>.Update.Set(p => p.Role, role);

        await _userRepository.UpdateAllAsync(filterDefinition, updateDefinition);
        await _roleRepository.UpdateAsync(role);
        return new Response { Entity = role };
    }
}