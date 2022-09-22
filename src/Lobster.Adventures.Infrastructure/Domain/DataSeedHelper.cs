using System.Reflection;

using Ardalis.GuardClauses;

namespace Lobster.Adventures.Infrastructure.Domain
{
    public static class DataSeedHelper
    {
        public static void SetPrivateProperty<T>(Object obj, string propName, T value)
        {
            Guard.Against.Null(obj);
            Guard.Against.Null(propName);
            Guard.Against.Null(value);

            if (obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new MissingFieldException($"{propName} is not found in {obj.GetType().FullName}");

            obj.GetType()
               .InvokeMember(propName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                    null,
                    obj,
                    new object[] { value });
        }

    }
}