using FasTnT.Epcis.Callback.Core.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace FasTnT.Epcis.Callback.Core.Utils;

internal class EpcisEventConverter : JsonConverter
{
    private static readonly JsonSerializerSettings _settings = new() { ContractResolver = new EmptyContractResolver() };

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);

        return obj.GetValue("type").Value<string>() switch
        {
            nameof(ObjectEvent) => Parse<ObjectEvent>(obj),
            nameof(AggregationEvent) => Parse<AggregationEvent>(obj),
            nameof(AssociationEvent) => Parse<AssociationEvent>(obj),
            nameof(TransactionEvent) => Parse<TransactionEvent>(obj),
            nameof(TransformationEvent) => Parse<TransformationEvent>(obj),
            _ => throw new ArgumentOutOfRangeException(obj.GetValue("type")?.Value<string>())
        };
    }

    public override bool CanWrite => false;
    public override bool CanConvert(Type objectType) => false;
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException(); // won't be called because CanWrite returns false

    private static T Parse<T>(JObject obj) => JsonConvert.DeserializeObject<T>(obj.ToString(), _settings);

    private sealed class EmptyContractResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            return typeof(EpcisEvent).IsAssignableFrom(objectType) && !objectType.IsAbstract
                ? null
                : base.ResolveContractConverter(objectType);
        }
    }
}
