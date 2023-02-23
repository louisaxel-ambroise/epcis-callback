namespace FasTnT.Epcis.Callback.Core.Events;

public class PersistentDisposition
{
    [XmlArray("set")]
    public string[] Set { get; set; }
    [XmlArray("unset")]
    public string[] Unset { get; set; }
}
