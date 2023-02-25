using FasTnT.Epcis.Callback.Api.Binding;
using FasTnT.Epcis.Callback.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FasTnT.Epcis.Callback.Api.Extensions;

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

    public static IMvcBuilder AddEpcisCallback(this IMvcBuilder mvcBuilder, Action<EpcisParserOptions> options = null)
    {
        var parserOptions = new EpcisParserOptions();

        if (options is not null)
        {
            options(parserOptions);
        }

        mvcBuilder.Services.AddSingleton(parserOptions);
        mvcBuilder.AddMvcOptions(opt => opt.ModelBinderProviders.Insert(0, new EpcisCallbackBinderProvider()));

        return mvcBuilder;
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
