using FasTnT.Epcis.Callback.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FasTnT.Epcis.Callback.Examples.Controllers.Controllers;

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