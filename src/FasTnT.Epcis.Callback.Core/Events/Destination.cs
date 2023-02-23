namespace FasTnT.Epcis.Callback.Core.Events;

public class EventDestination
{
    [XmlElement("destination")]
    public string Destination { get; set; }
    [XmlElement("type")]
    public string Type { get; set; }
}
