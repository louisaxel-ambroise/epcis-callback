namespace FasTnT.Epcis.Callback.Core.Events;

public class QuantityElement
{
    [XmlElement("epcClass")]
    [JsonPropertyName("epcClass")]
    public string EpcClass { get; set; }
    [XmlElement("quantity")]
    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; set; }
    [XmlElement("uom")]
    [JsonPropertyName("uom")]
    public string Uom { get; set; }
}
