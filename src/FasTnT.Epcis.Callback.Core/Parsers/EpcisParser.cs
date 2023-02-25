namespace FasTnT.Epcis.Callback.Core.Parsers;

public class EpcisParser
{
    private readonly Dictionary<string, Type> _registeredTypes;

    public EpcisParser(Dictionary<string, Type> registeredTypes)
    {
        _registeredTypes = registeredTypes;
    }

    public Type GetFromType(string eventType)
    {
        return _registeredTypes.TryGetValue(eventType, out var registeredType)
            ? registeredType
            : throw new NotImplementedException();
    }
}