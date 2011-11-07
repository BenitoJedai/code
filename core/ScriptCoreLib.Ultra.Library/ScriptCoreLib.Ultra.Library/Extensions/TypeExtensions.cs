using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class TypeExtensions
    {
        public static Type TryGetGenericTypeDefinition(this Type t)
        {

            if (t != null)
                if (t.IsGenericType)
                    if (!t.IsGenericTypeDefinition)
                        return t.GetGenericTypeDefinition();

            return t;
        }

        public static bool IsCommonDelegateType(this Type SourceType)
        {
            return new[]
                    {
                        typeof(EventHandler),
                        typeof(EventHandler<>),

                        typeof(Action),
                        typeof(Action<>),
                        typeof(Action<,>),
                        typeof(Action<,,>),
                        typeof(Action<,,,>),
                        typeof(Action<,,,,>),
                        typeof(Action<,,,,,>),
                        typeof(Action<,,,,,,>),
                        typeof(Action<,,,,,,,>),
                        typeof(Action<,,,,,,,,>),
                        typeof(Action<,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,,,,,>),
                        typeof(Action<,,,,,,,,,,,,,,,>),



                        typeof(Func<>),
                        typeof(Func<,>),
                        typeof(Func<,,>),
                        typeof(Func<,,,>),
                        typeof(Func<,,,,>),
                        typeof(Func<,,,,,>),
                        typeof(Func<,,,,,,>),
                        typeof(Func<,,,,,,,>),
                        typeof(Func<,,,,,,,,>),
                        typeof(Func<,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,,,,,>),
                        typeof(Func<,,,,,,,,,,,,,,,,>),
                    }.Contains(SourceType.TryGetGenericTypeDefinition());
        }

        public static bool TypeEqualsOrElementTypeEquals(this Type e, Type x)
        {
            // Type.Equals on arrays seems to return false even if element types are the same...

            if (e.HasElementType)
            {
                if (x.HasElementType)
                    return x.GetElementType().TypeEqualsOrElementTypeEquals(e.GetElementType());

                return false;
            }

            return e == x;
        }
    }
}
