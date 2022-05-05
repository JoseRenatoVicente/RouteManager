using AutoMapper;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;
using Teams.Domain.Contracts.v1;
using Teams.Domain.Entities.v1;
using Teams.Domain.Validations.v1;

namespace Teams.Domain.Commands.People.Create;

public class CreatePersonCommandHandler : CommandHandler<CreatePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository, INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _personRepository = personRepository;
    }

    public override async Task<Response> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);

        if (!ExecuteValidation(new PersonValidation(), person)) return new Response();

        await _personRepository.AddAsync(person);
        return new Response { Entity = person };
    }
}