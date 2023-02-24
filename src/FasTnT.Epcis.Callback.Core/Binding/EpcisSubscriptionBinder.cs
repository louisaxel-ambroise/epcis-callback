using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FasTnT.Epcis.Callback.Core.Binding;

internal class EpcisSubscriptionBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        try
        {
            var callback = await EpcisCallback.BindAsync(bindingContext.HttpContext);
            bindingContext.Result = ModelBindingResult.Success(callback);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex.Message);
        }
    }
}