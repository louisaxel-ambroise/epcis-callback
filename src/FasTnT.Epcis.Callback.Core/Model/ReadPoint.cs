namespace FasTnT.Epcis.Callback.Core.Model;

public class ReadPoint
{
    public string Id { get; set; }

    public static implicit operator string(ReadPoint readPoint) => readPoint.Id;
}
