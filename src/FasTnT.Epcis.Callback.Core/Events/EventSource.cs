namespace FasTnT.Epcis.Callback.Core.Events;

public class EventSource
{
    [XmlElement("source")]
    [JsonPropertyName("source")]
    public string Source { get; set; }
    [XmlElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
