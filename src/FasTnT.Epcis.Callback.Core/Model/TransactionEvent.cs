namespace FasTnT.Epcis.Callback.Core.Model;

public class TransactionEvent : EpcisEvent
{
    public BusinessTransaction[] BizTransactionList { get; set; }
    public string ParentId { get; set; }
    public string[] EpcList { get; set; }
    public EventAction Action { get; set; }
    public string BizStep { get; set; }
    public string Disposition { get; set; }
    public ReadPoint ReadPoint { get; set; }
    public BizLocation BizLocation { get; set; }
    public QuantityElement[] QuantityList { get; set; }
    public EventSource[] SourceList { get; set; }
    public EventDestination[] DestinationList { get; set; }
    public SensorElement[] SensorElementList { get; set; }
}
