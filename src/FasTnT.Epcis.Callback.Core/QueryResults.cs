namespace FasTnT.Epcis.Callback.Core;

public class QueryResults
{
    [XmlElement("queryName")]
    public string QueryName { get; set; }
    [XmlElement("subscriptionID")]
    public string SubscriptionID { get; set; }
    [XmlElement("resultsBody")]
    public ResultBody ResultsBody { get; set; }
}
