This repository contains utility classes and parsers for GS1 EPCIS 2.0 subscription callback endpoints. Supports XML and JSON formats.

# Usage

TODO

## Using MVC

```cs
[ApiController]
[Route("[controller]")]
public class EpcisCallbackController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] SubscriptionCallback callback)
    {
        Debug.Assert(callback is not null);

        return NoContent();
    }
}
```

## Using Minimal API
