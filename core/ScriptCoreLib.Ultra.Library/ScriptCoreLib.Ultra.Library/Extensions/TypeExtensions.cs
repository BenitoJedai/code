using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Extensions
{
    public static class TypeExtensions
    {
        public static MethodBase TryGetGenericMethodDefinition(this MethodBase t)
        {
            if (t is MethodInfo)
                return TryGetGenericMethodDefinition((MethodInfo)t);

            return t;
        }

        public static MethodInfo TryGetGenericMethodDefinition(this MethodInfo t)
        {

            if (t != null)
                if (t.IsGenericMethod)
                    if (!t.IsGenericMethodDefinition)
                        return t.GetGenericMethodDefinition();

            return t;
        }


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
