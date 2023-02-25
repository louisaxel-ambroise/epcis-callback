using FasTnT.Epcis.Callback.Core.Model;

namespace FasTnT.Epcis.Callback.Core;

public record EpcisCallback(string SubscriptionId, string QueryName, IEnumerable<EpcisEvent> Events);