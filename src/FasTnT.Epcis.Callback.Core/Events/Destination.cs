namespace FasTnT.Epcis.Callback.Core.Events;

public class EventDestination
{
    [XmlElement("destination")]
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
    [XmlElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
