namespace FasTnT.Epcis.Callback.Core.Model;

public abstract class EpcisEvent
{
    public string EventID { get; set; }
    public DateTimeOffset EventTime { get; set; }
    public string EventTimeZoneOffset { get; set; }
    public DateTimeOffset? RecordTime { get; set; }
    public ErrorDeclaration ErrorDeclaration { get; set; }
    public string[] Certifications { get; set; }
}
