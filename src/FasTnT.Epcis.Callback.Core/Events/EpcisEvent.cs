using FasTnT.Epcis.Callback.Core.Utils;
using Newtonsoft.Json;

namespace FasTnT.Epcis.Callback.Core.Events;

[JsonConverter(typeof(EpcisEventConverter))]
public abstract class EpcisEvent
{
    [XmlElement("type")]
    public EventType Type { get; set; }
    public string EventID { get; set; }
    [XmlElement("eventTime")]
    public DateTimeOffset EventTime { get; set; }
    [XmlElement("eventTimeZoneOffset")]
    public string EventTimeZoneOffset { get; set; }
    [XmlElement("recordTime")]
    public DateTimeOffset? RecordTime { get; set; }
    [XmlElement("errorDeclaration")]
    public ErrorDeclaration ErrorDeclaration { get; set; }
    [XmlArray("certifications")]
    public string[] Certifications { get; set; }
}
