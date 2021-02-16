using System;

namespace Quote.Utils
{
    public static class CGuid
    {
        public static Guid ToGuid(this Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public static T ValueOrDefault<T>(this Nullable<T> source) where T : struct
        {
            return source ?? default(T);
        }
    }
}
