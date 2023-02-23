namespace FasTnT.Epcis.Callback.Core;

[XmlRoot("EPCISQueryDocument", Namespace = "urn:epcglobal:epcis-query:xsd:1")]
public class EpcisQueryDocument
{
    [XmlAttribute("creationDate")]
    public DateTime CreationDate { get; set; }
    public string Id { get; set; }
    [XmlAttribute("schemaVersion")]
    public string SchemaVersion { get; set; }
    [XmlElement("EPCISBody", Namespace = "")]
    public EpcisBody EpcisBody { get; set; }
}
