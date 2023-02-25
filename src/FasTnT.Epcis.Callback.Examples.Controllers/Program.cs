using FasTnT.Epcis.Callback.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddEpcisCallback(opt => opt.RegisterBaseEventTypes());  // Register the default EPCIS eventTypes

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
