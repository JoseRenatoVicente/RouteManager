using AutoMapper;
using Bogus;
using Logging.Domain.Commands.v1.CreateLogging;
using Logging.Domain.Contracts.v1;
using Logging.Domain.Entities.v1;
using Moq;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Logging.UnitTests.Commands.v1.CreateLogging;

public class CreateLogCommandHandlerTest
{
    private readonly INotifier _notifier;
    private readonly IMapper _mapper;

    public CreateLogCommandHandlerTest()
    {
        _notifier = new Notifier();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CreateLogCommandProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact(DisplayName = "Should save a Log in repository when handle command")]
    public async Task ShouldSaveLogInRepositoryWhenHandleCommand()
    {
        var mockRepository = new Mock<ILogRepository>();
        var mockLog = new Faker<Log>()
            .Generate();
        var mockCreateLogCommand = new Faker<CreateLogCommand>()
            .Generate();

        mockRepository
            .Setup(x => x.FindAsync(It.IsAny<Expression<Func<Log, bool>>>()))
            .ReturnsAsync(mockLog);

        var handler = new CreateLogCommandHandler(mockRepository.Object, _mapper);

        await handler.Handle(mockCreateLogCommand, CancellationToken.None);

        Assert.Empty(_notifier.GetNotifications());
    }
}