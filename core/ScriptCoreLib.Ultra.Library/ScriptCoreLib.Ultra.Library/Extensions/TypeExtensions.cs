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

        public static Type TryMakeGenericType(this Type t, Type[] a)
        {
            
            if (t != null)
                if (t.IsGenericType)
                    if (t.IsGenericTypeDefinition)
                        return t.MakeGenericType(a);

            return t;
        }



        public static TMethod TryGetGenericTypeDefinitionMethod<TMethod>(this TMethod _Member) where TMethod : MemberInfo
        {
            var SourceMethod = (_Member as MethodBase);
            if (SourceMethod == null)
                return _Member;

            if (SourceMethod.DeclaringType == null)
                return _Member;


            var Methods = SourceMethod.DeclaringType.TryGetGenericTypeDefinition().GetMethods(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly
            );

            var Constructors = SourceMethod.DeclaringType.TryGetGenericTypeDefinition().GetConstructors(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly
            );

            try
            {
                var _MethodBase =
                     (MemberInfo)(Methods.FirstOrDefault(kk => kk.MetadataToken == SourceMethod.MetadataToken && kk.GetParameters().Length == SourceMethod.GetParameters().Length)) ??
                    (MemberInfo)(Constructors.FirstOrDefault(kk => kk.MetadataToken == SourceMethod.MetadataToken && kk.GetParameters().Length == SourceMethod.GetParameters().Length));



                return (TMethod)_MethodBase ?? _Member;
            }
            catch
            {
                // TryGetGenericTypeDefinitionMethod: Boolean Get(Int32, Int32)
                Console.WriteLine("TryGetGenericTypeDefinitionMethod: " + SourceMethod);

                throw;
            }
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
