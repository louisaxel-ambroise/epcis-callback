using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FasTnT.Epcis.Callback.Api.Binding;

public class EpcisCallbackBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        return new EpcisCallbackBinder();
    }
}