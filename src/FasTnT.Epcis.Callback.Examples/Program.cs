using FasTnT.Epcis.Callback.Core;
using FasTnT.Epcis.Callback.Core.Binding;
using FasTnT.Epcis.Callback.Core.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes()); // Register the default EPCIS eventTypes

var app = builder.Build();
// Allow to separate the actions based on the subscriptionID
app.MapEpcisCallback("/v2_0/callback", opt =>
{
    opt.OnCallback("allObjectEvents", async (IEnumerable<EpcisEvent> events) => { await Task.Delay(500); });
});

// Minimal API endpoint binding - will receive callbacks for any subscriptionID
app.MapPost("/v2_0/anyCallback", (EpcisCallback callback) => Results.Ok($"Received {callback.Events.Count()} event(s) for subscription {callback.SubscriptionId} from EPCIS server"));

app.Run();
