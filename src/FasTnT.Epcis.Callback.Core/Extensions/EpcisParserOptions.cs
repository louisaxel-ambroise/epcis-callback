using FasTnT.Epcis.Callback.Core.Model;

namespace FasTnT.Epcis.Callback.Core.Extensions;

public class EpcisParserOptions
{
    private readonly Dictionary<string, Type> _registeredTypes = new();

    public EpcisParserOptions RegisterBaseEventTypes()
    {
        var modelAssembly = typeof(EpcisEvent).Assembly;

        foreach (var type in modelAssembly.GetTypes().Where(x => !x.IsAbstract && typeof(EpcisEvent).IsAssignableFrom(x)))
        {
            RegisterEventType(type);
        }

        return this;
    }

    public EpcisParserOptions ReplaceEventType<TOld, TNew>()
        where TOld : EpcisEvent
        where TNew : EpcisEvent
    {
        return RegisterEventType(typeof(TOld).Name, typeof(TNew));
    }

    public EpcisParserOptions RegisterEventType<T>() where T : EpcisEvent
    {
        return RegisterEventType(typeof(T));
    }

    public EpcisParserOptions RegisterEventType(Type eventType)
    {
        return RegisterEventType(eventType.Name, eventType);
    }

    public EpcisParserOptions RegisterEventType(string eventName, Type eventType)
    {
        _registeredTypes[eventName] = eventType;

        return this;
    }

    public Type GetFromType(string eventType)
    {
        return _registeredTypes.TryGetValue(eventType, out var registeredType)
            ? registeredType
            : throw new NotImplementedException();
    }
}