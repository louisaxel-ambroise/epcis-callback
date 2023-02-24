using FasTnT.Epcis.Callback.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FasTnT.Epcis.Callback.Core.Binding;

public class EpcisCallbackBuilder
{
    private readonly Dictionary<string, Delegate> _mappedActions = new();

    public void OnCallback(string subscriptionId, Delegate action) => _mappedActions.Add(subscriptionId, action);

    internal ValueTask<object> HandleCallback(EpcisCallback callback, HttpContext context, CancellationToken cancellationToken)
    {
        return _mappedActions.TryGetValue(callback.SubscriptionId, out var handler)
            ? HandleCallbackAction(handler, callback, context, cancellationToken)
            : ValueTask.FromResult<object>(Results.Conflict());
    }

    private static ValueTask<object> HandleCallbackAction(Delegate handler, EpcisCallback callback, HttpContext context, CancellationToken cancellationToken)
    {
        try
        {
            var parameters = handler.Method.GetParameters();
            var paramList = new object[parameters.Length];

            for (int i = 0; i < paramList.Length; i++)
            {
                if (parameters[i].ParameterType.Name == callback.SubscriptionId)
                {
                    paramList[i] = callback.SubscriptionId;
                }
                else if (parameters[i].ParameterType.Name == callback.QueryName)
                {
                    paramList[i] = callback.QueryName;
                }
                else if (parameters[i].ParameterType == typeof(IEnumerable<EpcisEvent>))
                {
                    paramList[i] = callback.Events;
                }
                else if (parameters[i].ParameterType == typeof(EpcisEvent[]))
                {
                    paramList[i] = callback.Events.ToArray();
                }
                else if (parameters[i].ParameterType == typeof(EpcisCallback))
                {
                    paramList[i] = callback.Events;
                }
                else if (parameters[i].ParameterType == typeof(CancellationToken))
                {
                    paramList[i] = cancellationToken;
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
