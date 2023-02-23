namespace FasTnT.Epcis.Callback.Core.Events;

public class SensorElement
{
    [XmlElement("sensorMetadata")]
    public SensorMetadata SensorMetadata { get; set; }
    [XmlArray("sensorReports")]
    public SensorReport[] SensorReports { get; set; }
}
