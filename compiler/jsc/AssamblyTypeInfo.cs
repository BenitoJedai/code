using System;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text;


using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc
{
    public class AssamblyTypeInfo
    {
        public CommandLineOptions Options = new CommandLineOptions();

        public Type[] Types;


		

        public List<Type> ImplementationTypes = new List<Type>();



        static Type[] ParameterArrayToTypeArray(ParameterInfo[] xxp, Type src, Type impl)
        {
            Type[] pt = new Type[xxp.Length];

            for (int xxi = 0; xxi < xxp.Length; xxi++)
            {
                Type z = xxp[xxi].ParameterType;

                if (z.IsArray)
                {
                    Type z0 = src.MakeArrayType();
                    Type z1 = impl.MakeArrayType();

                    pt[xxi] = z == z0 ? z1 : z;
                }
                else
                    pt[xxi] = z == src ? impl : z;
            }

            return pt;
        }

        public MethodBase ResolveMethod(MethodBase src_method, Type impl_type, string impl_methodname)
        {
            // maybe a better filter needed?

            MethodBase[] zx = impl_type.GetMethods(
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static);

            foreach (MethodBase mx in zx)
            {
                if (mx.Name == (impl_methodname == null ? src_method.Name : impl_methodname))
                {
                    // implementation
                    ParameterInfo[] za = mx.GetParameters();
                    // original
                    ParameterInfo[] zb = src_method.GetParameters();

                    /// FIXME: failed to spot overrides??
                    if (za.Length == zb.Length && (src_method.IsConstructor ? mx.IsStatic : mx.IsStatic == src_method.IsStatic))
                    {
                        int zz = za.Length;
                        int zf = 0;

                        while (zz-- > 0)
                        {
                            if (za[zz].ParameterType.GUID == zb[zz].ParameterType.GUID)
                            {
                                zf++;
                            }
                            else
                            {
                                if (za[zz].ParameterType == impl_type)
                                {
                                    if (src_method.DeclaringType.GUID == zb[zz].ParameterType.GUID)
                                    {
                                        zf++;
                                    }

                                }
                            }


                        }

                        if (zf == za.Length)
                        {

                            return mx;
                        }
                    }


                }


            }
            return null;

        }

        public MethodBase ResolveImplementationTrace(Type src_type, MethodBase src_method)
        {
            Type impl_type = ResolveImplementationTrace(src_type);

            if (impl_type == null)
            {
                Console.WriteLine("no implementation for {0} {1}", src_type, src_type.GUID);
                return null;
            }

            Type[] pt = ParameterArrayToTypeArray(src_method.GetParameters(), src_type, impl_type);

            for (int i = 0; i < pt.Length; i++)
                Console.WriteLine("arg[{0}] is typeof {1}", i, pt[i].FullName);


            MethodBase b = null;

			if (src_method.IsInstanceConstructor())
            {
                b = impl_type.GetConstructor(pt);
            }
            else
            {
                b = ResolveMethod(src_method, impl_type, null);

                return b;
            }

            if (b == null)
            {
                Console.WriteLine("no matching prototype");
            }
            else
                Console.WriteLine("found matching prototype");

            return (b != null && b.IsStatic == src_method.IsStatic) ? b : null;

        }

        public MethodBase ResolveImplementation(Type src_type, MethodBase src_method)
        {
            return ResolveImplementation(src_type, src_method, ResolveImplementationDirectMode.ResolveBCLImplementation);
        }

        public MethodBase ResolveImplementation(Type src_type, MethodBase src_method, ResolveImplementationDirectMode Mode)
        {
            // todo: cache results

            Type impl_type =

                Mode == ResolveImplementationDirectMode.ResolveMethodOnly ?
                src_type :
                ResolveImplementation(
               src_type, Mode
                );

            if (impl_type == null)
                return null;


            MethodBase b = null;

            ParameterInfo[] pi = src_method.GetParameters();

            Type timpl = impl_type.IsGenericType ? impl_type.MakeGenericType(src_type.GetGenericArguments()) : impl_type;
            Type[] t = new Type[pi.Length];

            for (int i = 0; i < pi.Length; i++)
            {
                Type v = pi[i].ParameterType;

                if (v.IsGenericType && !v.IsGenericTypeDefinition)
                    v = v.GetGenericTypeDefinition();


                t[i] = v;


            }


            #region IsConstructor
			if (src_method.IsInstanceConstructor())
            {
                // b = timpl.GetConstructor(t);

                foreach (ConstructorInfo v in timpl.GetConstructors(
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance //| BindingFlags.Static 
                    ))
                {
                    ParameterInfo[] vp = v.GetParameters();

                    if (vp.Length != t.Length)
                        continue;

                    Type[] vpt = new Type[vp.Length];

                    for (int i = 0; i < vp.Length; i++)
                    {
                        Type v2 = vp[i].ParameterType;

                        if (v2.IsGenericType && !v2.IsGenericTypeDefinition)
                            v2 = v2.GetGenericTypeDefinition();


                        vpt[i] = v2;


                    }


                    for (int i = 0; i < vp.Length; i++)
                    {
                        // fixme generic type comparision

                        if (vpt[i].GUID == t[i].GUID)
                            continue;

                        if (vpt[i].GUID == impl_type.GUID)
                            if (src_type.GUID == t[i].GUID)
                                continue;


                        goto skip;

                    }

                    b = v;
                    break;
                skip: ;
                }
            }
            #endregion
            else
            {
                string MethodName = ((MethodInfo)src_method).Name;

                var AllMethods = timpl.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);


                foreach (MethodInfo v in AllMethods.Where(n => n.Name == MethodName))
                {
                    ParameterInfo[] vp = v.GetParameters();

                    if (vp.Length != t.Length)
                        if (Mode == ResolveImplementationDirectMode.ResolveNativeImplementationExtension &&
                            t.Length + 1 == vp.Length &&
                            v.IsStatic && !src_method.IsStatic &&
                            vp[0].ParameterType == src_method.DeclaringType)
                        {
                            vp = vp.Skip(1).ToArray();
                        }
                        else
                            continue;

                    Type[] vpt = new Type[vp.Length];



                    Func<Type, Type> ToGTD =
                        i =>
                            (i.IsGenericType && !i.IsGenericTypeDefinition)
                            ? i.GetGenericTypeDefinition() : i;



                    Func<Type, Type> ToElementIfAny =
                        z =>
                        {
                            if (z.IsArray)
                                return z.GetElementType();

                            return z;
                        };

                    for (int i = 0; i < vp.Length; i++)
                    {
                        vpt[i] = ToGTD(vp[i].ParameterType);


                    }




                    Func<Type, bool> IsGenericParameter =
                        i => i.IsGenericParameter || i.IsArray && i.GetElementType().IsGenericParameter;



                    for (int i = 0; i < vp.Length; i++)
                    {
                        // fixme generic type comparision

                        var vpt_i = vpt[i];
                        var t_i = t[i];

                        if (IsGenericParameter(vpt_i) && IsGenericParameter(t_i))
                            continue;

                        if (vpt_i.IsArray && t_i.IsArray)
                        {
                            var vpt_ie = vpt_i.GetElementType();
                            var t_ie = t_i.GetElementType();

                            if (IsGenericParameter(vpt_ie) && !IsGenericParameter(t_ie))
                            {
                                continue;
                            }

                            if (vpt_ie != t_ie)
                            {
                                goto skip;
                            }
                        }


                        // extension method Enumerable.Contains
                        if (vpt_i.IsGenericParameter && !t_i.IsGenericParameter)
                            continue;

                        if (vpt_i.GUID == t_i.GUID)
                            continue;

                        if (vpt_i.GUID == impl_type.GUID)
                            if (src_type.GUID == t_i.GUID)
                                continue;


                        goto skip;

                    }

                    var SourceMethodReturnType = ToElementIfAny(ToGTD(((MethodInfo)src_method).ReturnType));
                    var CurrentMethodReturnType = ToElementIfAny(ToGTD(v.ReturnType));

                    if (!(IsGenericParameter(SourceMethodReturnType) || IsGenericParameter(CurrentMethodReturnType)))
                        if (SourceMethodReturnType != CurrentMethodReturnType)
                            if (ResolveImplementation(SourceMethodReturnType, ResolveImplementationDirectMode.ResolveBCLImplementation) != CurrentMethodReturnType)
                                if (ResolveImplementation(CurrentMethodReturnType, ResolveImplementationDirectMode.ResolveBCLImplementation) != SourceMethodReturnType)
                                    goto skip;


                    b = v;
                    break;
                skip: ;
                }



            }

            if (b == null)
                return null;

            if (Mode == ResolveImplementationDirectMode.ResolveNativeImplementationExtension)
                return b;

            return b.IsStatic == src_method.IsStatic ? b : null;

        }

        public Type ResolveImplementationMethodTrace(MethodBase e)
        {
            Type x = e.DeclaringType;
            MethodBase m = e;


            return null;
        }

        public Type ResolveImplementationTrace(Type e)
        {
            if (ScriptAttribute.Of(e) != null)
            {
                Console.WriteLine("type [{0}] has not script attribute", e.FullName);
                return null;


            }

            foreach (Type z in ImplementationTypes)
            {

                ScriptAttribute sa = ScriptAttribute.Of(z);



                if (sa == null)
                    continue;

                if (sa.Implements == null)
                    continue;

                if (sa.Implements != null)
                {
                    Console.Write("impl:type: {0} {1} ", z.FullName, z.GUID);
                    Console.WriteLine(" - {0} {1}", sa.Implements.FullName, sa.Implements.GUID);

                    // we are looking for an implementation for a generic type
                    if (e.IsGenericType)
                    {
                        if (z.IsGenericTypeDefinition)
                        {
                            Type gtd = e.GetGenericTypeDefinition();

                            if (sa.Implements.GUID.Equals(gtd.GUID))
                            {
                                Type xt = z.MakeGenericType(e.GetGenericArguments());

                                return xt;
                            }

                            // Type ztd = sa.Implements.GetGenericTypeDefinition();

                        }
                    }

                    // sa.implements: placeholder
                    // z: found class

                    if (sa.Implements.GUID.Equals(e.GUID))
                    {
                        return z;
                    }
                }




            }


            return null;
        }

        Dictionary<Type, Type> ResolveImplementationDict = new Dictionary<Type, Type>();

        public Type ResolveImplementation(Type e)
        {
            return ResolveImplementation(e, ResolveImplementationDirectMode.ResolveBCLImplementation);
        }

        public Type ResolveImplementation(Type e, ResolveImplementationDirectMode Mode)
        {
            if (e == null)
                return null;


            if (
				Mode == ResolveImplementationDirectMode.ResolveBCLImplementation)
            {
                if (ResolveImplementationDict.ContainsKey(e))
                    return ResolveImplementationDict[e];

                return ResolveImplementationDict[e] = ResolveImplementationDirect(e, Mode);
            }
            else
                return ResolveImplementationDirect(e, Mode);
        }

        public enum ResolveImplementationDirectMode
        {
            ResolveBCLImplementation,
            ResolveNativeImplementationExtension,
            ResolveMethodOnly,

			ResolveBCLTypeFromScriptIsNativeType,

        }

        Type ResolveImplementationDirect(Type e, ResolveImplementationDirectMode Mode)
        {
            if (e == null)
                return null;

            if (Mode == ResolveImplementationDirectMode.ResolveBCLImplementation)
                if (e.ToScriptAttribute() != null)
                    return null;

			if (Mode == ResolveImplementationDirectMode.ResolveBCLTypeFromScriptIsNativeType)
				if (e.ToScriptAttribute() == null)
					return null;


            Type eg = (e.IsGenericType ? e.GetGenericTypeDefinition() : e);

			if (Mode == ResolveImplementationDirectMode.ResolveBCLTypeFromScriptIsNativeType)
			{
				// For java.lang.Integer we shall return global::System.Int32
				//
				//    [Script(Implements = typeof(global::System.Int32)
				//    , ImplementationType = typeof(java.lang.Integer)
				//    )]
				//	internal class __Int32

				return Enumerable.FirstOrDefault(
					from z in ImplementationTypes
					let sa = ScriptAttribute.OfProvider(z)
					where sa != null
					where sa.Implements != null
					where sa.ImplementationType != null
					where sa.ImplementationType == e
					select sa.Implements
				);
			}

            foreach (var i in
                from z in ImplementationTypes
                let sa = ScriptAttribute.OfProvider(z)
                where sa != null
                where sa.Implements != null
                select new { z, sa })
            {
				if (i.sa.Implements.Equals(eg))
					//if (i.sa.Implements.GUID.Equals(eg.GUID))
                    return i.z;

            }

            return null;
        }


    }
}
