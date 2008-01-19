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
            Write(z.Name);

            var BaseTypeImplementation = MySession.ResolveImplementation(z.BaseType) ?? z.BaseType;

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
                Write(" implements ");

                int i = 0;

                DebugBreak(za);

                foreach (Type timpv in timp)
                {
                    if (i++ > 0)
                        Write(", ");

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
                
                if (zfn.FieldType.IsGenericParameter)
                    Write("*");
                else
                    WriteDecoratedTypeName(zfn.FieldType);

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
                Write("private ");


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
                WriteDecoratedTypeName(mi.ReturnType);
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
            var TypeScriptAttribute = m.DeclaringType.ToScriptAttribute();
            var MethodScriptAttribute = m.ToScriptAttribute();
            var IsDefineAsStatic = MethodScriptAttribute != null && MethodScriptAttribute.DefineAsStatic;

            Action WriteMethodName =
                delegate
                {
                    if (TypeScriptAttribute.IsNative)
                        Write(m.Name);
                    else
                        WriteDecoratedMethodName(m, false);
                };

            var IsBaseConstructorCall = i.IsBaseConstructorCall(m);

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




            if (m.IsStatic || IsDefineAsStatic)
            {
                WriteDecoratedTypeName(m.DeclaringType);
                Write(".");

                #region prop
                if (!IsDefineAsStatic)
                {
                    var prop = new PropertyDetector(m);

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
                        var prop = new PropertyDetector(m);

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

            WriteParameterInfoFromStack(m, p, s, offset);
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
            throw new NotImplementedException();
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
            };

        public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            if (z.IsArray)
                return "Array";

            // convert c# type to actionscript typename literal

            if (NativeTypes.ContainsKey(z))
                return NativeTypes[z];

            return z.Name;
        }

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            // used when writing the source file

            return GetDecoratedTypeName(z, false);
        }


        public override void WriteDecoratedMethodName(System.Reflection.MethodBase z, bool q)
        {
            if (q)
                throw new NotImplementedException();

            if (z.Name == "ToString" && z.GetParameters().Length == 0)
            {
                Write("toString");
                return;
            }

            // todo: should use base62 encoding here
            Write(z.Name + "_" + z.MetadataToken);
        }

        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();

            Write("var ");
            WriteVariableName(u.DeclaringType, u, v);
            Write(":");
            //WriteDecoratedTypeName(v.LocalType);
            WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType/*, true, true*/);
            WriteLine(";");
        }

        private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv/*, bool favorPrimitives, bool favorTargetType*/)
        {
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
                /*
                if (favorTargetType)
                {
                    if (ScriptAttribute.OfProvider(iType).ImplementationType != null)
                        iType = null;
                }*/
            }

            if (iType == null)
                Write(GetDecoratedTypeName(timpv, true/*, favorPrimitives, true*/));
            else
                Write(GetDecoratedTypeName(iType, true));
        }
    }
}
