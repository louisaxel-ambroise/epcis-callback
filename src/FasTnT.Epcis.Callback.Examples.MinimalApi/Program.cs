using FasTnT.Epcis.Callback.Api.Extensions;
using FasTnT.Epcis.Callback.Core.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseEpcisCallback(opt => opt.RegisterBaseEventTypes()); // Register the default EPCIS eventTypes

var app = builder.Build();

// Allow to separate the actions based on the subscriptionID
app.MapEpcisCallback("/epcis/callback", opt =>
{
    opt.OnCallback("allObjectEvents", async (IEnumerable<EpcisEvent> events) =>
    {
        await Task.Delay(500);
    });
});

app.Run();
