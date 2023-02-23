namespace FasTnT.Epcis.Callback.Core.Events;

public class BusinessTransaction
{
    [XmlElement("bizTransaction")]
    [JsonPropertyName("bizTransaction")]
    public string BizTransaction { get; set; }
    [XmlElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
