namespace FasTnT.Epcis.Callback.Api.Binding;

public class EpcisCallbackProviderBuilder
{
    private readonly IDictionary<string, Delegate> _callbacks;

    public EpcisCallbackProviderBuilder()
    {
        _callbacks = new Dictionary<string, Delegate>();
    }

    public void OnCallback(string subscriptionId, Delegate action)
    {
        if (_callbacks.ContainsKey(subscriptionId))
        {
            throw new ArgumentException("Cannot register multiple handlers for the same subscription");
        }

        _callbacks.Add(subscriptionId, action);
    }

    public IEpcisCallbackProvider Build() => new SubscriptionEpcisCallbackProvider(_callbacks);
}
