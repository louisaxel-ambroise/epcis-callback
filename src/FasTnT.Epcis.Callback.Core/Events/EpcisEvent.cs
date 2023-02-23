namespace FasTnT.Epcis.Callback.Core.Events;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ObjectEvent), typeDiscriminator: nameof(ObjectEvent))]
[JsonDerivedType(typeof(AggregationEvent), typeDiscriminator: nameof(AggregationEvent))]
[JsonDerivedType(typeof(AssociationEvent), typeDiscriminator: nameof(AssociationEvent))]
[JsonDerivedType(typeof(TransactionEvent), typeDiscriminator: nameof(TransactionEvent))]
[JsonDerivedType(typeof(TransformationEvent), typeDiscriminator: nameof(TransformationEvent))]
public abstract class EpcisEvent
{
    [XmlElement("EventID")]
    [JsonPropertyName("eventID")]
    public string EventId { get; set; }
    [XmlElement("eventTime")]
    [JsonPropertyName("eventTime")]
    public DateTimeOffset EventTime { get; set; }
    [XmlElement("eventTimeZoneOffset")]
    [JsonPropertyName("eventTimeZoneOffset")]
    public string EventTimeZoneOffset { get; set; }
    [XmlElement("recordTime")]
    [JsonPropertyName("recordTime")]
    public DateTimeOffset? RecordTime { get; set; }
    [XmlElement("errorDeclaration")]
    [JsonPropertyName("errorDeclaration")]
    public ErrorDeclaration ErrorDeclaration { get; set; }
    [XmlArray("certifications")]
    [JsonPropertyName("certifications")]
    public string[] Certifications { get; set; }
}
