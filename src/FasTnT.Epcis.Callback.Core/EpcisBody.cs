namespace FasTnT.Epcis.Callback.Core;

public class EpcisBody
{
    [XmlElement("QueryResults", Namespace = "urn:epcglobal:epcis-query:xsd:1")]
    public QueryResults QueryResults { get; set; }
}
