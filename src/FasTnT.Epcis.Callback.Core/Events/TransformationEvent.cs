namespace FasTnT.Epcis.Callback.Core.Events;

public class TransformationEvent : EpcisEvent
{
    [XmlArray("inputEpcList")]
    public string[] InputEpcList { get; set; }
    [XmlArray("inputQuantityList"), XmlArrayItem("quantityElement")]
    public QuantityElement[] InputQuantityList { get; set; }
    [XmlArray("outputEpcList")]
    public string[] OutputEpcList { get; set; }
    [XmlArray("outputQuantityList"), XmlArrayItem("quantityElement")]
    public QuantityElement[] OutputQuantityList { get; set; }
    [XmlElement("transformationID")]
    public string TransformationId { get; set; }
    [XmlElement("bizStep")]
    public string BizStep { get; set; }
    [XmlElement("disposition")]
    public string Disposition { get; set; }
    [XmlElement("persistentDisposition")]
    public PersistentDisposition PersistentDisposition { get; set; }
    [XmlElement("readPoint")]
    public ReadPoint ReadPoint { get; set; }
    [XmlElement("bizLocation")]
    public BizLocation BizLocation { get; set; }
    [XmlArray("bizTransactionList")]
    public BusinessTransaction[] BizTransactionList { get; set; }
    [XmlArray("sourceList")]
    public EventSource[] SourceList { get; set; }
    [XmlArray("destinationList")]
    public EventDestination[] DestinationList { get; set; }
    [XmlArray("sensorElementList")]
    public SensorElement[] SensorElementList { get; set; }
    [XmlElement("ilmd")]
    public Ilmd Ilmd { get; set; }
}
