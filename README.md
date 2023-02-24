This repository contains utility classes and parsers for GS1 EPCIS 2.0 subscription callback endpoints using an ASP.NET Core API. 
It allows to parse the JSON callbacks with extensibility and with the custom namespaces.

# Usage

This subscription callback can be used with either ASP.NET Core Minimal API or with the ASP.NET Core MVC Controllers.

## Using ASP.NET Core MVC

The different event types needs to be registered using the `UseEpcisCallback` extension method:

```cs
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes());
```

Then retrieving a callback from HTTP is as simple as mapping a POST endpoint with a `EpcisCallback` parameter, like so:

```cs
[ApiController]
[Route("[controller]")]
public class EpcisCallbackController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] EpcisCallback callback, CancellationToken cancellationToken)
    {
        // TODO: do something with the callback

        return NoContent();
    }
}
```

## Using Minimal API

The different event types needs to be registered using the `UseEpcisCallback` extension method:

```cs
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes());
```

Then the `EpcisCallback` type can be used as a parameter from any POST endpoint:

```cs
app.MapPost("v2_0/subscriptionCallback", (EpcisCallback callback) => { /* Do something with the callback */ });
```

The `MapEpcisCallback` method allows to easily configure a different method depending on the Subscription ID value:

```cs
app.MapEpcisCallback("v2_0/subscriptionCallback", opt => 
{
	opt.OnCallback("aggregationSubscription", (EpcisCallback callback, IAggregationManager manager, CancellationToken cancellationToken) => manager.Register(callback.Events, cancellationToken));
	opt.OnCallback("allEventsSubscription", (IEnumerable<EpcisEvent> events) => { /* Do something with the callback */ });
});
```

Using this extension method, either the `EpcisCallback` or the `IEnumerable<EpcisEvent>` can be used as parameter binding. As the subscriptionID value is already used as filter, accepting the event list can make the code more readable.

# EventType extensions

*TODO*

# Authors

This project was created an is primarily maintained by Louis-Axel Ambroise. External contributions are welcome from anyone. 

# License

This project is licensed under the Apache 2.0 license - see the LICENSE file for details

Contact: fastnt@pm.me
