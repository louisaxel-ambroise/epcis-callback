using FasTnT.Epcis.Callback.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

namespace FasTnT.Epcis.Callback.Core.Binding;

[ModelBinder(BinderType = typeof(EpcisSubscriptionBinder))]
public record EpcisCallback(string SubscriptionId, string QueryName, IEnumerable<EpcisEvent> Events)
{
    public static async ValueTask<EpcisCallback> BindAsync(HttpContext context)
    {
        var parserOptions = context.RequestServices.GetRequiredService<EpcisParserOptions>();
        var document = await JsonDocument.ParseAsync(context.Request.Body, cancellationToken: context.RequestAborted);
        var namespaceContext = new EpcisNamespaceContext();

        if (!document.RootElement.TryGetProperty("type", out var documentType) || documentType.GetString() != "EPCISQueryDocument")
        {
            throw new Exception("Unable to parse document: JSON is not a valid EPCISQueryDocument");
        }

        // Parse @context field for custom namespaces
        if (document.RootElement.TryGetProperty("@context", out var jsonContext) && jsonContext.ValueKind == JsonValueKind.Array)
        {
            foreach (var value in jsonContext.EnumerateArray().Where(x => x.ValueKind == JsonValueKind.Object))
            {
                foreach (var property in value.EnumerateObject())
                {
                    namespaceContext.Namespaces.Add(property.Name, property.Value.GetString());
                }
            }
        }

        var queryResults = document.RootElement.GetProperty("epcisBody").GetProperty("queryResults");

        if (queryResults.ValueKind == JsonValueKind.Undefined)
        {
            throw new Exception("Unable to parse document: JSON is not a valid EPCISQueryDocument");
        }

        var queryName = queryResults.GetProperty("queryName").GetString();
        var subscriptionId = queryResults.GetProperty("subscriptionID").GetString();
        var eventContainer = queryResults.GetProperty("resultsBody").GetProperty("eventList");
        var eventList = new List<EpcisEvent>();

        if (eventContainer.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in eventContainer.EnumerateArray())
            {
                eventList.Add(ParseEvent(element, namespaceContext, parserOptions));
            }
            // TODO: parse event list
        }

        return new(subscriptionId, queryName, eventList);
    }

    private static EpcisEvent ParseEvent(JsonElement element, EpcisNamespaceContext namespaceContext, EpcisParserOptions parserOptions)
    {
        var jsonEventType = element.GetProperty("type").GetString();

        if (jsonEventType is null)
        {
            throw new Exception("Unable to parse document: JSON is not a valid EPCISQueryDocument (missing type property)");
        }

        var targetType = parserOptions.GetFromType(jsonEventType);

        if (targetType is null)
        {
            throw new Exception($"Event of type {jsonEventType} cannot be parsed");
        }

        return DeserializeElement(targetType, element, namespaceContext) as EpcisEvent;
    }

    private static object DeserializeElement(Type elementType, JsonElement element, EpcisNamespaceContext context)
    {
        var obj = Activator.CreateInstance(elementType);
        var jsonProperties = element.EnumerateObject();

        foreach (var property in elementType.GetProperties())
        {
            var name = property.GetCustomAttributes<EpcisPropertyAttribute>().FirstOrDefault();
            var value = default(object);
            var jsonName = name != null
                ? context.GetFullName(name)
            : property.Name;

            var jsonValue = jsonProperties.SingleOrDefault(x => x.Name.Equals(jsonName, StringComparison.OrdinalIgnoreCase));

            if (jsonValue.Value.ValueKind == JsonValueKind.Undefined)
            {
                continue;
            }

            // Parse correct type
            if (property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTimeOffset) || property.PropertyType == typeof(DateTimeOffset?) || property.PropertyType.IsPrimitive)
            {
                value = jsonValue.Value.Deserialize(property.PropertyType);
            }
            else if (property.PropertyType.IsEnum)
            {
                value = Enum.Parse(property.PropertyType, jsonValue.Value.GetString(), true);
            }
            else if (property.PropertyType.IsArray || property.PropertyType.Name == "IEnumerable`1" || property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null)
            {
                var innerElementType = property.PropertyType.IsArray
                    ? property.PropertyType.GetElementType()
                    : property.PropertyType.GenericTypeArguments[0];

                if (innerElementType.IsPrimitive || innerElementType == typeof(string))
                {
                    value = jsonValue.Value.Deserialize(property.PropertyType);
                }
                else
                {
                    var enumerableType = typeof(List<>).MakeGenericType(innerElementType);
                    var instance = (System.Collections.IList)Activator.CreateInstance(enumerableType);

                    foreach (var arrayValue in jsonValue.Value.EnumerateArray())
                    {
                        instance.Add(DeserializeElement(innerElementType, arrayValue, context));
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
                value = DeserializeElement(property.PropertyType, jsonValue.Value, context);
            }

            property.SetValue(obj, value);
        }

        return Convert.ChangeType(obj, elementType);
    }

    internal class EpcisNamespaceContext
    {
        public IDictionary<string, string> Namespaces = new Dictionary<string, string>();

        public string GetNamespacePrefix(string namespaceName)
        {
            var namespaceValue = Namespaces.FirstOrDefault(x => x.Value == namespaceName);

            return namespaceValue.Key;
        }

        internal string GetFullName(EpcisPropertyAttribute nameProperty)
        {
            return nameProperty.Namespace is null
                ? nameProperty.Name
                : $"{GetNamespacePrefix(nameProperty.Namespace)}:{nameProperty.Name}";
        }
    }
}
