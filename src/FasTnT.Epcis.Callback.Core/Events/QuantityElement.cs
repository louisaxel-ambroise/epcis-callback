namespace FasTnT.Epcis.Callback.Core.Events;

public class QuantityElement
{
    [XmlElement("epcClass")]
    public string EpcClass { get; set; }
    [XmlElement("quantity")]
    public decimal? Quantity { get; set; }
    [XmlElement("uom")]
    public string Uom { get; set; }
}
