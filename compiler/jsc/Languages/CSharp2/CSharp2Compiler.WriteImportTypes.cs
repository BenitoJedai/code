using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public void WriteImportTypes(Type z)
        {
            // all field types, return types, parameter types, variable types, statics

            DebugBreak(z.ToScriptAttributeOrDefault());

            var t = GetImportTypes(z).ToList();
            var imports = new List<string>();

    

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

                var n = NamespaceFixup(p.Namespace, p) ?? "";

                if (n != "")
                {
                    imports.Add(n);
                }
                // else top Level - no need to import



            }



            foreach (var v in imports.OrderBy(k => k).Distinct())
            {
                WriteIdent();
                WriteKeywordSpace(Keywords._using);
                Write(v);
                WriteLine(";");
            }


        }

        private readonly Dictionary<Type, IEnumerable<Type>> GetImportTypes_Cache =
    new Dictionary<Type, IEnumerable<Type>>();

        private IEnumerable<Type> GetImportTypes(Type t)
        {
            if (GetImportTypes_Cache.ContainsKey(t))
                return GetImportTypes_Cache[t];


            var imp = new List<Type>();

            #region AddGenericType
            var AddGenericType = default(Action<Type>);

            Action<Type[]> AddGenericTypes = e => Array.ForEach(e, AddGenericType);

            AddGenericType =
                gt =>
                {
                    if (gt.IsGenericType)
                    {
                        AddGenericTypes(gt.GetGenericArguments());
                    }

                    imp.Add(gt);
                };

            AddGenericType = AddGenericType.And(imp.Add).NonNullArgument();

            #endregion

            if (t.BaseType != null && t.BaseType != typeof(object))
                AddGenericType(MySession.ResolveImplementation(t.BaseType));

            if (t == typeof(object))
                return new Type[] { };

            if (t.BaseType == typeof(MulticastDelegate))
            {
                imp.Add(MySession.ResolveImplementation(typeof(IntPtr)));

                var _Invoke = t.GetMethod("Invoke");

                if (_Invoke.ReturnParameter.ParameterType != typeof(void))
                    imp.Add(MySession.ResolveImplementation(_Invoke.ReturnParameter.ParameterType));

                GetImportTypesFromMethod(t,  _Invoke, AddGenericType);

                goto removesome;
            }

			imp.AddRange(
				t.GetCustomAttributes(false).Where(k => !(k is ScriptAttribute)).Select(k => k.GetType())
			);

            var tinterfaces = t.GetInterfaces();

            foreach (var tinterface in tinterfaces)
                AddGenericType(tinterface);


             

            foreach (var v in this.GetAllFields(t))
            {
                AddGenericType(v.FieldType);
            }

            GetImportTypesFromMethod(t, t.GetStaticConstructor(), AddGenericType);

            foreach (MethodBase v in GetAllInstanceConstructors(t))
            {


                GetImportTypesFromMethod(t, v, AddGenericType);
            }

            foreach (MethodInfo mi in this.GetAllMethods(t))
            {
                //if (ScriptAttribute.IsAnonymousType(t))
                //{
                //    if (mi.Name == "GetHashCode") continue;
                //    if (mi.Name == "Equals") continue;
                //}

                AddGenericType(mi.ReturnParameter.ParameterType);

                MethodBase v = mi;

                GetImportTypesFromMethod(t, v, AddGenericType);
            }

        removesome:

            var imp_types = new List<Type>();

            imp.RemoveAll(i => i == typeof(void));
            imp.RemoveAll(i => i == null);
            imp.RemoveAll(i => i.IsGenericParameter);

            // todo: import only if used in code...
            //imp.Add(GetArrayEnumeratorType());


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
                        //Break("class import: no implementation for " + p.ToString() + " at " + t.FullName);
                    }
                    else
                    {
                        p = p_impl;
                    }
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

        private void GetImportTypesFromMethod(Type t, MethodBase v, Action<Type> AddGenericType)
        {
            if (v == null)
                return;

            var vs = v.ToScriptAttribute();


            // DebugBreak(vs);

            if (vs != null && vs.DefineAsStatic)
                AddGenericType(t);

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
                    AddGenericType(p.ParameterType);
                }
            }

            if (v.IsAbstract)
                return;

            var body = v.GetMethodBody();

            if (body != null)
            {
                foreach (LocalVariableInfo l in body.LocalVariables)
                {
                    AddGenericType(l.LocalType);
                }

                ILBlock b = new ILBlock(v);

                //for (var i = b.First; i != null; i = i.Next)
                foreach (var i in b.Instructrions)
                {
                    if (i == OpCodes.Nop)
                        continue;

                    if (i == OpCodes.Castclass)
                    {
                        AddGenericType(MySession.ResolveImplementation(i.ReferencedType) ?? i.ReferencedType);
                        continue;
                    }

                    if (i == OpCodes.Isinst)
                    {
                        AddGenericType(MySession.ResolveImplementation(i.TargetType) ?? i.TargetType);
                        continue;
                    }

                    if (i == OpCodes.Call && i.ReferencedMethod != null)
                    {
                        // jsc:actionscript allows to define new methods on native types
                        // but the implementations must reside in a non-native static class
                        // this is how the add event (+=) and remove event (-=) is made possible

                        if (i.ReferencedMethod.ToScriptAttributeOrDefault().NotImplementedHere)
                            AddGenericType(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension) ?? i.ReferencedMethod.DeclaringType);
                        else
                            AddGenericType(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType) ?? i.ReferencedMethod.DeclaringType);

                        continue;
                    }

                    if (i == OpCodes.Ldtoken)
                    {
                        AddGenericType(MySession.ResolveImplementation(typeof(RuntimeTypeHandle)));

                        // A RuntimeHandle can be a fieldref/fielddef, a methodref/methoddef, or a typeref/typedef.
                        var RuntimeTypeHandle_Type = i.TargetType;

                        AddGenericType(MySession.ResolveImplementation(RuntimeTypeHandle_Type) ?? RuntimeTypeHandle_Type);

                        continue;
                    }

                    if (i == OpCodes.Ldvirtftn)
                    {
                        AddGenericType(typeof(IntPtr));
                        continue;
                    }

                    if (i == OpCodes.Ldftn)
                    {
                        AddGenericType(typeof(IntPtr));
                        continue;
                    }

                    if (i == OpCodes.Box)
                    {

                        if (i.TargetType.IsGenericParameter)
                        {
                            // http://msdn2.microsoft.com/en-us/library/system.type.getgenericparameterconstraints(VS.80).aspx
                            var c = i.TargetType.GetGenericParameterConstraints().SingleOrDefault();

                            if (c != null)
                                AddGenericType(c);
                        }
                        else
                        {
                            AddGenericType(i.TargetType);
                        }
                    }

                    if (i.TargetMethod != null)
                    {
                        var attr = i.TargetMethod.ToScriptAttribute();

                        if (attr != null && attr.NotImplementedHere)
                        {
                            var impl = MySession.ResolveImplementation(i.TargetMethod.DeclaringType, i.TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);

                            if (impl != null)
                                AddGenericType(impl.DeclaringType);
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
                                    AddGenericType(i.ReferencedMethod.DeclaringType);
                                    continue;
                                }

                                if (i.ReferencedMethod.DeclaringType.IsInterface)
                                {
                                    AddGenericType(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType));
                                    continue;
                                }

                                MethodBase method = GetMethodImplementation(MySession, i);
                                var method_attribute = method.ToScriptAttribute();
                                if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
                                {
                                    AddGenericType(method.DeclaringType);
                                    continue;
                                }
                            }
                    }



                    if (i.TargetField != null)
                    {
                        if (i.TargetField.IsStatic)
                        {
                            AddGenericType(i.TargetField.DeclaringType);
                            continue;
                        }
                    }
                }
            }
        }


    }
}
