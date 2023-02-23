namespace FasTnT.Epcis.Callback.Core.Events;

public class EventSource
{
    [XmlElement("source")]
    public string Source { get; set; }
    [XmlElement("type")]
    public string Type { get; set; }
}
