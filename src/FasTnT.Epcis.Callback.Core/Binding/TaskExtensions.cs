using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace FasTnT.Epcis.Callback.Core.Binding;

static class TaskExtensions
{
    public static ValueTask<object> TryCastTask(this object taskObj)
    {
        return taskObj is null
            ? ValueTask.FromResult<object>(Results.NoContent())
            : taskObj.CastTask();
    }

    public static ValueTask<object> CastTask(this object taskObj)
    {
        var resultType = taskObj.GetType().GenericTypeArguments.First();
        var castTaskMethodGeneric = typeof(TaskExtensions).GetMethod(nameof(CastTaskInner), BindingFlags.Static | BindingFlags.Public);
        var castTaskMethod = castTaskMethodGeneric.MakeGenericMethod(resultType, typeof(object));

        return (ValueTask<object>)castTaskMethod.Invoke(null, new[] { taskObj });
    }

    public static async ValueTask<TResult> CastTaskInner<T, TResult>(Task<T> task)
    {
        var result = await task as object;

        return (TResult)result;
    }
}