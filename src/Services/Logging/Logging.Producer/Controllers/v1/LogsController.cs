using Logging.Producer.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Logging.Producer.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly ConnectionFactory _factory;
    private const string QueueName = "messagelogs";

    public LogsController(ConnectionFactory factory)
    {
        _factory = factory;
    }


    [Authorize(Roles = "Logs")]
    [HttpPost]
    public IActionResult PostLog(LogRequest logRequest)
    {
        using (var connection = _factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(
                    queue: QueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var stringfieldMessage = JsonSerializer.Serialize(logRequest);
                var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: QueueName,
                    basicProperties: null,
                    body: bytesMessage
                );
            }
        }
        return Ok();
    }
}