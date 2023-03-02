using FasTnT.Epcis.Callback.Api.Extensions;
using FasTnT.Epcis.Callback.Core;
using FasTnT.Epcis.Callback.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FasTnT.Epcis.Callback.Api.Binding;

public class EpcisCallbackHandler
{
    public static async ValueTask<object> HandleCallback(HttpContext context, IEpcisCallbackProvider callbackProvider)
    {
        EpcisCallback callback;

        try
        {
            callback = await EpcisCallbackBinder.BindAsync(context);
        }
        catch
        {
            return Results.Problem(statusCode: 400);
        }

        var handler = callbackProvider.GetCallbackForSubscription(callback.SubscriptionId);

        return handler is not null
            ? await HandleCallbackAction(handler, callback, context)
            : Results.Conflict();
    }

    private static ValueTask<object> HandleCallbackAction(Delegate handler, EpcisCallback callback, HttpContext context)
    {
        try
        {
            var parameters = handler.Method.GetParameters();
            var paramList = new object[parameters.Length];

            for (int i = 0; i < paramList.Length; i++)
            {
                if (parameters[i].ParameterType.Name.Equals(nameof(callback.SubscriptionId), StringComparison.OrdinalIgnoreCase) && parameters[i].ParameterType == typeof(string))
                {
                    paramList[i] = callback.SubscriptionId;
                }
                else if (parameters[i].ParameterType.Name.Equals(nameof(callback.QueryName), StringComparison.OrdinalIgnoreCase) && parameters[i].ParameterType == typeof(string))
                {
                    paramList[i] = callback.QueryName;
                }
                else if (parameters[i].ParameterType == typeof(IEnumerable<EpcisEvent>))
                {
                    paramList[i] = callback.Events;
                }
                else if (parameters[i].ParameterType == typeof(List<EpcisEvent>))
                {
                    paramList[i] = callback.Events.ToList();
                }
                else if (parameters[i].ParameterType == typeof(EpcisEvent[]))
                {
                    paramList[i] = callback.Events.ToArray();
                }
                else if (parameters[i].ParameterType == typeof(EpcisCallback))
                {
                    paramList[i] = callback;
                }
                else if (parameters[i].ParameterType == typeof(CancellationToken))
                {
                    paramList[i] = context.RequestAborted;
                }
                else
                {
                    paramList[i] = context.RequestServices.GetRequiredService(parameters[i].ParameterType);
                }
            }

            return handler.DynamicInvoke(paramList).TryCastTask();
        }
        catch
        {
            return ValueTask.FromResult<object>(Results.Problem(statusCode: 500));
        }
    }
}
