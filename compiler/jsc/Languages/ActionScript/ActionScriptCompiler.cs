using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "as";

        public readonly AssamblyTypeInfo MySession;

        public ActionScriptCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }

        private void GetImportTypesFromMethod(Type t, List<Type> imp, MethodBase v)
        {
            if (v == null)
                return;

            var vs = v.ToScriptAttribute();


            // DebugBreak(vs);

            if (vs != null && vs.DefineAsStatic)
                imp.Add(t);

            DebugBreak(vs);

            //imp.AddRange(GetMethodExceptions(v));

            foreach (ParameterInfo p in v.GetParameters())
            {
                if (p.ParameterType.IsByRef)
                {
                    // fixme: add byref support
                }
                else
                {
                    imp.Add(p.ParameterType);
                }
            }

            if (v.IsAbstract)
                return;

            var body = v.GetMethodBody();

            if (body != null)
            {
                foreach (LocalVariableInfo l in body.LocalVariables)
                {
                    imp.Add(l.LocalType);
                }

                ILBlock b = new ILBlock(v);

                //for (var i = b.First; i != null; i = i.Next)
                foreach (var i in b.Instructrions)
                {
                    if (i == OpCodes.Nop)
                        continue;

                    if (i == OpCodes.Castclass)
                    {
                        imp.Add(MySession.ResolveImplementation(i.ReferencedType));
                        continue;
                    }

                    if (i == OpCodes.Call && i.ReferencedMethod != null)
                    {
                        // jsc:actionscript allows to define new methods on native types
                        // but the implementations must reside in a non-native static class
                        // this is how the add event (+=) and remove event (-=) is made possible

                        if (i.ReferencedMethod.ToScriptAttributeOrDefault().NotImplementedHere)
                            imp.Add(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension) ?? i.ReferencedMethod.DeclaringType);
                        else
                            imp.Add(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType) ?? i.ReferencedMethod.DeclaringType);

                        continue;
                    }

                    if (i == OpCodes.Ldtoken)
                    {
                        imp.Add(MySession.ResolveImplementation(typeof(RuntimeTypeHandle)));

                        // A RuntimeHandle can be a fieldref/fielddef, a methodref/methoddef, or a typeref/typedef.
                        var RuntimeTypeHandle_Type = i.TargetType;

                        imp.Add(MySession.ResolveImplementation(RuntimeTypeHandle_Type) ?? RuntimeTypeHandle_Type);

                        continue;
                    }

                    if (i == OpCodes.Ldvirtftn)
                    {
                        imp.Add(typeof(IntPtr));
                        continue;
                    }

                    if (i == OpCodes.Ldftn)
                    {
                        imp.Add(typeof(IntPtr));
                        continue;
                    }

                    if (i == OpCodes.Box)
                    {

                        if (i.TargetType.IsGenericParameter)
                        {
                            // http://msdn2.microsoft.com/en-us/library/system.type.getgenericparameterconstraints(VS.80).aspx
                            var c = i.TargetType.GetGenericParameterConstraints().SingleOrDefault();

                            if (c != null)
                                imp.Add(c);
                        }
                        else
                        {
                            imp.Add(i.TargetType);
                        }
                    }

                    if (i.TargetMethod != null)
                    {
                        var attr = i.TargetMethod.ToScriptAttribute();

                        if (attr != null && attr.NotImplementedHere)
                        {
                            var impl = MySession.ResolveImplementation(i.TargetMethod.DeclaringType, i.TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);

                            if (impl != null)
                                imp.Add(impl.DeclaringType);
                        }
                    }

                    if (i.ReferencedMethod != null)
                    {
                        if (!IsTypeOfOperator(i.ReferencedMethod))
                            if (i.ReferencedMethod.DeclaringType != typeof(object))
                            {
                                if (i.ReferencedMethod.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
                                    continue;

                                if (ScriptAttribute.IsCompilerGenerated(i.ReferencedMethod.DeclaringType))
                                {
                                    imp.Add(i.ReferencedMethod.DeclaringType);
                                    continue;
                                }

                                if (i.ReferencedMethod.DeclaringType.IsInterface)
                                {
                                    imp.Add(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType));
                                    continue;
                                }

                                MethodBase method = GetMethodImplementation(MySession, i);
                                var method_attribute = method.ToScriptAttribute();
                                if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
                                {
                                    imp.Add(method.DeclaringType);
                                    continue;
                                }
                            }
                    }



                    if (i.TargetField != null)
                    {
                        if (i.TargetField.IsStatic)
                        {
                            imp.Add(i.TargetField.DeclaringType);
                            continue;
                        }
                    }
                }
            }
        }

        private readonly Dictionary<Type, IEnumerable<Type>> GetImportTypes_Cache =
            new Dictionary<Type, IEnumerable<Type>>();

        private IEnumerable<Type> GetImportTypes(Type t)
        {
            if (GetImportTypes_Cache.ContainsKey(t))
                return GetImportTypes_Cache[t];


            var imp = new List<Type>();



            if (t.BaseType != null && t.BaseType != typeof(object))
                imp.Add(MySession.ResolveImplementation(t.BaseType));

            if (t == typeof(object))
                return new Type[] { };

            if (t.BaseType == typeof(MulticastDelegate))
            {
                imp.Add(MySession.ResolveImplementation(typeof(IntPtr)));

                var _Invoke = t.GetMethod("Invoke");

                if (_Invoke.ReturnParameter.ParameterType != typeof(void))
                    imp.Add(MySession.ResolveImplementation(_Invoke.ReturnParameter.ParameterType));

                GetImportTypesFromMethod(t, imp, _Invoke);

                goto removesome;
            }

            var tinterfaces = t.GetInterfaces();

            foreach (Type tinterface in tinterfaces)
                imp.Add(tinterface);


            /*
            Type bp = t.BaseType;

            while (bp != typeof(object) &&
                    bp != null)
            {
                imp.Add(bp);
                bp = bp.BaseType;
            }
            */
            foreach (FieldInfo v in this.GetAllFields(t))
            {
                imp.Add(v.FieldType);
            }

            GetImportTypesFromMethod(t, imp, t.GetStaticConstructor());

            foreach (MethodBase v in GetAllInstanceConstructors(t))
            {


                GetImportTypesFromMethod(t, imp, v);
            }

            foreach (MethodInfo mi in this.GetAllMethods(t))
            {
                if (ScriptAttribute.IsAnonymousType(t))
                {
                    if (mi.Name == "GetHashCode") continue;
                    if (mi.Name == "Equals") continue;
                }

                imp.Add(mi.ReturnParameter.ParameterType);

                MethodBase v = mi;

                GetImportTypesFromMethod(t, imp, v);
            }

        removesome:

            var imp_types = new List<Type>();

            imp.RemoveAll(i => i == typeof(void));
            imp.RemoveAll(i => i == null);
            imp.RemoveAll(i => i.IsGenericParameter);

            // todo: import only if used in code...
            imp.Add(GetArrayEnumeratorType());


            while (imp.Count > 0)
            {
                Type p = imp[0];


                // remove duplicates
                imp.RemoveAll(
                     delegate(Type w)
                     {
                         if (w == null)
                             return true;

                         if (w.IsArray && p.IsArray)
                         {
                             return w.GetElementType().GUID == p.GetElementType().GUID;
                         }

                         return w.GUID == p.GUID;
                     }
                 );

                // todo fix additional types handling

                while (p.IsArray)
                {
                    p = p.GetElementType();

                }

                if (p.IsGenericParameter)
                    continue;

                if (p.IsEnum)
                    continue;

                if (p == typeof(object)) continue;
                if (p == typeof(void)) continue;
                if (p == typeof(string)) continue;

                if (p == typeof(int)) continue;
                if (p == typeof(uint)) continue;

                if (p == typeof(short)) continue;
                if (p == typeof(ushort)) continue;

                if (p == typeof(long)) continue;
                if (p == typeof(ulong)) continue;

                if (p == typeof(double)) continue;
                if (p == typeof(float)) continue;
                if (p == typeof(decimal)) continue;

                if (p == typeof(byte)) continue;
                if (p == typeof(sbyte)) continue;

                if (p == typeof(bool)) continue;

                if (p == typeof(char)) continue;

                // is a BCL type
                var a = p.ToScriptAttribute();

                if (a == null)
                {
                    if (ScriptAttribute.IsCompilerGenerated(p))
                    {
                        imp_types.Add(p);

                        continue;
                    }

                    // and has an implementation type
                    var p_impl = MySession.ResolveImplementation(p);

                    if (p_impl == null)
                    {
                        //if (p.IsInterface)
                        //{
                        //    // silently skip this interface
                        //    continue;
                        //}

                        Break("class import: no implementation for " + p.ToString() + " at " + t.FullName);
                    }

                    p = p_impl;
                    // a = ScriptAttribute.Of(p, true);
                }


                imp_types.Add(p);


            }

            imp_types.AddRange(
                from i in imp_types.ToArray()
                let ia = i.ToScriptAttribute()
                where ia != null
                where ia.ImplementationType != null
                select ia.ImplementationType
            );


            return GetImportTypes_Cache[t] = imp_types;
        }

        public void WriteImportTypes(Type z)
        {
            // all field types, return types, parameter types, variable types, statics

            DebugBreak(z.ToScriptAttributeOrDefault());

            var t = GetImportTypes(z).ToList();
            var imports = new List<string>();

            imports.AddRange(
                z.GetCustomAttributes<ScriptImportsTypeAttribute>().Select(i => i.Name)
            );

            /*
            t.RemoveAll(delegate(Type x)
            {
                return IsEmptyImplementationType(x);
            });
            */

            while (t.Count > 0)
            {
                Type p = t[0];

                // optimize me

                t.RemoveAll(
                    delegate(Type x)
                    {
                        return x.GUID == z.GUID || x.GUID == p.GUID;
                    }
                );



                var a = ScriptAttribute.Of(p, false);

                var n = NamespaceFixup(p.Namespace) ?? "";

                if (n != "")
                {
                    n += ".";

                    if (a != null && a.Implements != null && a.ExternalTarget != null)
                    {
                        if (p != z)
                        {

                            imports.Add(n + GetDecoratedTypeName(p, false));
                        }

                        imports.Add(GetDecoratedTypeName(p, true));


                    }
                    else
                    {

                        imports.Add(n + GetDecoratedTypeName(p, true));



                    }
                }
                // else top Level - no need to import



            }


            imports.Sort(
               delegate(string x, string y)
               {
                   return x.CompareTo(y);
               });

            foreach (string var in imports)
            {

                WriteIdent();

                Write("import ");
                Write(/*NamespaceFixup(*/var/*)*/);
                WriteLine(";");

            }


        }





        public override void WriteMethodParameterList(MethodBase m)
        {
            WriteMethodParameterList(m, null, null);
        }

        public void WriteMethodParameterList(MethodBase m, ILFlow.StackItem[] DefaultValues, Action<Action> AddDefaultVariableInitializer)
        {
            ParameterInfo[] mp = m.GetParameters();

            var ma = m.ToScriptAttribute();


            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);

                // cannot use 'this' on arguments as it is a keyword
                WriteSelf();
                Write(":");

                if (m.DeclaringType.ToScriptAttributeOrDefault().Implements == typeof(object))
                {
                    Write(NativeTypes[typeof(object)]);
                }
                else
                {
                    WriteDecoratedTypeNameOrImplementationTypeName(m.DeclaringType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, m.DeclaringType));
                }

          
            }

            DebugBreak(ma);

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                ParameterInfo p = mp[mpi];

                ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);


                var ParamIndex = mpi;

                // Nameless params is used by delegates and these parameters are not used
                WriteMethodParameter(ParamIndex, p);

                var ParameterType = p.ParameterType;

                // A NativeExtension class should never define a variable to its type rather the native type
                if (ParameterType == m.DeclaringType && m.DeclaringType.IsNativeTypeExtension())
                    ParameterType = za.Implements;

                Write(":");

                // fixme: byref supported?

                if (ParameterType.IsByRef)
                    Write("*");
                else
                    WriteDecoratedTypeNameOrImplementationTypeName(ParameterType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, ParameterType));

                if (DefaultValues != null && mpi < DefaultValues.Length)
                {
                    WriteAssignment();

                    // if the value aint literal we cannot use it with
                    // the curent actionscript compiler

                    var DefaultValue = DefaultValues[mpi].SingleStackInstruction;

                    if (DefaultValue.IsLiteral)
                        EmitInstruction(null, DefaultValue);
                    else
                    {
                        WriteKeywordNull();

                        if (AddDefaultVariableInitializer == null)
                            throw new NullReferenceException("AddDefaultVariableInitializer");

                        AddDefaultVariableInitializer(
                            delegate
                            {
                                WriteIdent();
                                Write("if (");
                                WriteMethodParameter(ParamIndex, p);
                                Write(" == null) ");
                                WriteMethodParameter(ParamIndex, p);
                                WriteAssignment();
                                EmitInstruction(null, DefaultValue);
                                WriteLine(";");
                            }
                        );
                    }
                }
                /*
                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteDecoratedTypeName(p.ParameterType);
                else
                    WriteDecoratedTypeName(za.Implements);
                */

            }
        }

        /// <summary>
        /// Some parameters can be nameless which are used by delegates and these parameters are not used
        /// </summary>
        /// <param name="mpi"></param>
        /// <param name="p"></param>
        private void WriteMethodParameter(int mpi, ParameterInfo p)
        {
            if (string.IsNullOrEmpty(p.Name))
                Write("_" + mpi);
            else
                WriteDecoratedMethodParameter(p);
        }

        public void ConvertTypeAndEmit(CodeEmitArgs e, Type x)
        {

            var s = e.i.StackBeforeStrict.Single().SingleStackInstruction;

            if (s.OpCode == OpCodes.Box)
            {
                var BoxParam = s.StackBeforeStrict.Single();
                var BoxParamType = BoxParam.SingleStackInstruction.ReferencedType;

                if ((MySession.ResolveImplementation(BoxParamType) ?? BoxParamType) == x)
                {
                    Emit(e.p, BoxParam);

                    return;
                }
            }

            var r = s.ReferencedType;
            var ra = r.ToScriptAttribute();


            if (r == x || (ra != null && ra.IsArray))
            {
                EmitFirstOnStack(e);
                return;
            }

            // prevent compiler being funny: a.Add_100664081((*(e)));
            if (x.IsGenericParameter)
            {
                EmitFirstOnStack(e);
            }
            else
            {
                Write("(");
                WriteDecoratedTypeNameOrImplementationTypeName(x, true, true, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, x));
                Write("(");
                EmitFirstOnStack(e);
                Write(")");
                Write(")");
            }
        }

        public override void ConvertTypeAndEmit(CodeEmitArgs e, string x)
        {
            Write("(" + x + "(");
            EmitFirstOnStack(e);
            Write("))");
        }

        Type CachedArrayEnumeratorType;

        public Type GetArrayEnumeratorType()
        {
            return CachedArrayEnumeratorType ?? (CachedArrayEnumeratorType = (from i in MySession.ImplementationTypes
                                                                              let a = i.ToScriptAttribute()
                                                                              where a != null
                                                                              where a.IsArrayEnumerator
                                                                              select i).SingleOrDefault());
        }

        public override void WriteArrayToCustomArrayEnumeratorCast(Type Enumerable, Type ElementType, ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            var x = GetArrayEnumeratorType();
            if (x == null)
                throw new Exception("SZArrayEnumerator is missing");

            var ArrayToEnumerator = x.GetImplicitOperators(null, null).Single();

            WriteDecoratedTypeNameOrImplementationTypeName(x, false, false, IsFullyQualifiedNamesRequired(p.DeclaringMethod.DeclaringType, x));
            Write(".");
            WriteDecoratedMethodName(ArrayToEnumerator, false);
            Write("(");

            Emit(p, s);

            Write(")");

        }

        public override string GetDecoratedMethodParameter(ParameterInfo p)
        {
            return GetSafeLiteral(p.Name);
        }




        public static uint[] StructAsUInt32Array(object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new uint[size / sizeof(int)];

            unsafe
            {
                var p = (uint*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }

        public static int[] StructAsInt32Array(object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new int[size / sizeof(int)];

            unsafe
            {
                int* p = (int*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }

        public override void WriteDecoratedTypeName(Type context, Type subject)
        {
            // used by OpCodes.Newobj

            WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject));

        }

        public void WriteDecoratedTypeName(Type context, Type subject, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject), Mode );

        }

        protected override void WriteTypeInstanceConstructors(Type z)
        {
            var zci = GetAllInstanceConstructors(z);

            if (zci.Length > 1)
            {
                // visual basic can have optional parameters on its own, its c# that needs some help
                // as3 does not support method overloading but does support default parameters
                // we need to figure out which ctor is real and which are just sattelites

                // default values should be extended to allow instance values
                // a workaround could be:
                // if (param == null) { param = UIComponent; } 

                var query =
                    from c in zci
                    let b = new ILBlock(c).Prestatements.PrestatementCommands.Where(p => !p.Instruction.IsAnyOpCodeOf(OpCodes.Ret, OpCodes.Nop)).ToArray()
                    where (b.Length == 1 && b[0].Instruction == OpCodes.Call)
                    let i = b[0].Instruction
                    let t = i.TargetConstructor
                    where t != null && zci.Contains(t)
                    // skip the ldarg0/this op
                    select new { ctor = c, target = t, args = i.StackBeforeStrict.Skip(1).ToArray() };

                var cache = query.ToArray();
                var targets = zci.Except(cache.Select(i => i.ctor)).ToArray();


                if (targets.Length != 1)
                    throw new NotSupportedException("Unable to transform overloaded constructors to a single constructor via optional parameters for " + z.FullName);

                var target = targets.Single();


                // step 1
                var ctor = cache.Single(i => i.target == target);
                var args = ctor.args;

                while (true)
                {
                    ctor = cache.SingleOrDefault(i => i.target == ctor.ctor);

                    if (ctor == null)
                        break;

                    args = args.Select((s, i) => s.SingleStackInstruction.TargetParameter == null ? s : ctor.args[i]).ToArray();
                }

                // now we should have one ctor and others that point to them

                Action CustomVariableInitialization = delegate { };

                WriteMethodSignature(target, false, WriteMethodSignatureMode.Delcaring, args, i => CustomVariableInitialization += i, null);
                WriteMethodBody(target, this.MethodBodyFilter, CustomVariableInitialization);


            }
            else
            {
                foreach (var zc in zci)
                {
                    WriteMethodSignature(zc, false);
                    WriteMethodBody(zc);

                }
            }

            WriteLine();
        }
    }


}
