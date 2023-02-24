namespace FasTnT.Epcis.Callback.Core.Model;

public class ErrorDeclaration
{
    public DateTimeOffset DeclarationTime { get; set; }
    public string Reason { get; set; }
    public string[] CorrectiveEventIds { get; set; }
}
