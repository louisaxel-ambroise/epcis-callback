namespace FasTnT.Epcis.Callback.Core.Events;

public class SensorMetadata
{
    public DateTimeOffset Time { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string DeviceId { get; set; }
    public string DeviceMetadata { get; set; }
    public string RawData { get; set; }
    public string BizRules { get; set; }
}
