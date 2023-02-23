namespace FasTnT.Epcis.Callback.Core.Events;

public class AggregationEvent : EpcisEvent
{
    [XmlElement("parentID")]
    public string ParentID { get; set; }
    [XmlArray("childEPCs")]
    public string[] ChildEPCs { get; set; }
    [XmlElement("action")]
    public EventAction Action { get; set; }
    [XmlElement("bizStep")]
    public string BizStep { get; set; }
    [XmlElement("disposition")]
    public string Disposition { get; set; }
    [XmlElement("readPoint")]
    public ReadPoint ReadPoint { get; set; }
    [XmlElement("bizLocation")]
    public BizLocation BizLocation { get; set; }
    [XmlArray("bizTransactionList")]
    public BusinessTransaction[] BizTransactionList { get; set; }
    [XmlArray("childQuantityList")]
    public QuantityElement[] ChildQuantityList { get; set; }
    [XmlArray("sourceList")]
    public EventSource[] SourceList { get; set; }
    [XmlArray("destinationList")]
    public EventDestination[] DestinationList { get; set; }
    [XmlArray("sensorElementList")]
    public SensorElement[] SensorElementList { get; set; }
}
