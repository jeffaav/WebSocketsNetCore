using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Net.WebSockets;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;

namespace WebSocketsNetCore.Middlewares
{
    public class WebsocketMiddleware
    {
        private readonly RequestDelegate next;
        private readonly MongoClient client;

        private static ConcurrentBag<WebSocket> websocketsBag;

        static WebsocketMiddleware()
        {
            websocketsBag = new ConcurrentBag<WebSocket>();
        }

        public WebsocketMiddleware(RequestDelegate next, MongoClient client)
        {
            this.next = next;
            this.client = client;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var websocket = await context.WebSockets.AcceptWebSocketAsync();

                websocketsBag.Add(websocket);

                while (websocket.State == WebSocketState.Open)
                {
                    
                }
            }

            await next(context);
        }
    }

    public static class WebsocketMiddlewareExtensions
    {
        public static void UseWebsocketMiddleware(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseMiddleware<WebsocketMiddleware>();
        }
    }
}