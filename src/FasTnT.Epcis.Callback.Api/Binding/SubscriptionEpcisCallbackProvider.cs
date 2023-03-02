namespace FasTnT.Epcis.Callback.Api.Binding;

public class SubscriptionEpcisCallbackProvider : IEpcisCallbackProvider
{
    private readonly IDictionary<string, Delegate> _callbacks;

    public SubscriptionEpcisCallbackProvider(IDictionary<string, Delegate> callbacks)
    {
        _callbacks = callbacks;
    }

    public Delegate GetCallbackForSubscription(string subscriptionId) => _callbacks.TryGetValue(subscriptionId, out var callback) ? callback : default;
}
