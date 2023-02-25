namespace FasTnT.Epcis.Callback.Core.Extensions;

static class TypeExtensions
{
    private static readonly Type[] _primitiveTypes = new[]
    {
        typeof(string),
        typeof(decimal),
        typeof(DateTime),
        typeof(DateTimeOffset)
    };

    public static bool IsPrimitive(this Type type)
    {
        return
            type.IsPrimitive
         || _primitiveTypes.Contains(type)
         || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].IsPrimitive());
    }

    public static Type NonNullableType(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? type.GetGenericArguments()[0]
            : type;
    }
}
