using System.Globalization;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IWebSocketManagerService _socketManagerService;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(IWebSocketManagerService socketManagerService, ILogger<NotificationsController> logger)
        {
            _socketManagerService = socketManagerService;
            _logger = logger;
        }

        [HttpGet("sub")]
        public async Task Subscribe()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {

                using WebSocket ws = await HttpContext.WebSockets.AcceptWebSocketAsync();

                string key = _socketManagerService.AddSocket(ws);

                await _socketManagerService.SendAsync(key,new WebSocketMessage
                {
                    MessageType = MessageType.ConnectionEvent,
                    Data = new WebSocketMessageData
                    {
                        Topic = "Connection",
                        Payload = $"Connection established {DateTime.UtcNow}"
                    }
                });

                _logger.LogInformation($"New connection: {key} {DateTime.UtcNow}");

                while (ws.State == WebSocketState.Open)
                {

                }

                await _socketManagerService.RemoveSocketAsync(key);

                //var socketFinishedTcs = new TaskCompletionSource<object>();

                //await socketFinishedTcs.Task;
            }
            else
            {
                throw new BadRequestException("Only web socket connections");
            }
        }

        [HttpPost("pub")]
        public async Task<IActionResult> Publish([FromBody] string msg)
        {
            await _socketManagerService.SendAsync(new WebSocketMessage
            {
                MessageType = MessageType.Text,
                Data = new WebSocketMessageData
                {
                    Topic = "Test",
                    Payload = msg
                }
            });

            return Ok(new {message = msg});
        }
    }
}
