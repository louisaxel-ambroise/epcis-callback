This repository contains utility classes and parsers for GS1 EPCIS 2.0 subscription callback endpoints using an ASP.NET Core API. 
It allows to parse the JSON callbacks with extensibility and with the custom namespaces.

# Usage

This subscription callback can be used with either ASP.NET Core Minimal API or with the ASP.NET Core MVC Controllers.

## Using ASP.NET Core MVC

The different event types needs to be registered using the `AddEpcisCallback` extension method after adding the controllers:

```cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddEpcisCallback(opt => opt.RegisterBaseEventTypes()); // Register the default EPCIS eventTypes

[...]
```

Then retrieving a callback from HTTP is as simple as mapping a POST endpoint with a `EpcisCallback` parameter, like so:

```cs
[ApiController]
[Route("[controller]")]
public class EpcisCallbackController : ControllerBase
{
    private readonly ILogger<EpcisCallbackController> _logger;

    public EpcisCallbackController(ILogger<EpcisCallbackController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] EpcisCallback callback)
    {
        _logger.LogInformation("Received {0} event(s) for subscription {1}", callback.Events.Count(), callback.SubscriptionId);

        return NoContent();
    }
}
```

## Using Minimal API

The different event types needs to be registered using the `UseEpcisCallback` extension method, then the `MapEpcisCallback` method allows to easily configure a different method depending on the Subscription ID value:

```cs
using FasTnT.Epcis.Callback.Api.Extensions;
using FasTnT.Epcis.Callback.Core.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes()); // Register the default EPCIS eventTypes

var app = builder.Build();
app.MapEpcisCallback("epcis/callback", opt => 
{
    opt.OnCallback("allEventsSubscription", (IEnumerable<EpcisEvent> events) => { /* Do something with the callback */ });
});

app.Run();
```

It is also possible to register a callback independently of the SubscriptionID value:

The different event types needs to be registered using the `UseEpcisCallback` extension method, then the `MapEpcisCallback` method allows to easily configure a different method depending on the Subscription ID value:

```cs
using FasTnT.Epcis.Callback.Api.Extensions;
using FasTnT.Epcis.Callback.Core.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes()); // Register the default EPCIS eventTypes

var app = builder.Build();

app.MapEpcisCallback("/epcis/anycallback", (EpcisCallback callback) => Results.Ok($"Received callback for subscription {callback.SubscriptionId}"));
app.Run();
```


Using these extension methods, either the `EpcisCallback` or the `IEnumerable<EpcisEvent>` can be used as parameter binding. As the subscriptionID value is already used as filter, accepting the event list directly can make the code more readable.

## Using WebSocket subscription

The `JsonCallbackParser.ParseAsync(Stream, EpcisParser, CancellationToken)` method can be used to parse a received message from a websocket connection. The EpcisParser can be created using the `EpcisParserOptions` class like this:

```cs
var parser = new EpcisParserOptions().RegisterBaseEventTypes().BuildParser();
```

# EventType extensions

*TODO*

# Authors

This project was created an is primarily maintained by Louis-Axel Ambroise. External contributions are welcome from anyone. 

# License

This project is licensed under the Apache 2.0 license - see the LICENSE file for details

Contact: fastnt@pm.me
