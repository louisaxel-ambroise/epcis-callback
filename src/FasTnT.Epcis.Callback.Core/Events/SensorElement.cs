namespace FasTnT.Epcis.Callback.Core.Events;

public class SensorElement
{
    [XmlElement("sensorMetadata")]
    [JsonPropertyName("sensorMetadata")]
    public SensorMetadata SensorMetadata { get; set; }
    [XmlArray("sensorReports")]
    [JsonPropertyName("sensorReports")]
    public SensorReport[] SensorReports { get; set; }
}
