using AutoMapper;
using Logging.Domain.Contracts.v1;
using Logging.Domain.Entities.v1;
using RouteManager.Domain.Core.Handlers;
using RouteManager.WebAPI.Core.Notifications;

namespace Logging.Domain.Commands.v1.CreateLogging;

public sealed class CreateLogCommandHandler : ICommandHandler<CreateLogCommand>
{
    private readonly ILogRepository _logRepository;
    private readonly IMapper _mapper;

    public CreateLogCommandHandler(ILogRepository logRepository, IMapper mapper)
    {
        _logRepository = logRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(CreateLogCommand request, CancellationToken cancellationToken)
    {
        var log = _mapper.Map<Log>(request);

        await _logRepository.AddAsync(log);
        return new Response { Content = log };
    }
}