using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx
{
  

    [DebuggerDisplay("count: {TotalCount}, time: {TotalTime.Span}")]
    public class ReflectionCache
    {
        public readonly static ReflectionCache Default = new ReflectionCache();

        public struct MetadataKey
        {
            public Guid Module;
            public int Target;


        }

        public struct MetadataKeyArguments
        {
            public Module Module;
            public int Target;

            public MethodBase ReferencedMethod;
            public Instruction Instruction;

            public Type[] GenericTypeArguments
            {
                get
                {
                    var x = ReferencedMethod.DeclaringType;
                    if (x != null)
                        if (x.IsGenericTypeDefinition)
                            return x.GetGenericArguments();

                    return null;
                }
            }

            public Type[] GenericMethodArguments
            {
                get
                {
                    var x = ReferencedMethod;
                    if (x.IsGenericMethodDefinition)
                        return x.GetGenericArguments();

                    return null;
                }
            }



            public MetadataKeyArguments(Delegate e)
                : this(e.Method)
            {


            }

            public MetadataKeyArguments(MethodBase e)
            {
                if (e == null) throw new ArgumentNullException();

                

                Module = e.Module;
                Target = e.MetadataToken;
     
                ReferencedMethod = e;
                Instruction = null;
            }

            public MetadataKey ToKey()
            {
                return new MetadataKey { Module = Module.ModuleVersionId, Target = Target };
            }
        }

        [DebuggerDisplay("count: {Value.Count}, time: {PrefetchTime.Value}")]
        public class CachedDictonary<T>
        {

            private readonly Dictionary<MetadataKey, T> Value = new Dictionary<MetadataKey, T>();

            public IEnumerable<T> Values
            {
                get
                {
                    return Value.Values;
                }
            }

            public int Count
            {
                get
                {
                    return Value.Count;
                }
            }

            public Func<MetadataKeyArguments, T> Convert;

            public CachedDictonary(Func<MetadataKeyArguments, T> c)
            {
                Convert = c;
            }



            public TimeCounter PrefetchTime = new TimeCounter();


            public void Prefetch(MetadataKeyArguments e)
            {
                Prefetch(new [] { e });
            }

            public void Prefetch(IEnumerable<MetadataKeyArguments> e)
            {
                using (new TimeCounter(PrefetchTime))
                {
                    foreach (var v in from i in e
                                          where !Value.Keys.Contains(i.ToKey())
                                          select i)
                    {
                        Value[v.ToKey()] = Convert(v);
                    }
                }


            }

            public T this[MetadataKeyArguments i]
            {
                get
                {
                    Prefetch(i);

                    return Value[i.ToKey()];
                }
            }

            public T this[Delegate e]
            {
                get
                {
                    return this[new MetadataKeyArguments(e)];
                }
            }

            public T this[MethodBase e]
            {
                get
                {
                    return this[new MetadataKeyArguments(e)];
                }
            }

            public T this[MetadataKey i]
            {
                get
                {
                    return Value[i];
                }
            }




        }

        public readonly CachedDictonary<InstructionAnalysis> InstructionAnalysis;
        public readonly CachedDictonary<MethodBase> Methods =  new CachedDictonary<MethodBase>(ResolveMethod);
        public readonly CachedDictonary<FieldInfo> Fields = new CachedDictonary<FieldInfo>(ResolveField);
        public readonly CachedDictonary<string> Strings = new CachedDictonary<string>(( i) => i.Module.ResolveString(i.Target));

        #region resolve
        static MethodBase ResolveMethod(MetadataKeyArguments i)
        {
            return i.Module.ResolveMethod(i.Target, i.GenericTypeArguments, i.GenericMethodArguments);
        }

        static FieldInfo ResolveField(MetadataKeyArguments i)
        {
            return i.Module.ResolveField(i.Target, i.GenericTypeArguments, i.GenericMethodArguments);
        }

        InstructionAnalysis ResolveInstructionAnalysis(MetadataKeyArguments i)
        {
            return new InstructionAnalysis(this, Methods[i]);
        }
        #endregion

        public IEnumerable<MethodBase> MethodsForInstructionAnalysis
        {
            get
            {
                return Methods.Values.Where(m => m.GetMethodBody() != null && !InstructionAnalysis.Values.Any(i => i.Value == m));
            }
        }

        public ReflectionCache()
        {
            InstructionAnalysis = new CachedDictonary<InstructionAnalysis>(ResolveInstructionAnalysis);
        }

        public double Speed
        {
            get
            {
                return TotalCount / TotalTime.Span.TotalSeconds;
            }
        }

        public int TotalCount
        {
            get
            {
                return InstructionAnalysis.Count
                + Methods.Count
                + Fields.Count
                + Strings.Count;
            }
        }

        public TimeCounter TotalTime
        {
            get
            {
                return InstructionAnalysis.PrefetchTime
                + Methods.PrefetchTime
                + Fields.PrefetchTime
                + Strings.PrefetchTime;
            }
        }
    }


}
