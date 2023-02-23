using FasTnT.Epcis.Callback.Core.Events;

namespace FasTnT.Epcis.Callback.Core;

public class ResultBody
{
    [XmlArray("EventList")]
    [XmlArrayItem(nameof(ObjectEvent), typeof(ObjectEvent))]
    [XmlArrayItem(nameof(TransformationEvent), typeof(TransformationEvent))]
    [XmlArrayItem(nameof(TransactionEvent), typeof(TransactionEvent))]
    [XmlArrayItem(nameof(AggregationEvent), typeof(AggregationEvent))]
    [XmlArrayItem(nameof(AssociationEvent), typeof(AssociationEvent))]
    [JsonPropertyName("eventList")]
    public EpcisEvent[] EventList { get; set; }
}