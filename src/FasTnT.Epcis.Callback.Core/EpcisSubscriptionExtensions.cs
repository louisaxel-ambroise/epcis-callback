using FasTnT.Epcis.Callback.Core.Binding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FasTnT.Epcis.Callback.Core;

public static class EpcisSubscriptionExtensions
{
    public static IServiceCollection UseEpcisCallback(this IServiceCollection services, Action<EpcisParserOptions> options = null)
    {
        var parserOptions = new EpcisParserOptions();

        if (options is not null)
        {
            options(parserOptions);
        }

        services.AddSingleton(parserOptions);

        return services;
    }

    public static IEndpointRouteBuilder MapEpcisCallback(this IEndpointRouteBuilder routeBuilder, string path, Action<EpcisCallbackBuilder> actions = null)
    {
        var builder = new EpcisCallbackBuilder();

        if (actions is not null)
        {
            actions(builder);
        }

        routeBuilder.MapPost(path, builder.HandleCallback);

        return routeBuilder;
    }
}
