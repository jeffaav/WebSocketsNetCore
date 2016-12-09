using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Net.WebSockets;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace WebSocketsNetCore.Middlewares
{
    public class WebsocketMiddleware
    {
        private readonly RequestDelegate next;
        private readonly MongoClient client;

        private static IList<WebSocket> websocketsBag;

        static WebsocketMiddleware()
        {
            websocketsBag = new List<WebSocket>();
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
                    var token = CancellationToken.None;

                    var buffer = new ArraySegment<byte>(new byte[4096]);
                    var result = await websocket.ReceiveAsync(buffer, token);


                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            var message = buffer.GetString();
                            buffer = message.GetBytes();
                            foreach (var ws in websocketsBag)
                            {
                                if (ws.State == WebSocketState.Open)
                                    await ws.SendAsync(buffer, WebSocketMessageType.Text, true, token);
                            }
                            break;
                        // case WebSocketMessageType.Close:
                        //     Console.WriteLine("connection closed");
                        //     break;
                    }
                }
            }
            else
            {
                await next(context);
            }
        }
    }

    public static class WebsocketMiddlewareExtensions
    {
        public static void UseWebsocketMiddleware(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseMiddleware<WebsocketMiddleware>();
        }

        public static string GetString(this ArraySegment<byte> buffer)
        {
            return Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
        }

        public static ArraySegment<byte> GetBytes(this string text)
        {
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(text));
        }
    }
}