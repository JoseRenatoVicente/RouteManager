using AutoMapper;
using Logging.Domain.Contracts.v1;
using Logging.Domain.Entities.v1;
using Logging.Domain.Validations.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Logging.Domain.Commands.Create;

public class CreateLogCommandHandler : CommandHandler<CreateLogCommand>
{
    private readonly ILogRepository _logRepository;
    private readonly IMapper _mapper;

    public CreateLogCommandHandler(INotifier notifier, ILogRepository logRepository, IMapper mapper) : base(notifier)
    {
        _logRepository = logRepository;
        _mapper = mapper;
    }

    public override async Task<Response> Handle(CreateLogCommand request, CancellationToken cancellationToken)
    {
        var log = _mapper.Map<Log>(request);

        if (!ExecuteValidation(new LogValidation(), log)) return new Response();

        await _logRepository.AddAsync(log);
        return new Response { Entity = log };
    }
}