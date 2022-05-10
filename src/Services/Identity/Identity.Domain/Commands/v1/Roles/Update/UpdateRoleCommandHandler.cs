using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using MongoDB.Driver;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.v1.Roles.Update;

public sealed class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly INotifier _notifier;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper, INotifier notifier)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<Response> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request);

        var roleBefore = await _roleRepository.FindAllAsync(roleFilter => roleFilter.Id == role.Id);

        if (roleBefore == null)
        {
            _notifier.Handle("Função não encontrada");
            return new Response();
        }

        var filterDefinition = Builders<User>.Filter.Eq(p => p.Role!.Id, role.Id);
        var updateDefinition = Builders<User>.Update.Set(p => p.Role, role);

        await _userRepository.UpdateAllAsync(filterDefinition, updateDefinition);
        await _roleRepository.UpdateAsync(role);
        return new Response { Entity = role };
    }
}