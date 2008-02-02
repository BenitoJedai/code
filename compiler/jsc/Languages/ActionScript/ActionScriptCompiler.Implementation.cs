using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
        // lets use for instead of while where we can
        public override bool SupportsForStatements
        {
            get { return true; }
        }

        public override bool SupportsInlineAssigments
        {
            get { return true; }
        }

        public override bool SupportsInlineArrayInit
        {
            get { return true; }
        }

        public override bool SupportsInlineThisReference
        {
            get { return true; }
        }

        public override ScriptCoreLib.ScriptType GetScriptType()
        {
            return ScriptCoreLib.ScriptType.ActionScript;
        }

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            WriteIdent();
            Write("package " + NamespaceFixup(z.Namespace));
            WriteLine();

            using (CreateScope())
            {
                WriteImportTypes(z);

                WriteLine();

                var za = ScriptAttribute.Of(z, true);

                #region type summary
                var u = GetXMLNode(z);

                if (u != null)
                    WriteBlockComment(u["summary"].InnerText);
                #endregion

                WriteTypeSignature(z, za);

                using (CreateScope())
                {
                    if (z.IsDelegate())
                    {
                        this.WriteDelegate(z, za);
                    }
                    else
                    {
                        WriteTypeFields(z, za);
                        WriteLine();

                        WriteTypeInstanceConstructors(z);
                        WriteLine();

                        WriteTypeInstanceMethods(z, za);
                        WriteLine();

                        WriteTypeStaticMethods(z, za);
                        WriteLine();
                    }

                }
            }

            return true;
        }

        public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            DebugBreak(za);


            WriteIdent();

            if (za.Implements != null)
            {
                // private for developers but public for the runtime

                Write("public ");
            }
            else
            {
                if (z.IsPublic)
                    Write("public ");
                else
                    Write("internal ");
            }

            if (z.IsSealed)
                Write("final ");


            Write("class ");
            WriteDecoratedTypeName(z);

            var BaseTypeImplementation =
                z.BaseType == typeof(object) ? z.BaseType :
                MySession.ResolveImplementation(z.BaseType) ?? z.BaseType;

            #region extends
            if (BaseTypeImplementation != typeof(object) && BaseTypeImplementation != null)
            {
                Write(" extends ");



                ScriptAttribute ba = ScriptAttribute.Of(BaseTypeImplementation, true);

                if (ba == null)
                    throw new NotSupportedException("extending object has no attribute");


                if (ba.Implements == null)
                    WriteDecoratedTypeName(BaseTypeImplementation);
                else
                    Write(GetDecoratedTypeName(BaseTypeImplementation, false));

            }
            #endregion

            #region implements
            Type[] timp = z.GetInterfaces();

            if (timp.Length > 0)
            {


                int i = 0;

                DebugBreak(za);

                foreach (Type timpv in timp)
                {
                    // ignore interfaces which are not visible to scripting
                    if (timpv.ToScriptAttribute() == null)
                        continue;

                    if (i++ > 0)
                        Write(", ");
                    else
                        Write(" implements ");

                    WriteDecoratedTypeName(timpv);
                    //WriteDecoratedTypeNameOrImplementationTypeName(timpv);
                }
            }
            #endregion

            WriteLine();
        }

        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            FieldInfo[] zf = GetAllFields(z);

            foreach (FieldInfo zfn in zf)
            {
                // external class cannot have static variables inside a type
                // should be defined outside as global static instead
                if (za.HasNoPrototype && !zfn.IsStatic)
                    continue;

                if (zfn.IsLiteral)
                    continue;

                // write the attributes for current field
                foreach (var v in from i in zfn.GetCustomAttributes(false)
                                  let type = i.GetType()
                                  let meta = ScriptAttribute.Of(type)
                                  where meta != null
                                  let name = type.Name.Substring(0, type.Name.Length - "Attribute".Length)
                                  let fields = type.GetFields()
                                  select new { name, type, i, meta, fields })
                {
                    WriteIdent();
                    Write("[");
                    WriteSafeLiteral(v.name);
                    Write("(");

                    v.fields.Aggregate("",
                        (seed, f) =>
                        {
                            Write(seed);

                            Write(f.Name);
                            WriteAssignment();

                            if (f.FieldType == typeof(string))
                            {
                                WriteQuotedLiteral((string)f.GetValue(v.i));
                            }
                            else
                                throw new NotImplementedException();

                            return ", ";
                        }
                    );


                    Write(")");
                    Write("]");
                    WriteLine();
                }

                WriteIdent();
                WriteTypeFieldModifier(zfn);

                Write("var ");
                WriteSafeLiteral(zfn.Name);
                Write(":");
                //WriteGenericOrDecoratedTypeName(zfn.FieldType);
                WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);

                //WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);
                //WriteSpace();

                //WriteVariableType(zfn.FieldType, true);

                /*
                if (zfn.IsStatic && zfn.IsInitOnly)
                {
                    ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

                    if (ci.Length == 1)
                    {
                        ILBlock cctor = new ILBlock(ci[0]);
                        ILBlock.Prestatement assign = cctor.GetStaticFieldFinalAssignment(zfn);

                        if (assign != null)
                        {
                            WriteAssignment();

                            EmitFirstOnStack(assign);
                        }
                    }
                }
                */

                WriteLine(";");
            }
        }




        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            if (zfn.IsPublic)
                Write("public ");
            else
            {
                if (zfn.IsFamily)
                    Write("protected ");
                else
                    Write("private ");
            }
            /*
            if (zfn.IsInitOnly)
                WriteKeywordFinal();
            */
            if (zfn.IsStatic)
                Write("static ");

            /*
            if (zfn.IsNotSerialized)
                Write("transient ");
             * */
        }



        public override void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic)
        {
            
            var TypeScriptAttribute = m.DeclaringType.ToScriptAttribute();
                
            var IsNativeTarget = TypeScriptAttribute != null && TypeScriptAttribute.IsNative;

            WriteIdent();

            if (m.IsPublic)
                Write("public ");
            else
                if (m.IsFamily)
                    Write("protected ");
                else
                {
                    // cannot use private as it blocks off delegates
                    Write("internal ");
                }


            if (m.IsStatic || dStatic)
                Write("static ");


            Write("function ");

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
            {
                var prop = new PropertyDetector(m);

                if (!dStatic && prop.SetProperty != null)
                {
                    Write("set ");
                    Write(prop.SetProperty.Name);
                }
                else if (!dStatic && prop.GetProperty != null)
                {
                    Write("get ");
                    Write(prop.GetProperty.Name);
                }
                else if (IsNativeTarget)
                {
                    Write(m.Name);
                }
                else
                {

                    WriteDecoratedMethodName(m, false);
                }
            }

            Write("(");
            WriteMethodParameterList(m);
            Write(")");

            #region ReturnType
            var mi = m as MethodInfo;

            if (mi != null)
            {
                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(mi.ReturnType, true, true);
                
            }
            #endregion


            WriteLine();
        }

        class PropertyDetector
        {
            public PropertyInfo SetProperty;
            public PropertyInfo GetProperty;

            public PropertyDetector(MethodBase m)
            {
                if (m.IsConstructor)
                    return;

                #region set
                {
                    var prefix = "set_";
                    if (m.Name.StartsWith(prefix))
                    {
                        SetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length));
                    }
                }
                #endregion

                #region set
                {
                    var prefix = "get_";
                    if (m.Name.StartsWith(prefix))
                    {
                        GetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length));
                    }
                }
                #endregion

            }
        }

        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, System.Reflection.MethodBase m)
        {
            // remove the base call for now

            var TargetMethod = m;
            var MethodScriptAttribute = TargetMethod.ToScriptAttribute();

            if (MethodScriptAttribute != null && MethodScriptAttribute.NotImplementedHere)
            {
                TargetMethod = MySession.ResolveImplementation(m.DeclaringType, TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);
                MethodScriptAttribute = TargetMethod.ToScriptAttribute();
            }

            var TypeScriptAttribute = TargetMethod.DeclaringType.ToScriptAttribute();
            
            



            var IsDefineAsStatic = MethodScriptAttribute != null && MethodScriptAttribute.DefineAsStatic;

            Action WriteMethodName =
                delegate
                {
                    if (TypeScriptAttribute.IsNative)
                        Write(TargetMethod.Name);
                    else
                        WriteDecoratedMethodName(TargetMethod, false);
                };

            var IsBaseConstructorCall = i.IsBaseConstructorCall(TargetMethod);

            var s = i.StackBeforeStrict;
            var offset = 1;

            #region WritePropertyAssignment
            Action<PropertyDetector> WritePropertyAssignment =
                prop =>
                {
                    WriteAssignment();

                    #region bool
                    if (prop.SetProperty.PropertyType == typeof(bool))
                    {
                        if (s[1].StackInstructions.Length == 1)
                        {
                            if (s[1].SingleStackInstruction.TargetInteger == 0)
                            {
                                Write("false");
                                return;
                            }

                            if (s[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                Write("true");
                                return;
                            }
                        }
                    }
                    #endregion

                    Emit(p, s[1]);
                };
            #endregion


            if (TargetMethod.IsStatic || IsDefineAsStatic)
            {
                WriteDecoratedTypeName(TargetMethod.DeclaringType);
                Write(".");

                #region prop
                if (!IsDefineAsStatic)
                {
                    var prop = new PropertyDetector(TargetMethod);

                    if (prop.SetProperty != null)
                    {
                        Write(prop.SetProperty.Name);
                        WritePropertyAssignment(prop);

                        return;
                    }

                    if (prop.GetProperty != null)
                    {
                        Write(prop.GetProperty.Name);
                        return;
                    }
                }
                #endregion

                WriteMethodName();

                if (IsDefineAsStatic)
                    offset = 1;
                else
                    offset = 0;
            }
            else
            {
                if (IsBaseConstructorCall)
                {
                    Write("super");
                }
                else
                {
                    Emit(p, s[0]);
                    Write(".");


                    #region prop
                    {
                        var prop = new PropertyDetector(TargetMethod);

                        if (prop.SetProperty != null)
                        {
                            Write(prop.SetProperty.Name);
                            WritePropertyAssignment(prop);
                            return;
                        }

                        if (prop.GetProperty != null)
                        {
                            Write(prop.GetProperty.Name);
                            return;
                        }
                    }
                    #endregion

                    WriteMethodName();
                }
            }

            WriteParameterInfoFromStack(TargetMethod, p, s, offset);
        }


        public override void WriteSelf()
        {
            Write("_this");
        }



        public override Type[] GetActiveTypes()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m)
        {
            return MySession.ResolveImplementation(t, m);
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias);
        }

        public override void WriteTypeConstructionVerified()
        {
            Write("{}");
        }

        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            throw new NotImplementedException();
        }

        Dictionary<Type, string> NativeTypes =
            new Dictionary<Type, string>
            {
                {typeof(int), "int"},
                {typeof(uint), "uint"},
                {typeof(double), "Number"},
                {typeof(void), "void"},
                {typeof(string), "String"},
                {typeof(object), "Object"},
            };

        public override string GetDecoratedTypeName(Type x, bool bExternalAllowed)
        {
            Func<Type, string> GetShortName =
                z =>
                {
                    if (z.IsArray)
                        return "Array";

                    // convert c# type to actionscript typename literal

                    if (NativeTypes.ContainsKey(z))
                        return NativeTypes[z];


                    return GetSafeLiteral(z.Name);
                };

            var p = x;
            var s = "";

            do
            {
                if (string.IsNullOrEmpty(s))
                    s = GetShortName(p);
                else
                    s = GetShortName(p) + "_" + s;

                p = p.DeclaringType;
            }
            while (p != null);

            return s;
        }

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            // used when writing the source file

            return GetDecoratedTypeName(z, false);
        }


        public override void WriteDecoratedMethodName(System.Reflection.MethodBase z, bool q)
        {
            if (q)
                WriteQuote();

            try
            {
                // tostring is a special method
                if (z.Name == "ToString" && z.GetParameters().Length == 0)
                {
                    Write("toString");
                    return;
                }

                // todo: should use base62 encoding here
                WriteSafeLiteral(z.Name + "_" + z.MetadataToken);

            }
            finally
            {
                if (q)
                    WriteQuote();
            }
        }

        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();

            Write("var ");
            WriteVariableName(u.DeclaringType, u, v);
            Write(":");
            WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
            WriteLine(";");
        }


        public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType)
        {
            if (timpv.IsGenericParameter)
            {
                Write("*");
                return;
            }

            //[Script(Implements = typeof(global::System.Boolean),
            //    ImplementationType=typeof(java.lang.Integer))]

            if (NativeTypes.ContainsKey(timpv))
            {
                // write native
                Write(NativeTypes[timpv]);
                return;
            }



            Type iType = MySession.ResolveImplementation(timpv);

            if (iType != null)
            {
                if (favorTargetType)
                {
                    if (ScriptAttribute.OfProvider(iType).ImplementationType != null)
                        iType = null;
                }
            }





            if (iType == null)
            {
                var s = timpv.ToScriptAttribute();

                // favorPrimitives
                if (s != null && s.ImplementationType != null)
                    WriteSafeLiteral(GetDecoratedTypeName(s.ImplementationType, true/*, favorPrimitives, true*/));
                else
                    WriteSafeLiteral(GetDecoratedTypeName(timpv, true/*, favorPrimitives, true*/));
            }
            else
                WriteSafeLiteral(GetDecoratedTypeName(iType, true));
        }
    }
}
