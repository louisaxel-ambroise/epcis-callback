namespace FasTnT.Epcis.Callback.Core.Events;

public class BizLocation
{
    [XmlElement("id")]
    public string Id { get; set; }

    public static implicit operator string(BizLocation bizLocation) => bizLocation.Id;
}
