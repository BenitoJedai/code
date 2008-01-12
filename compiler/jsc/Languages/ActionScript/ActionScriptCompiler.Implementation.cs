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
                    WriteTypeInstanceConstructors(z);
                    WriteLine();
                }
            }

            return true;
        }

        public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            WriteIdent();
            Write("public class ");
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

        public override void WriteTypeFields(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeFieldModifier(System.Reflection.FieldInfo zfn)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeInstanceMethods(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic)
        {
            WriteIdent();
            Write("public function ");

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
                throw new NotImplementedException();

            Write("(");
            Write(")");

            WriteLine();
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

                    #region set
                    {
                        var prefix = "set_";
                        if (m.Name.StartsWith(prefix))
                        {
                            var property = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length));

                            if (property != null)
                            {
                                Write(property.Name);
                                WriteAssignment();

                                #region bool
                                if (property.PropertyType == typeof(bool))
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
                        }
                    }
                    #endregion

                    #region get
                    {
                        var prefix = "get_";
                        if (m.Name.StartsWith(prefix))
                        {
                            var property = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length));

                            if (property != null)
                            {
                                Write(property.Name);

                                return;
                            }
                        }
                    }
                    #endregion

                    WriteDecoratedMethodName(m, false);
                }
            }

            WriteParameterInfoFromStack(m, p, s, offset);

        }

        public override void WriteMethodParameterList(System.Reflection.MethodBase m)
        {
            throw new NotImplementedException();
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
