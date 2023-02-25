using FasTnT.Epcis.Callback.Core.Attributes;
using FasTnT.Epcis.Callback.Core.Extensions;
using FasTnT.Epcis.Callback.Core.Model;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FasTnT.Epcis.Callback.Core.Parsers;

public static class XmlCallbackParser
{
    public static async ValueTask<EpcisCallback> ParseAsync(Stream content, EpcisParserOptions parserOptions, CancellationToken cancellationToken)
    {
        var document = await XDocument.LoadAsync(content, LoadOptions.None, cancellationToken);
        var namespaceManager = new XmlNamespaceManager(new NameTable());
        namespaceManager.AddNamespace("epcisq", "urn:epcglobal:epcis-query:xsd:1");

        var queryResults = document.XPathSelectElement("//EPCISBody/epcisq:QueryResults", namespaceManager);

        if (queryResults is null)
        {
            throw new Exception("Unable to parse document: XML is not a valid EPCISQueryDocument");
        }

        var queryName = queryResults.Element("queryName").Value;
        var subscriptionId = queryResults.Element("subscriptionID").Value;
        var eventContainer = queryResults.XPathSelectElement("resultsBody/EventList");
        var eventList = new List<EpcisEvent>();

        if (eventContainer is not null)
        {
            foreach (var element in eventContainer.Elements().Select(FlattenExtensions))
            {
                eventList.Add(ParseEvent(element, parserOptions));
            }
        }

        return new(subscriptionId, queryName, eventList);
    }

    private static XElement FlattenExtensions(XElement element)
    {
        if(element.Name != XName.Get("extension", string.Empty))
        {
            return element;
        }

        return FlattenExtensions(element.Elements().First());
    }

    private static EpcisEvent ParseEvent(XElement element, EpcisParserOptions parserOptions)
    {
        var eventType = element.Name.LocalName;
        var targetType = parserOptions.GetFromType(eventType);

        if (targetType is null)
        {
            throw new Exception($"Event of type {eventType} cannot be parsed");
        }

        return DeserializeElement(targetType, element) as EpcisEvent;
    }

    private static object DeserializeElement(Type elementType, XElement element)
    {
        var obj = Activator.CreateInstance(elementType);
        var properties = element.Elements().Select(FlattenExtensions);

        foreach (var property in elementType.GetProperties())
        {
            var name = property.GetCustomAttributes<EpcisPropertyAttribute>().FirstOrDefault();
            var propName = name?.Name ?? property.Name;
            var propNs = name?.Namespace ?? string.Empty;
            var value = default(object);

            var propertyElement = properties.SingleOrDefault(x => x.Name.LocalName.Equals(propName, StringComparison.OrdinalIgnoreCase) && x.Name.NamespaceName == propNs);

            if (propertyElement is null)
            {
                if (property.PropertyType.IsPrimitive())
                {
                    var attribute = element.Attributes().SingleOrDefault(x => x.Name.LocalName.Equals(propName, StringComparison.OrdinalIgnoreCase) && x.Name.NamespaceName == propNs);

                    if (attribute is not null)
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType.NonNullableType());
                        if (converter.CanConvertFrom(typeof(string)))
                        {
                            value = converter.ConvertFrom(attribute.Value);
                            property.SetValue(obj, value);
                        }
                    }
                }

                continue;
            }
            else if (propertyElement is null)
            {
                continue;
            }

            // Parse correct type
            if (property.PropertyType.IsPrimitive())
            {
                var converter = TypeDescriptor.GetConverter(property.PropertyType.NonNullableType());
                if (converter.CanConvertFrom(typeof(string)))
                {
                    value = converter.ConvertFrom(propertyElement.Value);
                }
            }
            else if (property.PropertyType.IsEnum)
            {
                value = Enum.Parse(property.PropertyType, propertyElement.Value, true);
            }
            else if (property.PropertyType.IsArray || property.PropertyType.Name == "IEnumerable`1" || property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null)
            {
                var innerElementType = property.PropertyType.IsArray
                    ? property.PropertyType.GetElementType()
                    : property.PropertyType.GenericTypeArguments[0];

                if (innerElementType.IsPrimitive())
                {
                    value = Convert.ChangeType(propertyElement.Elements().Select(x => x.Value).ToArray(), property.PropertyType);
                }
                else
                {
                    var enumerableType = typeof(List<>).MakeGenericType(innerElementType);
                    var instance = (System.Collections.IList)Activator.CreateInstance(enumerableType);

                    foreach (var arrayValue in propertyElement.Elements())
                    {
                        instance.Add(DeserializeElement(innerElementType, arrayValue));
                    }

                    if (property.PropertyType.IsArray)
                    {
                        var array = Array.CreateInstance(innerElementType, instance.Count);
                        instance.CopyTo(array, 0);

                        value = array;
                    }
                    else
                    {
                        value = instance;
                    }
                }
            }
            else
            {
                value = DeserializeElement(property.PropertyType, propertyElement);
            }

            property.SetValue(obj, value);
        }

        return Convert.ChangeType(obj, elementType);
    }
}
