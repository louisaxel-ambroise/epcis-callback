namespace FasTnT.Epcis.Callback.Core.Events;

public class ErrorDeclaration
{
    [XmlElement("declarationTime")]
    public DateTimeOffset DeclarationTime { get; set; }
    [XmlElement("reason")]
    public string Reason { get; set; }
    [XmlArray("correctiveEventIDs")]
    public string[] CorrectiveEventIds { get; set; }
}
