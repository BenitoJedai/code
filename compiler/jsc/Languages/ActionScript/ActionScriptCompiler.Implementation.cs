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

            return true;
        }

        public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            WriteIdent();


            if (z.IsNotPublic)
                Write("private ");

            if (z.IsPublic)
                Write("public ");

            if (z.IsSealed)
                Write("final ");


            Write("class ");
            Write(z.Name);

            #region extends
            if (z.BaseType != typeof(object) && z.BaseType != null)
            {
                Write(" extends ");

                ScriptAttribute ba = ScriptAttribute.Of(z.BaseType, true);

                if (ba == null)
                    throw new NotSupportedException("extending object has no attribute");


                if (ba.Implements == null)
                    WriteDecoratedTypeName(z.BaseType);
                else
                    Write(GetDecoratedTypeName(z.BaseType, false));

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

                WriteIdent();
                WriteTypeFieldModifier(zfn);

                Write("var ");
                WriteSafeLiteral(zfn.Name);
                Write(":");
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
            WriteIdent();

            if (m.IsPublic)
                Write("public ");

            if (m.IsPrivate)
                Write("private ");

            if (m.IsStatic)
                Write("static ");


            Write("function ");

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
            {
                var prop = new PropertyDetector(m);

                if (prop.SetProperty != null)
                {
                    Write("set ");
                    Write(prop.SetProperty.Name);
                }
                else if (prop.GetProperty != null)
                {
                    Write("get ");
                    Write(prop.GetProperty.Name);
                }
                else
                {
                    Write(m.Name);
                }
            }

            Write("(");
            WriteMethodParameterList(m);
            Write(")");

            var mi = m as MethodInfo;

            if (mi != null)
            {
                Write(":");
                WriteDecoratedTypeName(mi.ReturnType);
            }

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

            var IsBaseConstructorCall = i.IsBaseConstructorCall(m);

            var s = i.StackBeforeStrict;
            var offset = 1;

            if (m.IsStatic)
            {
                WriteDecoratedTypeName(m.DeclaringType);
                Write(".");
                WriteDecoratedMethodName(m, false);

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

                    var prop = new PropertyDetector(m);

                    #region set
                    if (prop.SetProperty != null)
                    {

                        Write(prop.SetProperty.Name);
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
                        return;
                    }
                    #endregion

                    #region get
                    if (prop.GetProperty != null)
                    {

                        Write(prop.GetProperty.Name);

                        return;
                    }
                    #endregion

                    WriteDecoratedMethodName(m, false);
                }
            }

            WriteParameterInfoFromStack(m, p, s, offset);

        }


        public override void WriteSelf()
        {
            Write("this");
        }



        public override Type[] GetActiveTypes()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m, string alias)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeConstructionVerified()
        {
            throw new NotImplementedException();
        }

        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            throw new NotImplementedException();
        }

        public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            if (z.IsArray)
                return "Array";

            // convert c# type to actionscript typename literal
            var dict = new Dictionary<Type, string>
            {
                {typeof(int), "int"},
                {typeof(uint), "uint"},
                {typeof(double), "Number"},
                {typeof(void), "void"},
            };

            if (dict.ContainsKey(z))
                return dict[z];

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

            Write(z.Name);
        }

        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();

            Write("var ");
            WriteVariableName(u.DeclaringType, u, v);
            Write(":");
            WriteDecoratedTypeName(v.LocalType);
            //WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
            WriteLine(";");
        }
    }
}
