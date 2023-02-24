namespace FasTnT.Epcis.Callback.Core.Model;

public class AggregationEvent : EpcisEvent
{
    public string ParentID { get; set; }
    public string[] ChildEPCs { get; set; }
    public EventAction Action { get; set; }
    public string BizStep { get; set; }
    public string Disposition { get; set; }
    public ReadPoint ReadPoint { get; set; }
    public BizLocation BizLocation { get; set; }
    public BusinessTransaction[] BizTransactionList { get; set; }
    public QuantityElement[] ChildQuantityList { get; set; }
    public EventSource[] SourceList { get; set; }
    public EventDestination[] DestinationList { get; set; }
    public SensorElement[] SensorElementList { get; set; }
}
