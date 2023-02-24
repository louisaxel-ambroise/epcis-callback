namespace FasTnT.Epcis.Callback.Core.Model;

public class SensorReport
{
    public DateTimeOffset Time { get; set; }
    public string DeviceId { get; set; }
    public string DeviceMetadata { get; set; }
    public string RawData { get; set; }
    public string DataProcessingMethod { get; set; }
    public string MeasurementType { get; set; }
    public string Exception { get; set; }
    public string Microorganism { get; set; }
    public string ChemicalSubstance { get; set; }
    public double? Value { get; set; }
    public string Component { get; set; }
    public string CoordinateReferenceSystem { get; set; }
    public string StringValue { get; set; }
    public bool? BooleanValue { get; set; }
    public byte[] HexBinaryValue { get; set; }
    public string UriValue { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public double? MeanValue { get; set; }
    public double? SDev { get; set; }
    public double? PercRank { get; set; }
    public double? PercValue { get; set; }
    public string UnitOfMeasure { get; set; }
}
