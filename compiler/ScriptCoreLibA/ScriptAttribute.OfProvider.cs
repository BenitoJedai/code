using System.Runtime.CompilerServices;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib.CSharp.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib
{
    partial class ScriptAttribute
    {
        static Dictionary<Type, ScriptAttribute> CachedOfProvider = new Dictionary<Type, ScriptAttribute>();

        public static ScriptAttribute OfProvider(ICustomAttributeProvider m)
        {

            if (m is Type)
            {
                var t = m as Type;

            
                if (t.IsArray)
                    t = t.GetElementType();

                if (t.IsGenericParameter)
                    return null;

                if (t.IsGenericType && !t.IsGenericTypeDefinition)
                    t = t.GetGenericTypeDefinition();



                if (!CachedOfProvider.ContainsKey(t))
                {
                    // ah must be the first time.
                    // let's cache all types.. are we breaking anything by doing this?

                    var Types = t.Assembly.GetTypes();

                    Action<Type> f = null;

                    f = item =>
                        {
                            CachedOfProvider[item] = InternalOfProvider(item);

                            foreach (var item2 in item.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
	                        {
                                f(item2);
	                        }
                           
                        };



                    foreach (var item in Types)
                    {
                        f(item);
                    }

                    
                }

                if (CachedOfProvider.ContainsKey(t))
                {
                    return CachedOfProvider[t];
                }
            }

            return InternalOfProvider(m);
        }

        static ScriptAttribute InternalOfProvider(ICustomAttributeProvider m)
        {
            // first call to this method shall prepare the cache for all types in the same assembly

            if (m == null)
                return null;

            try
            {
                if (m is Type)
                {
                    // a context assembly can define any type to not be translated
                    // we might want to cache this
                    if (OfProviderContext.Any(k => k.NonScriptTypes.Contains(m)))
                        return null;
                }

                var s = new ScriptAttribute[0];

                try
                {
                    s = m.GetCustomAttributes(typeof(ScriptAttribute), false) as ScriptAttribute[];
                }
                catch
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130304-net-4-0
                    // most likely type cannot be loaded!
                }

                var x = s.Length == 0 ? null : s[0];

                var t = m as Type;
                if (t != null)
                {
                    var ts = t.Assembly.ToScriptAttributeOrDefault();

                    if (ts.NonScriptTypes != null && ts.NonScriptTypes.Contains(t))
                        return null;

                    if (ts.IsScriptLibrary)
                    {
                        // we should not be overriding the script attribute if it already exists.

                        if (x == null)
                            x = new ScriptAttribute();
                    }

                }

                if (x == null)
                {
                    if (IsScriptLibraryViaObfuscationAttribute(m))
                        x = new ScriptAttribute();
                }


                // Whatif a corelib was marked as a IsScriptLib
                // this will confuse jsc currently...

                //if (x == null)

                if (x == null)
                    if (t != null)
                        if (Enumerable.Any(
                            from p in OfProviderContext
                            let ScriptLibraries = p.Context.ToScriptAttributeOrDefault().ScriptLibraries
                            where ScriptLibraries != null
                            from l in ScriptLibraries

                            // A library which takes matters in its own hand
                            // should keep doing that...

                            where l.Assembly.GetCustomAttributes(typeof(ScriptAttribute), false).Length == 0
                            where l.Assembly == t.Assembly

                            select new { p, l }
                            ))
                            x = new ScriptAttribute();


                return x;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }



}
