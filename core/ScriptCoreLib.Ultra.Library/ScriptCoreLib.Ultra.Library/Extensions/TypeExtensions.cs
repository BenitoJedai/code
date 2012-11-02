using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

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




        public static TMethod TryGetGenericTypeDefinitionMethod<TMethod>(this TMethod _Member) where TMethod : MemberInfo
        {
            var _Method = (_Member as MethodBase);
            if (_Method == null)
                return _Member;

            var Methods = _Method.DeclaringType.TryGetGenericTypeDefinition().GetMethods(
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly
                        );

            var Constructors = _Method.DeclaringType.TryGetGenericTypeDefinition().GetMethods(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly
                );

            var _MethodBase =
                 (MemberInfo)(Methods.SingleOrDefault(kk => kk.MetadataToken == _Method.MetadataToken)) ??
                (MemberInfo)(Constructors.SingleOrDefault(kk => kk.MetadataToken == _Method.MetadataToken));



            return (TMethod)_MethodBase ?? _Member;
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

            // what about generics?
            if (e.IsGenericType && !e.IsGenericTypeDefinition)
            {
                var g = e.TryGetGenericTypeDefinition().TypeEqualsOrElementTypeEquals(x.TryGetGenericTypeDefinition());

                if (!g)
                    return false;


                return e.GetGenericArguments().Zip(x.GetGenericArguments(),
                    (_e, _x) =>
                        _e.TypeEqualsOrElementTypeEquals(_x)
                ).All(k => k);
            }


            return e.FullName == x.FullName;
        }
    }
}
