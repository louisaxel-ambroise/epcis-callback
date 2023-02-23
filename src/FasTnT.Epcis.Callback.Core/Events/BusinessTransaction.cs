namespace FasTnT.Epcis.Callback.Core.Events;

public class BusinessTransaction
{
    [XmlElement("bizTransaction")]
    public string BizTransaction { get; set; }
    [XmlElement("type")]
    public string Type { get; set; }
}
