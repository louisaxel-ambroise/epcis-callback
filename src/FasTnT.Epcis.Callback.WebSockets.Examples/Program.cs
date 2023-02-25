using FasTnT.Epcis.Callback.Core.Extensions;
using FasTnT.Epcis.Callback.Core.Parsers;
using System.Net.WebSockets;

Console.Title = "Client";

using var ws = new ClientWebSocket();
var url = new Uri("wss://localhost:5001/v2_0/queries/AllObjectEvents/events?stream=true&auth=YWRtaW46UEBzc3cwcmQ=");
var parser = new EpcisParserOptions().RegisterBaseEventTypes().BuildParser();

await ws.ConnectAsync(url, CancellationToken.None);

if (ws.State == WebSocketState.Open)
{
    Console.WriteLine($"Connected");

    while (ws.State == WebSocketState.Open)
    {
        using var stream = await ReceiveStream(ws);

        var callback = await JsonCallbackParser.ParseAsync(stream, parser, default);
        Console.WriteLine($"Received {callback.Events.Count()} event(s)");
    }

    Console.WriteLine($"Disconnected");
}

static async Task<Stream> ReceiveStream(ClientWebSocket ws)
{
    WebSocketReceiveResult result;
    var memStream = new MemoryStream();
    var data = new byte[4096];
    var buffer = new ArraySegment<byte>(data);

    do
    {
        result = await ws.ReceiveAsync(buffer, CancellationToken.None);

        if (result.Count > 0)
        {
            memStream.Write(buffer.Array, buffer.Offset, result.Count);
        }
    } while (!result.EndOfMessage); // check end of message mark

    memStream.Position = 0;

    return memStream;
}