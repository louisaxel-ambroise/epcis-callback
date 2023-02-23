namespace FasTnT.Epcis.Callback.Core.Events;

public class ErrorDeclaration
{
    [XmlElement("declarationTime")]
    [JsonPropertyName("declarationTime")]
    public DateTimeOffset DeclarationTime { get; set; }
    [XmlElement("reason")]
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
    [XmlArray("correctiveEventIDs")]
    [JsonPropertyName("correctiveEventIDs")]
    public string[] CorrectiveEventIds { get; set; }
}
