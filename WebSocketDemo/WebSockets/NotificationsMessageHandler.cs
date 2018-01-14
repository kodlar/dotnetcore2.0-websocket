using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;


namespace WebSocketDemo
{
    public class NotificationsMessageHandler: WebSocketHandler
    {
        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string message = Encoding.UTF8.GetString(buffer,0,result.Count);
            await SendMessageToAllAsync(message);
        }
    }
}
