namespace FasTnT.Epcis.Callback.Core.Events;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventAction
{
    ADD,
    OBSERVE,
    DELETE
}
