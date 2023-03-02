using FasTnT.Epcis.Callback.Api.Binding;
using FasTnT.Epcis.Callback.Core.Parsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        services.AddSingleton(parserOptions.BuildParser());

        return services;
    }

    public static IMvcBuilder AddEpcisCallback(this IMvcBuilder mvcBuilder, Action<EpcisParserOptions> options = null)
    {
        var parserOptions = new EpcisParserOptions();

        if (options is not null)
        {
            options(parserOptions);
        }

        mvcBuilder.Services.AddSingleton(parserOptions.BuildParser());
        mvcBuilder.AddMvcOptions(opt => opt.ModelBinderProviders.Insert(0, new EpcisCallbackBinderProvider()));

        return mvcBuilder;
    }

    public static IEndpointRouteBuilder MapEpcisCallback(this IEndpointRouteBuilder routeBuilder, string path, Action<EpcisCallbackProviderBuilder> actions = null)
    {
        var builder = new EpcisCallbackProviderBuilder();

        if (actions is not null)
        {
            actions(builder);
        }

        var callbackProvider = builder.Build();

        routeBuilder.MapPost(path, (HttpContext context) => EpcisCallbackHandler.HandleCallback(context, callbackProvider));

        return routeBuilder;
    }

    public static IEndpointRouteBuilder MapEpcisCallback(this IEndpointRouteBuilder routeBuilder, string path, Delegate action)
    {
        var callbackProvider = new SingleEpcisCallbackProvider(action);

        routeBuilder.MapPost(path, (HttpContext context) => EpcisCallbackHandler.HandleCallback(context, callbackProvider));

        return routeBuilder;
    }
}
