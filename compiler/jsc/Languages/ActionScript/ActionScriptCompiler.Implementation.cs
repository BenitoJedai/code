using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

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

        public override bool SupportsInlineExceptionVariable
        {
            get { return true; }
        }

        public override bool SupportsAbstractMethods
        {
            get { return false; }
        }
        public override bool SupportsCustomArrayEnumerator
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

                if (ScriptAttribute.IsAnonymousType(z))
                    za = new ScriptAttribute();

                #region type summary
                var u = GetXMLNode(z);

                if (u != null)
                    WriteBlockComment(u["summary"].InnerText);
                #endregion

                if (za.IsDebugCode)
                {
                    WriteIdent();
                    WriteCommentLine("[Script(IsDebugCode = true)]");
                }
                WriteCustomAttributes(z);
                WriteTypeSignature(z, za);

                var cctor = default(Action);

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

                        if (ScriptAttribute.IsAnonymousType(z))
                        {
                            WriteTypeInstanceConstructors(z);
                            WriteLine();

                            foreach (var p in z.GetProperties())
                            {
                                var GetMethod = p.GetGetMethod();

                                WriteMethodSignature(GetMethod, false);
                                WriteMethodBody(GetMethod);

                            }


                            var ToString = z.GetMethod("ToString");

                            WriteMethodSignature(ToString, false);
                            WriteMethodBody(ToString);
                        }
                        else
                        {
                            if (za.ImplementationType == null)
                            {
                                // there is another type that needs to be created

                                WriteTypeInstanceConstructors(z);
                                WriteLine();

                                WriteTypeInstanceMethods(z, za);
                                WriteLine();
                            }
                            else
                            {
                                WriteIdent();
                                WriteCommentLine("this class is just extending another class via static members");
                            }
                        }

                        WriteTypeStaticMethods(z, za);
                        WriteLine();

                        cctor = WriteTypeStaticConstructor(z, za);
                        WriteLine();

                        if (!z.IsInterface)
                        {
                            WriteInterfaceMappingMethods(z);
                        }

                        WriteVirtualMethodOverrides(z);
                    }

                }

                if (cctor != null)
                    cctor();
            }

            return true;
        }

        

        private Action WriteTypeStaticConstructor(Type z, ScriptAttribute za)
        {
            var cctor = z.GetStaticConstructor();

            if (cctor == null)
                return null;

            WriteXmlDoc(cctor);
            WriteMethodSignature(z, cctor, false);
            WriteMethodBody(cctor, MethodBodyFilter);
            WriteLine();

            return delegate
            {
                WriteIdent();
                WriteDecoratedTypeName(z);
                Write(".");
                WriteDecoratedMethodName(cctor, false);
                Write("();");
                WriteLine();
            };
        }

        public MethodBase ResolveMethod(MethodBase m)
        {
            return
                (m.DeclaringType.ToScriptAttribute() == null
                            ? ResolveImplementationMethod(m.DeclaringType, m)
                            : m);
        }

        public MethodBase ResolveMethod(Type t, MethodBase m)
        {
            var s = m.DeclaringType.ToScriptAttributeOrDefault();

            return
                (s == null
                            ? ResolveImplementationMethod(t, m)
                            : m);
        }

        private void WriteInterfaceMappingMethods(Type z)
        {
            DebugBreak(z.ToScriptAttribute());

            // current interface exclusion implementation might not work well with abstract classes 

            Action<MethodInfo, MethodInfo, MethodInfo> WriteInterfaceMapping =
                (_InterfaceMethod, _ParamSignature, _TargetMethod) =>
                {
                    DebugBreak(_TargetMethod.ToScriptAttributeOrDefault());

                    var BaseMethod = z.BaseType.GetMethod(_InterfaceMethod);

                    WriteMethodSignature(_InterfaceMethod, false,
                        (BaseMethod != null && BaseMethod.IsAbstract) ?
                        WriteMethodSignatureMode.OverridingImplementing :
                        WriteMethodSignatureMode.Implementing
                        , null, null, _ParamSignature);

                    using (CreateScope())
                    {
                        WriteIdent();

                        if (_InterfaceMethod.ReturnType != typeof(void))
                        {
                            WriteKeywordReturn();
                            WriteSpace();
                        }

                        WriteThisReference();
                        Write(".");

                        #region prop
                        {
                            var prop = new PropertyDetector(_TargetMethod);

                            if (_InterfaceMethod.GetParameters().Length == 1 && prop.SetProperty != null
                                 && prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1)
                            {
                                Write(prop.SetProperty.Name);
                                WriteAssignment();
                                WriteSafeLiteral(_InterfaceMethod.GetParameters().Single().Name);
                            }
                            else if (prop.GetProperty != null
                                  && prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0)
                            {
                                Write(prop.GetProperty.Name);
                            }
                            else
                            {
                                WriteDecoratedMethodName(_TargetMethod, false);
                                Write("(");
                                for (int i = 0; i < _InterfaceMethod.GetParameters().Length; i++)
                                {
                                    if (i > 0)
                                    {
                                        Write(",");
                                        WriteSpace();
                                    }
                                    WriteSafeLiteral(_InterfaceMethod.GetParameters()[i].Name);
                                }
                                Write(")");
                            }
                        }
                        #endregion

                        Write(";");
                        WriteLine();
                    }
                };



            WriteIdent();
            WriteCommentLine(DateTime.Now.ToString());

            foreach (var i in z.GetInterfaces())
            {
                WriteIdent();
                WriteCommentLine("interface " + i.Namespace + "::" + i.Name);

                var mapping = z.GetInterfaceMap(i);

                WriteIdent();
                WriteCommentLine(" mappings:");

                Action WriteInterfaceMappingDelayed = delegate { };

                for (int j = 0; j < mapping.InterfaceMethods.Length; j++)
                {
                    var InterfaceMethod = mapping.InterfaceMethods[j];
                    var InterfaceMethodDeclaringType =
                        mapping.InterfaceType.IsGenericType ?
                        mapping.InterfaceType.GetGenericTypeDefinition() :
                        mapping.InterfaceType
                        ;

                    var InterfaceMethodImplementation = (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, InterfaceMethod,
                        //AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
                        AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
                        ) ?? InterfaceMethod;

                    var InterfaceMethodImplementationSignature = (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, InterfaceMethod,
                        AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
                        //AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
                        ) ?? InterfaceMethod;

                    var TargetMethod = mapping.TargetMethods[j];



                    if (TargetMethod.DeclaringType == z)
                    {
                        WriteIdent();

                        if (InterfaceMethodImplementation == null)
                        {
                            BreakToDebugger("Interface Mapping Error: " +
                                InterfaceMethod.DeclaringType.Name + "." + InterfaceMethod.Name +
                                " -> " +
                                TargetMethod.DeclaringType.Name + "." + TargetMethod.Name);
                        }
                        else
                        {
                            WriteCommentLine(" " +
                                InterfaceMethod.DeclaringType.Name + "." + InterfaceMethod.Name + " = " +
                                InterfaceMethodImplementation.DeclaringType.Name + "." + InterfaceMethodImplementation.Name +
                                " -> this." + TargetMethod.Name);

                            WriteInterfaceMappingDelayed +=
                                delegate
                                {
                                    WriteIdent();

                                    WriteCommentLine(" " +
                                        InterfaceMethodImplementation.DeclaringType.Name + "." + InterfaceMethodImplementation.Name + "_" + InterfaceMethodImplementation.MetadataToken
                                        );

                                    WriteInterfaceMapping(InterfaceMethodImplementation, InterfaceMethodImplementationSignature, TargetMethod);
                                };
                        }
                    }
                }

                WriteInterfaceMappingDelayed();

                WriteLine();
            }

            WriteLine();
            //}




        }

        public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            DebugBreak(za);


            WriteIdent();

            if (ScriptAttribute.IsAnonymousType(z) || (za != null && za.Implements != null))
            {
                // private for developers but public for the runtime

                Write("public ");
            }
            else
            {
                // as3 cannot have assembly private types

                Write("public ");

            }

            if (z.IsSealed)
                Write("final ");

            if (z.IsInterface)
                Write("interface ");
            else
                Write("class ");

            WriteDecoratedTypeName(z);

            if (z.IsClass)
            {
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

                    if (ba.ImplementationType != null)
                    {
                        WriteDecoratedTypeNameOrImplementationTypeName(ba.ImplementationType, false, false, IsFullyQualifiedNamesRequired(z, ba.ImplementationType));
                    }
                    else// if (ba.Implements == null)
                        WriteDecoratedTypeNameOrImplementationTypeName(BaseTypeImplementation, false, false, IsFullyQualifiedNamesRequired(z, BaseTypeImplementation));
                    //else
                    //    Write(GetDecoratedTypeName(BaseTypeImplementation, false));

                }
                #endregion
            }

            #region implements
            var timp = z.GetInterfaces();

            if (timp.Length > 0)
            {
                int i = 0;

                DebugBreak(za);

                foreach (var v in timp)
                {
                    var timpv = v;

                    // ignore interfaces which are not visible to scripting
                    if (timpv.ToScriptAttribute() == null)
                    {
                        timpv = MySession.ResolveImplementation(timpv);

                        if (timpv == null)
                            continue;
                    }

                    if (i++ > 0)
                        Write(", ");
                    else
                    {
                        // http://livedocs.adobe.com/specs/actionscript/3/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=as3_specification189.html
                        // http://livedocs.adobe.com/specs/actionscript/3/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=as3_specification100.html#wp127562

                        if (z.IsInterface)
                            Write(" extends ");
                        else
                            Write(" implements ");
                    }

                    WriteDecoratedTypeNameOrImplementationTypeName(timpv, false, true, IsFullyQualifiedNamesRequired(z, timpv));
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
                if ((za.HasNoPrototype || za.ImplementationType != null) && !zfn.IsStatic)
                    continue;

                if (zfn.IsLiteral)
                    continue;


                // write the attributes for current field
                WriteCustomAttributes(zfn);

                WriteIdent();
                WriteTypeFieldModifier(zfn);

                Write("var ");
                WriteSafeLiteral(zfn.Name);
                Write(":");
                //WriteGenericOrDecoratedTypeName(zfn.FieldType);
                WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true, IsFullyQualifiedNamesRequired(z, zfn.FieldType));

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

        private void WriteCustomAttributes(ICustomAttributeProvider zfn)
        {
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
                        if (f.FieldType == typeof(string))
                        {
                            var value = (string)f.GetValue(v.i);

                            if (value == null)
                                return seed;

                            Write(seed);

                            Write(f.Name);
                            WriteAssignment();

                            WriteQuotedLiteral(value);


                        }
                        else if (f.FieldType == typeof(uint))
                        {
                            Write(seed);

                            Write(f.Name);
                            WriteAssignment();

                            var value = (uint)f.GetValue(v.i);

                            var HexA = f.GetCustomAttributes(typeof(HexAttribute), false).Cast<HexAttribute>().SingleOrDefault();

                            if (HexA != null)
                                Write(string.Format("0x{0:x8}", value));
                            else
                                Write((value).ToString());
                        }
                        else if (f.FieldType == typeof(int))
                        {
                            Write(seed);

                            Write(f.Name);
                            WriteAssignment();

                            var value = (int)f.GetValue(v.i);

                            var HexA = f.GetCustomAttributes(typeof(HexAttribute), false).Cast<HexAttribute>().SingleOrDefault();

                            if (HexA != null)
                                Write(string.Format("0x{0:x8}", value));
                            else
                                Write((value).ToString());
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
        }




        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            // If the field is not public and also not used then we can ditch the public keywword
            // until then we always need public because the field is used by other generated types

            Write("public ");

            /*
            if (zfn.IsPublic)
                Write("public ");
            else
            {
                if (zfn.IsFamily)
                    Write("protected ");
                else
                    Write("private ");
            }*/
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




        class PropertyDetector
        {
            public PropertyInfo SetProperty;
            public PropertyInfo GetProperty;

            public PropertyDetector(MethodBase m)
            {
                if (m.IsConstructor)
                    return;

                var any = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

                #region set
                {
                    var prefix = "set_";
                    if (m.Name.StartsWith(prefix))
                    {
                        SetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
                    }
                }
                #endregion

                #region get
                {
                    var prefix = "get_";
                    if (m.Name.StartsWith(prefix))
                    {
                        GetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
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

            if (ScriptAttribute.IsAnonymousType(TargetMethod.DeclaringType))
            {
                // nop
            }
            else
            {
                if (MethodScriptAttribute != null && MethodScriptAttribute.NotImplementedHere)
                {
                    // a native type has a method that is defined by scriptcorelib
                    // the implementation must be in some other type that is not native
                    // we need to find that type

                    TargetMethod = MySession.ResolveImplementation(m.DeclaringType, TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);
                    MethodScriptAttribute = TargetMethod.ToScriptAttribute();
                }
                else
                {
                    TargetMethod = ResolveMethod(m);
                    MethodScriptAttribute = TargetMethod.ToScriptAttribute();
                }
            }

            var TypeScriptAttribute = TargetMethod.DeclaringType.ToScriptAttribute();





            var IsDefineAsStatic = MethodScriptAttribute != null && MethodScriptAttribute.DefineAsStatic;
            var HasMethodExternalTarget = MethodScriptAttribute != null && MethodScriptAttribute.ExternalTarget != null;

            Action WriteMethodName =
                delegate
                {
                    if (TypeScriptAttribute != null && TypeScriptAttribute.IsNative)
                        Write(TargetMethod.Name);
                    else
                        if (HasMethodExternalTarget)
                            Write(MethodScriptAttribute.ExternalTarget);
                        else
                            WriteDecoratedMethodName(TargetMethod, false);

                };

            var IsBaseConstructorCall = i.IsBaseConstructorCall(TargetMethod, ResolveMethod);

            var s = i.StackBeforeStrict;
            var offset = 1;

            if (TargetMethod.IsStatic || IsDefineAsStatic)
            {
                if (IsDefineAsStatic)
                    offset = 1;
                else
                    offset = 0;
            }

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
                WriteDecoratedTypeName(i.OwnerMethod.DeclaringType, TargetMethod.DeclaringType, WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
                Write(".");

                #region prop
                if (!IsDefineAsStatic)
                {


                    var prop = new PropertyDetector(TargetMethod);


                    if (prop.SetProperty != null && prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1)
                    {

                        WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.SetProperty.Name);
                        WritePropertyAssignment(prop);

                        return;
                    }

                    if (prop.GetProperty != null && prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0)
                    {
                        WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.GetProperty.Name);
                        return;
                    }
                }
                #endregion

                WriteMethodName();


            }
            else
            {
                if (IsBaseConstructorCall)
                {
                    DebugBreak(p.DeclaringMethod.ToScriptAttribute());

                    Write("super");
                }
                else
                {
                    if (!i.OwnerMethod.IsStatic && i.OwnerMethod.DeclaringType.IsSubclassOf(TargetMethod.DeclaringType) && s[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
                    {
                        Write("super");
                    }
                    else
                    {
                        Emit(p, s[0]);
                    }

                    Write(".");


                    #region prop
                    if (!TargetMethod.DeclaringType.IsInterface)
                    {
                        var prop = new PropertyDetector(TargetMethod);

                        if (prop.SetProperty != null &&
                            (HasMethodExternalTarget ||
                                (prop.SetProperty.GetSetMethod(true) != null &&
                                 prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1
                                )
                            ))
                        {
                            WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.SetProperty.Name);
                            WritePropertyAssignment(prop);
                            return;
                        }

                        if (prop.GetProperty != null &&
                             (HasMethodExternalTarget ||
                                (prop.GetProperty.GetGetMethod(true) != null &&
                                 prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0
                                )
                            ))
                        {
                            WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.GetProperty.Name);
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

        public override void WriteInstanceOfOperator(ILInstruction value, Type type)
        {
            EmitInstruction(null, value);

            // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/compilerWarnings.html
            //  	3556	The instanceof operator is deprecated, use the is operator instead.
            WriteSpace();
            Write("is");
            WriteSpace();

            WriteDecoratedTypeName(type);
        }

        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            if (p.Block.IsTryBlock)
            {

                WriteIdent();
                WriteLine("try");


                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = false;
                bool _leave = b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

                EmitScope(b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last));


            }
            else if (p.Block.IsHandlerBlock)
            {


                WriteIdent();



                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
                bool _leave =
                    b.Last == OpCodes.Endfinally
                ||
                    (b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

                b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                b.RemoveNopOpcodes();

                if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                {
                    Write("catch (");

                    if (p.Block.Clause.CatchType == typeof(object))
                    {
                        WriteExceptionVar();
                        Write(":*");
                    }
                    else
                    {
                        ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];
                        WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);
                        Write(":");
                        WriteDecoratedTypeNameOrImplementationTypeName(p.Block.Clause.CatchType, true, true, IsFullyQualifiedNamesRequired(p.Block.OwnerMethod.DeclaringType, p.Block.Clause.CatchType));


                        // remove the set command
                        b.PrestatementCommands.RemoveAt(0);

                    }


                    WriteLine(")");

                    EmitScope(b);
                }
                else
                {
                    WriteLine("finally");
                    EmitScope(b);
                }

                // additional space
                WriteLine();
            }
            else
            {
                return false;
            }

            return true;
        }

        Dictionary<Type, string> NativeTypes =
            new Dictionary<Type, string>
            {
                {typeof(byte), "int"},
                {typeof(sbyte), "int"},
                {typeof(short), "int"},

                {typeof(int), "int"},
                {typeof(char), "int"}, // char = int
                {typeof(uint), "uint"},
                {typeof(ushort), "uint"},

                {typeof(bool), "Boolean"},

                {typeof(long), "Number"},
                {typeof(ulong), "Number"},

                {typeof(double), "Number"},
                {typeof(decimal), "Number"},
                {typeof(float), "Number"},

                {typeof(void), "void"},
                {typeof(string), "String"},
                {typeof(object), "Object"},
            };

        public override string GetDecoratedTypeName(Type x, bool bExternalAllowed)
        {
            if (x.IsEnum)
                return "int";

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

                var s = z.DeclaringType.ToScriptAttribute();
                if (s != null && s.IsNative)
                    WriteSafeLiteral(z.Name);
                else
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

            WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true, IsFullyQualifiedNamesRequired(u.DeclaringType, v.LocalType));
            WriteLine(";");
        }



        public bool IsFullyQualifiedNamesRequired(Type context, Type subject)
        {
            if (context != subject && context.Name == subject.Name)
                return true;

            // there is a field with the same name as the type we would be importing
            if (context.GetField(subject.Name) != null)
                return true;

            return GetImportTypes(context).Count(i => i.Name == subject.Name) > 1;
        }

        public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(timpv, favorPrimitives, favorTargetType, UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode.Default);
        }

        public enum WriteDecoratedTypeNameOrImplementationTypeNameMode
        {
            Default,
            IgnoreImplementationType
        }

        public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
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


            var iType = MySession.ResolveImplementation(timpv);

            if (iType != null)
            {
                if (favorTargetType)
                {
                    var s = iType.ToScriptAttribute();

                    if (s.ImplementationType != null)
                        iType = s.ImplementationType;
                }
            }



            Action<Type> WriteTypeName =
                t =>
                {
                    var ns = NamespaceFixup(t.Namespace);

                    if (UseFullyQualifiedName && !string.IsNullOrEmpty(ns))
                    {
                        Write(ns);
                        Write(".");
                    }

                    WriteSafeLiteral(GetDecoratedTypeName(t, true));
                };

            if (iType == null)
            {
                var s = timpv.ToScriptAttribute();

                if (!(Mode == WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType) && s != null && s.ImplementationType != null)
                    WriteTypeName(s.ImplementationType);
                else
                    WriteTypeName(timpv);

            }
            else
            {
                WriteTypeName(iType);
            }
        }
    }
}
