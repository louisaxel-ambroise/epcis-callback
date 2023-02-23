namespace FasTnT.Epcis.Callback.Core.Events;

public class TransactionEvent : EpcisEvent
{
    [XmlArray("bizTransactionList")]
    [JsonPropertyName("bizTransactionList")]
    public BusinessTransaction[] BizTransactionList { get; set; }
    [XmlElement("parentID")]
    [JsonPropertyName("parentID")]
    public string ParentId { get; set; }
    [XmlArray("epcList")]
    [JsonPropertyName("epcList")]
    public string[] EpcList { get; set; }
    [XmlElement("action")]
    [JsonPropertyName("action")]
    public EventAction Action { get; set; }
    [XmlElement("bizStep")]
    [JsonPropertyName("bizStep")]
    public string BizStep { get; set; }
    [XmlElement("disposition")]
    [JsonPropertyName("disposition")]
    public string Disposition { get; set; }
    [XmlElement("readPoint")]
    [JsonPropertyName("readPoint")]
    public ReadPoint ReadPoint { get; set; }
    [XmlElement("bizLocation")]
    [JsonPropertyName("bizLocation")]
    public BizLocation BizLocation { get; set; }
    [XmlArray("quantityList")]
    [JsonPropertyName("quantityList")]
    public QuantityElement[] QuantityList { get; set; }
    [XmlArray("sourceList")]
    [JsonPropertyName("sourceList")]
    public EventSource[] SourceList { get; set; }
    [XmlArray("destinationList")]
    [JsonPropertyName("destinationList")]
    public EventDestination[] DestinationList { get; set; }
    [XmlArray("sensorElementList")]
    [JsonPropertyName("sensorElementList")]
    public SensorElement[] SensorElementList { get; set; }
}
