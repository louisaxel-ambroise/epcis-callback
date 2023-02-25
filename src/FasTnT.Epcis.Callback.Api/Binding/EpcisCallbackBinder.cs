using FasTnT.Epcis.Callback.Api.Extensions;
using FasTnT.Epcis.Callback.Core;
using FasTnT.Epcis.Callback.Core.Parsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace FasTnT.Epcis.Callback.Api.Binding;

internal class EpcisCallbackBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        if (bindingContext.ModelType != typeof(EpcisCallback))
        {
            return;
        }

        try
        {
            var callback = await BindAsync(bindingContext.HttpContext);
            bindingContext.Result = ModelBindingResult.Success(callback);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex.Message);
        }
    }

    public static ValueTask<EpcisCallback> BindAsync(HttpContext context)
    {
        var parser = context.RequestServices.GetRequiredService<EpcisParser>();

        if (context.Request.HasContentType("xml"))
        {
            return XmlCallbackParser.ParseAsync(context.Request.Body, parser, context.RequestAborted); // Assume v1.2
        }
        else if (context.Request.HasContentType("json"))
        {
            return JsonCallbackParser.ParseAsync(context.Request.Body, parser, context.RequestAborted); // Assume v2.0
        }

        throw new Exception("Unable to parse EpcisCallbeck: unknown content-type");
    }
}
