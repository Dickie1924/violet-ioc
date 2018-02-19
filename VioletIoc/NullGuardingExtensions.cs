using System;

namespace VioletIoc
{
    internal static class NullGuardingExtensions
    {
        internal static T ThrowIfNull<T>(this T obj, string name)
            where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }

            return obj;
        }
    }
}
