namespace FasTnT.Epcis.Callback.Core;

public class QueryResults
{
    [XmlElement("queryName", Namespace = "")]
    [JsonPropertyName("queryName")]
    public string QueryName { get; set; }
    [XmlElement("subscriptionID", Namespace = "")]
    [JsonPropertyName("subscriptionId")]
    public string SubscriptionID { get; set; }
    [XmlElement("resultsBody", Namespace = "")]
    [JsonPropertyName("resultsBody")]
    public ResultBody ResultsBody { get; set; }
}
