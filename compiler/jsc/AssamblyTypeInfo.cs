using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text;


using ScriptCoreLib;

namespace jsc
{
    public class AssamblyTypeInfo
    {
        public CommandLineOptions Options;

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

            if (src_method.IsConstructor)
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

            Type impl_type = ResolveImplementation(
               src_type
                );

            if (impl_type == null)
                return null;



            //Type[] pt = ParameterArrayToTypeArray(


            //    src_method.GetParameters(), src_type, impl_type);


            

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



            if (src_method.IsConstructor)
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
                skip:;
                }
            }
            else
            {
                string MethodName = ((MethodInfo)src_method).Name;

                foreach (MethodInfo v in timpl.GetMethods(
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.Static 
                    ))
                {
                    if (v.Name != MethodName)
                        continue;

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
                skip:;
                }

               

            }


            return (b != null && b.IsStatic == src_method.IsStatic) ? b : null;

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
            if (e == null)
                return null;

            if (ResolveImplementationDict.ContainsKey(e))
                return ResolveImplementationDict[e];

            return ResolveImplementationDict[e] = ResolveImplementationDirect(e);
        }

        Type ResolveImplementationDirect(Type e)
        {
            if (e == null)
                return null;

            

            if (ScriptAttribute.OfProvider(e) != null)
                return null;


            Type eg = (e.IsGenericType ? e.GetGenericTypeDefinition() : e);

            foreach (Type z in ImplementationTypes)
            {

                ScriptAttribute sa = ScriptAttribute.OfProvider(z);

                if (sa == null)
                    continue;

                if (sa.Implements == null)
                    continue;

                //if (e.IsGenericType)
                //{
                //    if (z.IsGenericTypeDefinition)
                //    {
                //        Type gtd = e.GetGenericTypeDefinition();

                //        if (sa.Implements.GUID.Equals(gtd.GUID))
                //        {
                //            Type xt = z.MakeGenericType(e.GetGenericArguments());

                //            return xt;
                //        }
                //    }
                //}
                //else
                if (sa.Implements.GUID.Equals(eg.GUID))
                    return z;

            }

            return null;
        }


    }
}
