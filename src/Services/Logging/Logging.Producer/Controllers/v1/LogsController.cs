using System.Text;
using System.Text.Json;
using Logging.Producer.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Logging.Producer.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "messagelogs";

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
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var stringfieldMessage = JsonSerializer.Serialize(logRequest);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                    );
                }
            }
            return Ok();
        }
    }
}
