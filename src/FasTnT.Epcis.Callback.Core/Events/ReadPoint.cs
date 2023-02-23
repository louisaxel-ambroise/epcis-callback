namespace FasTnT.Epcis.Callback.Core.Events;

public class ReadPoint
{
    [XmlElement("id")]
    public string Id { get; set; }

    public static implicit operator string(ReadPoint readPoint) => readPoint.Id;
}