namespace FasTnT.Epcis.Callback.Api.Binding;

public interface IEpcisCallbackProvider
{
    Delegate GetCallbackForSubscription(string subscriptionId);
}
