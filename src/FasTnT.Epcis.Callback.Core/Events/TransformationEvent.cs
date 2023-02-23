namespace FasTnT.Epcis.Callback.Core.Events;

public class TransformationEvent : EpcisEvent
{
    [XmlArray("inputEpcList")]
    [JsonPropertyName("inputEpcList")]
    public string[] InputEpcList { get; set; }
    [XmlArray("inputQuantityList"), XmlArrayItem("quantityElement")]
    [JsonPropertyName("inputQuantityList")]
    public QuantityElement[] InputQuantityList { get; set; }
    [XmlArray("outputEpcList")]
    [JsonPropertyName("outputEpcList")]
    public string[] OutputEpcList { get; set; }
    [XmlArray("outputQuantityList"), XmlArrayItem("quantityElement")]
    [JsonPropertyName("outputQuantityList")]
    public QuantityElement[] OutputQuantityList { get; set; }
    [XmlElement("transformationID")]
    [JsonPropertyName("transformationID")]
    public string TransformationId { get; set; }
    [XmlElement("bizStep")]
    [JsonPropertyName("bizStep")]
    public string BizStep { get; set; }
    [XmlElement("disposition")]
    [JsonPropertyName("disposition")]
    public string Disposition { get; set; }
    [XmlElement("persistentDisposition")]
    [JsonPropertyName("persistentDisposition")]
    public PersistentDisposition PersistentDisposition { get; set; }
    [XmlElement("readPoint")]
    [JsonPropertyName("readPoint")]
    public ReadPoint ReadPoint { get; set; }
    [XmlElement("bizLocation")]
    [JsonPropertyName("bizLocation")]
    public BizLocation BizLocation { get; set; }
    [XmlArray("bizTransactionList")]
    [JsonPropertyName("bizTransactionList")]
    public BusinessTransaction[] BizTransactionList { get; set; }
    [XmlArray("sourceList")]
    [JsonPropertyName("sourceList")]
    public EventSource[] SourceList { get; set; }
    [XmlArray("destinationList")]
    [JsonPropertyName("destinationList")]
    public EventDestination[] DestinationList { get; set; }
    [XmlArray("sensorElementList")]
    [JsonPropertyName("sensorElementList")]
    public SensorElement[] SensorElementList { get; set; }
    [XmlElement("ilmd")]
    [JsonPropertyName("ilmd")]
    public Ilmd Ilmd { get; set; }
}
