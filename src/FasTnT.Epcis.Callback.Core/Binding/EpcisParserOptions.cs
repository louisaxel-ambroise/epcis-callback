using FasTnT.Epcis.Callback.Core.Model;
using System.Collections;
using System.Reflection;
using System.Text.Json;

namespace FasTnT.Epcis.Callback.Core.Binding;

public class EpcisParserOptions
{
    private readonly Dictionary<string, Type> _registeredTypes = new();

    public void RegisterBaseEventTypes()
    {
        var modelAssembly = typeof(EpcisEvent).Assembly;

        foreach (var type in modelAssembly.GetTypes().Where(x => !x.IsAbstract && typeof(EpcisEvent).IsAssignableFrom(x)))
        {
            RegisterEventType(type);
        }
    }

    public void RegisterEventType<T>() where T : EpcisEvent
    {
        RegisterEventType(typeof(T));
    }

    public void RegisterEventType(Type eventType)
    {
        RegisterEventType(eventType.Name, eventType);
    }

    public void RegisterEventType(string eventName, Type eventType)
    {
        _registeredTypes[eventName] = eventType; // GenerateParser(eventType);
    }

    public void ReplaceEventType<TOld, TNew>()
        where TOld : EpcisEvent
        where TNew : EpcisEvent
    {
        RegisterEventType(typeof(TOld).Name, typeof(TNew));
    }

    //private Func<JsonElement, EpcisNamespaceContext, object> GenerateParser(Type type)
    //{
    //    return (element, context) => DeserializeElement(type, element, context);
    //}



    internal Type GetFromType(string eventType)
    {
        if (_registeredTypes.TryGetValue(eventType, out var registeredType))
        {
            return registeredType;
        }

        // TODO: search for native event type
        throw new NotImplementedException();
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class EpcisPropertyAttribute : Attribute
{
    public string Name { get; }
    public string Namespace { get; }

    public EpcisPropertyAttribute(string name, string @namespace = null)
    {
        Name = name;
        Namespace = @namespace;
    }
}