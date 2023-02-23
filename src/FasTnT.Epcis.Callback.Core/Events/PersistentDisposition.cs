namespace FasTnT.Epcis.Callback.Core.Events;

public class PersistentDisposition
{
    [XmlArray("set")]
    [JsonPropertyName("set")]
    public string[] Set { get; set; }
    [XmlArray("unset")]
    [JsonPropertyName("unset")]
    public string[] Unset { get; set; }
}
