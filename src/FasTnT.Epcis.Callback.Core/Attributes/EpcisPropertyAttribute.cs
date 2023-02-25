namespace FasTnT.Epcis.Callback.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class EpcisPropertyAttribute : Attribute
{
    public string Name { get; }
    public string Namespace { get; }

    public EpcisPropertyAttribute(string name, string @namespace = null)
    {
        Name = name;
        Namespace = @namespace;
    }
}
