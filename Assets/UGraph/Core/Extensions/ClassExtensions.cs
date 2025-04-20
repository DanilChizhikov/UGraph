using System;

namespace DTech.UGraph.Core.Extensions
{
    public static class ClassExtensions
    {
        public static T ThrowIfNull<T>(this T source)
            where T : class =>
            source ?? throw new NullReferenceException(nameof(source));
    }
}