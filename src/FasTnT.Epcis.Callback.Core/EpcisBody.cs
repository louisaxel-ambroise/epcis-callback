namespace FasTnT.Epcis.Callback.Core;

public class EpcisBody
{
    [XmlElement("QueryResults", Namespace = "urn:epcglobal:epcis-query:xsd:1")]
    [JsonPropertyName("queryResults")]
    public QueryResults QueryResults { get; set; }
}
