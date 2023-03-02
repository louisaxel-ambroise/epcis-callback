namespace FasTnT.Epcis.Callback.Api.Binding;

public class SingleEpcisCallbackProvider : IEpcisCallbackProvider
{
    private readonly Delegate _callback;

    public SingleEpcisCallbackProvider(Delegate callback)
    {
        _callback = callback;
    }

    public Delegate GetCallbackForSubscription(string subscriptionId) => _callback;
}
