using AutoMapper;
using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using Identity.Domain.Validations.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Identity.Domain.Commands.Roles.Create;

public class CreateRoleCommandHandler : CommandHandler<CreateRoleCommand>
{

    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IMapper mapper, IRoleRepository roleRepository, INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public override async Task<Response> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request);

        if (!ExecuteValidation(new RoleValidation(), role)) return new Response();

        await _roleRepository.AddAsync(role);
        return new Response { Entity = role };
    }
}