namespace FasTnT.Epcis.Callback.Core.Model;

public class TransformationEvent : EpcisEvent
{
    public string[] InputEpcList { get; set; }
    public QuantityElement[] InputQuantityList { get; set; }
    public string[] OutputEpcList { get; set; }
    public QuantityElement[] OutputQuantityList { get; set; }
    public string TransformationId { get; set; }
    public string BizStep { get; set; }
    public string Disposition { get; set; }
    public PersistentDisposition PersistentDisposition { get; set; }
    public ReadPoint ReadPoint { get; set; }
    public BizLocation BizLocation { get; set; }
    public BusinessTransaction[] BizTransactionList { get; set; }
    public EventSource[] SourceList { get; set; }
    public EventDestination[] DestinationList { get; set; }
    public SensorElement[] SensorElementList { get; set; }
}