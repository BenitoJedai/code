
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{
    // http://www.javacamp.org/javavscsharp/constructor.html
    // http://www4.ncsu.edu/~kaltofen/courses/Languages/JavaExamples/cpp_vs_java/
    public partial class JavaCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "java";

        public override ScriptType GetScriptType()
        {
            return ScriptType.Java;
        }

        public override Type[] GetActiveTypes()
        {
            return MySession.Types;
        }

        public override bool CompileToSingleFile
        {
            get
            {
                return false;
            }
        }

        public override bool IsUTF8SupportedInLiterals()
        {
            return true;
        }

        public readonly AssamblyTypeInfo MySession;

        public JavaCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {

            MySession = xs;

            CreateInstructionHandlers();

        }







        private void WriteTypeStaticAccessor()
        {
            Write(".");
        }



        public Type ResolveImplementation(Type t)
        {
            return MySession.ResolveImplementation(t); ;
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
        {
            return MySession.ResolveImplementation(t, m);
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias);
        }







        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            if (IsEmptyImplementationType(z))
                return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();

            if (z.Namespace != null)
            {
                WriteIdent();
                Write("package " + NamespaceFixup(z.Namespace) + ";");
                WriteLine();
                WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);



            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion


            WriteTypeSignature(z, za);

            using (CreateScope())
            {
                WriteTypeFields(z, za);
                WriteLine();
                WriteTypeStaticConstructor(z, za);
                WriteLine();

                if (za.Implements == null)
                {
                    WriteTypeInstanceConstructors(z);
                    WriteLine();
                }

                WriteTypeInstanceMethods(z, za);
                WriteLine();
                WriteTypeStaticMethods(z, za);


            }

            //Thread.Sleep(100);

            return true;
        }



        private void WriteTypeStaticConstructor(Type z, ScriptAttribute za)
        {
            ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

            if (ci.Length > 0)
            {
                if (ci.Length > 1)
                    Break("more than  static ctor?");

                this.WriteIdent();
                this.WriteKeywordStatic();
                this.WriteLine();


                foreach (ConstructorInfo m in ci)
                {
                    ILBlock cctor = new ILBlock(m);

                    WriteMethodBody(m,
                        delegate(ILBlock.Prestatement p)
                        {
                            // final fields must be final

                            if (p.Instruction != null)
                            {
                                FieldInfo f = p.Instruction.TargetField;

                                if (f != null && f.IsStatic && f.IsInitOnly)
                                {
                                    ILBlock.Prestatement xp = cctor.GetStaticFieldFinalAssignment(p.Instruction.TargetField);

                                    if (xp != null && xp.Instruction.Index == p.Instruction.Index)
                                        return true;
                                }
                            }

                            return p.Instruction == OpCodes.Ret;
                        }
                    );
                }
            }
            else
            {

                if (ScriptLibraryImport(z) != null)
                {
                    this.WriteIdent();
                    this.WriteKeywordStatic();
                    this.WriteLine();

                    using (this.CreateScope())
                    {
                        WriteIdent();

                        WriteLine("java.lang.System.loadLibrary(\"" + ScriptLibraryImport(z) + "\");");
                    }
                }
            }
        }









        public override void WriteXmlDoc(MethodBase m)
        {
            if (this.XmlDoc != null)
            {
                DebugBreak(m);

                XmlNode n = GetXMLNodeForMethod(m);

                if (n != null)
                {
                    string Summary = n["summary"].InnerText.Trim();

                    WriteBlockComment(Summary);
                }
                else
                {
                    //WriteJavaDoc(MethodSig);
                }
            }
        }





        public override void WriteMethodSignature(MethodBase m, bool dStatic)
        {
            DebugBreak(ScriptAttribute.Of(m));

            WriteIdent();

            if (m.IsAbstract)
                Write("abstract ");

            int flags = (int)m.GetMethodImplementationFlags();

            // http://blogs.msdn.com/ricom/archive/2004/05/05/126542.aspx
            // http://msdn2.microsoft.com/en-us/library/system.reflection.methodimplattributes.aspx
            if ((flags & (int)MethodImplAttributes.Synchronized) == (int)MethodImplAttributes.Synchronized)
                Write("synchronized ");


            if (m.IsPublic)
                WriteKeywordPublic();
            else
            {
                if (m.IsFamily)
                    Write("protected ");
                else
                    WriteKeywordPrivate();
            }

            if (m.IsStatic || dStatic)
                WriteKeywordStatic();
            else
            {
                if (m is MethodInfo)
                {
                    if (m.IsFinal || !m.IsVirtual)
                        Write("final ");
                }
            }

            if (ScriptIsPInvoke(m))
            {
                Write("native ");
            }

            if (m is MethodInfo)
            {
                MethodInfo mi = m as MethodInfo;

                //WriteDecoratedTypeName(mi.ReturnType);
                WriteDecoratedTypeNameOrImplementationTypeName(mi.ReturnType, true, true);

                //Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));
                WriteSpace();
            }

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
                WriteDecoratedMethodName(m, false);

            Write("(");
            WriteMethodParameterList(m);

            Write(")");

            WriteMethodSignatureThrows(m);

            if (m.IsAbstract || ScriptIsPInvoke(m))
                WriteLine(";");
            else
                WriteLine();

        }

        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();

            WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
            WriteSpace();

            //WriteVariableType(v.LocalType, true);

            WriteVariableName(u.DeclaringType, u, v);

            WriteLine(";");
        }



        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(i.OwnerMethod));

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool IsExternalDefined = ma != null && ma.ExternalTarget != null;
            bool IsDefineAsInstance = ma != null && ma.DefineAsInstance;
            bool IsBaseCall = false;
            bool IsDefineAsStatic = ma != null && ma.DefineAsStatic;

            if (m.IsConstructor)
            {
                // fixme: update the BCL resolving issue
                // the super ctor call gets lost otherwise

                if (i.IsBaseConstructorCall(m))
                {

                    IsBaseCall = true;
                }
                else
                    Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
            }



            ILFlow.StackItem[] s = i.StackBeforeStrict;

            int offset = 1;

            #region static call defined as instance call


            if (m.IsStatic && IsExternalDefined & IsDefineAsInstance)
            {
                // what?? string?

                Emit(p, s[0]);
                Write(".");
                WriteExternalMethod(ma.ExternalTarget, m);
                WriteParameterInfoFromStack(m, p, s, 1);

                return;
            }
            #endregion



            if ((m.IsStatic || IsDefineAsStatic) || IsBaseCall)
            {
                #region static
                //TODO: ???
                if (IsBaseCall)
                {
                    //WriteTypeBaseType();
                    //Write(".");

                }
                else
                {
                    //ScriptAttribute ta = ScriptAttribute.Of(m.DeclaringType);

                    if (IsExternalDefined)
                    {
                        //WriteBoxedComment("impl");



                        //WriteTypeOrExternalTargetTypeName(ta.Implements);

                        WriteDecoratedTypeName(ScriptAttribute.Of(m.DeclaringType).ImplementationType);
                        Write(".");

                    }
                    else
                    {
                        //WriteBoxedComment("ext");

                        WriteTypeOrExternalTargetTypeName(m.DeclaringType);
                        Write(".");
                    }
                }
                #endregion

                offset = !m.IsStatic && (IsDefineAsStatic || IsBaseCall) ? 1 : 0;
            }
            else
            {

                // WriteBoxedComment("variable.call");

                // base. ?

                if (i.OpCode == OpCodes.Call &&
                    s[0].SingleStackInstruction == OpCodes.Ldarg_0 &&
                    i.OwnerMethod.DeclaringType.BaseType == m.DeclaringType)
                {
                    Write("super");
                }
                else
                {
                    Emit(p, s[0]);
                }

                Write(".");
            }



            if (IsExternalDefined)
            {
                WriteExternalMethod(ma.ExternalTarget, m);
            }
            else
            {
                if (IsBaseCall)
                {
                    Write("super");
                }
                else
                {
                    WriteDecoratedMethodName(m, false);
                }
            }

            WriteParameterInfoFromStack(m, p, s, offset);

        }

        public void WriteExternalMethod(string p, MethodBase m)
        {
            if (p.Contains("*"))
            {
                foreach (PropertyInfo v in m.DeclaringType.GetProperties())
                {
                    if (v.GetGetMethod() == m || v.GetSetMethod() == m)
                    {
                        Write(p.Replace("*", v.Name));

                        return;
                    }
                }

                throw new NotSupportedException("The use of * is only allowed on properties to capture its name.");

            }
            else
            {
                Write(p);
            }
        }

        public override void MethodCallParameterTypeCast(Type context, Type p)
        {
            Write("(");
            WriteDecoratedTypeName(p);
            Write(")");
        }

        private void WriteTypeOrExternalTargetTypeName(Type m)
        {


            string x = ScriptGetExternalTarget(m);

            if (x == null)
                Write(GetDecoratedTypeName(m, false));
            else
                Write(x);

        }


        public void WriteTypeNameAsMemberName(Type e)
        {
            if (e.IsArray)
            {
                Write("ArrayOf");

                WriteTypeNameAsMemberName(e.GetElementType());

                return;
            }

            WriteDecoratedTypeName(e);
        }

        public override void WriteDecoratedMethodName(MethodBase z, bool q)
        {
            if (q)
                Write("\"");

            if (z.Name == "ToString" && !z.IsStatic)
                Write("toString");
            else
            {
                if (z.Name == "op_Implicit")
                {


                    Type rt = ((MethodInfo)z).ReturnType;

                    if (rt == z.DeclaringType)
                    {
                        // name clash?

                        Write("Of");

                        //Write("From");
                        //WriteTypeNameAsMemberName(z.GetParameters()[0].ParameterType);
                    }
                    else
                    {
                        Write("To");

                        if (rt.IsPrimitive)
                            Write("_");

                        WriteTypeNameAsMemberName(rt);

                    }

                }
                else
                {
                    Write(z.Name);
                }

            }

            if (q)
                Write("\"");
        }

        private void WriteMethodSignatureThrows(MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(m));

            List<Type> list = GetMethodExceptions(m);

            if (list.Count > 0)
            {
                WriteSpace();
                WriteKeywordThrows();

                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                        Write(", ");

                    WriteVariableType(list[i], false);

                }
            }
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
                    DebugBreak(p.DeclaringMethod.ToScriptAttribute());

                    Write("catch (");

                    if (p.Block.Clause.CatchType == typeof(object))
                    {
                        Write("java.lang.Throwable");
                        WriteSpace();
                        WriteExceptionVar();
                    }
                    else
                    {
                        var ExceptionType = MySession.ResolveImplementation(p.Block.Clause.CatchType) ?? p.Block.Clause.CatchType;
                        var ExceptionTypeAttribute = ExceptionType.ToScriptAttribute();

                        if (ExceptionTypeAttribute != null && ExceptionTypeAttribute.ImplementationType != null)
                            Write(GetDecoratedTypeName(ExceptionTypeAttribute.ImplementationType, true));
                        else
                            Write(GetDecoratedTypeName(ExceptionType, true));

                        WriteSpace();

                        ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];

                        if (set_exc.Instruction.TargetVariable == null)
                            Write("__ExceptionValue");
                        else
                            WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);

                        // remove the set command if there is one
                        if (set_exc.Instruction.TargetVariable != null)
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






        public override void WriteSelf()
        {
            Write("that");
        }



        public override void WriteMethodParameterList(MethodBase m)
        {
            ParameterInfo[] mp = m.GetParameters();

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);


                ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                if (sa.Implements == null)
                {
                    WriteDecoratedTypeName(m.DeclaringType);

                }
                else
                {
                    WriteDecoratedTypeName(sa.Implements);
                }

                // this parameter is on the argument list

                WriteSpace();
                WriteSelf();
            }

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                ParameterInfo p = mp[mpi];

                ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteVariableType(p.ParameterType, true);
                else
                    WriteVariableType(za.Implements, true);

                Write(p.Name);
            }
        }

        public void WriteVariableType(Type t, bool bSpace)
        {

            Write(GetDecoratedTypeName(t, true, true, true));

            if (bSpace)
                WriteSpace();
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

                WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);
                WriteSpace();

                //WriteVariableType(zfn.FieldType, true);
                Write(zfn.Name);

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

                WriteLine(";");
            }
        }

        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            if (zfn.IsPublic)
                WriteKeywordPublic();
            else
            {
                if (zfn.IsFamily)
                    Write("protected ");
                else
                    WriteKeywordPrivate();
            }

            if (zfn.IsInitOnly)
                WriteKeywordFinal();

            if (zfn.IsStatic)
                WriteKeywordStatic();

            if (zfn.IsNotSerialized)
                Write("transient ");
        }

        #region keywords
        private void WriteKeywordStatic()
        {
            Write("static ");
        }

        private void WriteKeywordImport()
        {
            Write("import ");
        }


        private void WriteKeywordFinal()
        {
            Write("final ");
        }

        private void WriteKeywordPrivate()
        {
            Write("private ");
        }

        private void WriteKeywordPublic()
        {
            Write("public ");
        }

        private void WriteKeywordClass()
        {
            Write("class ");
        }

        private void WriteKeywordThrows()
        {
            Write("throws ");
        }

        #endregion

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            return GetDecoratedTypeName(z, false, false, false);
        }

        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            WriteIdent();



            if (za.Implements != null || z.IsPublic || z.IsNestedPublic)
                WriteKeywordPublic();
            //else
            //    WriteKeywordPrivate();


            if (z.IsAbstract && !z.IsSealed)
                Write("abstract ");

            if (z.IsSealed)
                Write("final ");
            else
            {
                // Shall we seal all nonused objects?
            }

            if (z.IsInterface)
                WriteKeywordInterface();
            else
                WriteKeywordClass();

            if (za.Implements == null)
                Write(GetDecoratedTypeNameWithinNestedName(z));
            else
                Write(GetDecoratedTypeName(z, false));


            #region extends
            if (z.BaseType != typeof(object) && z.BaseType != null)
            {

                Write(" extends ");

                ScriptAttribute ba = ScriptAttribute.Of(z.BaseType, true);

                if (ba == null)
                    Break("extending object has no attribute");


                if (ba.Implements == null)
                    WriteDecoratedTypeName(z.BaseType);
                else
                    Write(GetDecoratedTypeName(z.BaseType, false));

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

                    WriteDecoratedTypeNameOrImplementationTypeName(timpv);
                }
            }
            #endregion

            WriteLine();
        }

        private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(timpv, false, false);
        }

        /// <summary>
        /// tries to use the implementation name
        /// </summary>
        /// <param name="timpv"></param>
        /// <param name="favorTargetType"></param>
        private void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType)
        {
            //[Script(Implements = typeof(global::System.Boolean),
            //    ImplementationType=typeof(java.lang.Integer))]


            Type iType = ResolveImplementation(timpv);

            if (iType != null)
            {
                if (favorTargetType)
                {
                    if (ScriptAttribute.OfProvider(iType).ImplementationType != null)
                        iType = null;
                }
            }

            if (iType == null)
                Write(GetDecoratedTypeName(timpv, true, favorPrimitives, true));
            else
                Write(GetDecoratedTypeName(iType, true));
        }

        private void WriteKeywordInterface()
        {
            Write("interface ");
        }


        public override void WriteDecoratedMethodParameter(ParameterInfo p)
        {
            Write(p.Name);
        }

        string ToJavaTypeName(string e)
        {
            e = e.Replace("`", "_");
            e = e.Replace("<", "_");
            e = e.Replace(">", "_");

            return e;
        }

        // http://www.idevelopment.info/data/Programming/java/miscellaneous_java/Java_Primitive_Types.html
        public override string GetDecoratedTypeName(Type type, bool bExternalAllowed)
        {
            if (type == null)
                return "null";

            return GetDecoratedTypeName(type, bExternalAllowed, true);

        }

        public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives)
        {
            return GetDecoratedTypeName(type, bExternalAllowed, bUsePrimitives, true);
        }

        public string GetDecoratedTypeName(Type type, bool bExternalAllowed, bool bUsePrimitives, bool bChopNestedParents)
        {
            if (type.IsArray)
            {
                return GetDecoratedTypeName(type.GetElementType(), bExternalAllowed, bUsePrimitives, bChopNestedParents) + "[]";
            }

            ScriptAttribute a = ScriptAttribute.Of(type, true);


            Type __impl = ResolveImplementation(type);

            if (bExternalAllowed && a == null && __impl != null)
            {
                a = ScriptAttribute.Of(__impl);
            }

            if (bExternalAllowed && a != null && a.ExternalTarget != null)
            {
                return a.ExternalTarget;
            }
            else
            {
                if (type.IsNested)
                {
                    List<string> x = new List<string>();

                    Type p = type;

                    if (bChopNestedParents && a != null && a.IsNative)
                        return type.Name;

                    while (p != null)
                    {
                        x.Add(ToJavaTypeName(p.Name));


                        p = p.DeclaringType;
                    }



                    x.Reverse();

                    // if they are native java inner types
                    // we need to use .

                    // custom nested classes are refactored as
                    // separate classes by _

                    if (a == null)
                        BreakToDebugger("typename");

                    return string.Join(
                        a.IsNative
                        ? "."
                        : "_", x.ToArray());
                }
                else
                {
                    if (bUsePrimitives)
                    {
                        if (type == typeof(void)) return "void";
                        else if (type == typeof(int)) return "int";
                        else if (type == typeof(double)) return "double";
                        else if (type == typeof(bool)) return "boolean";
                        else if (type == typeof(long)) return "long";
                        else if (type == typeof(byte)) Break("java does not support unsigned bytes");
                        else if (type == typeof(sbyte)) return "byte";
                        else if (type == typeof(char)) return "char";
                        else if (type == typeof(short)) return "short";
                        else if (type == typeof(float)) return "float";
                        else if (type == typeof(double)) return "double";

                        if (type.IsArray)
                        {
                            if (type.GetElementType() == typeof(sbyte))
                                return "byte[]";
                            else if (type.GetElementType() == typeof(float))
                                return "float[]";
                        }
                    }
                    else
                    {
                        // http://www.dotnetspider.com/tutorials/Datatypes.aspx
                        // box for java

                        if (type == typeof(int))
                            return "Integer";
                        if (type == typeof(long))
                            return "Long";
                        if (type == typeof(sbyte))
                            return "Byte";
                        if (type == typeof(float))
                            return "Float";
                    }

                    return ToJavaTypeName(type.Name);
                }
            }
        }




        public override Predicate<ILBlock.Prestatement> MethodBodyFilter
        {
            get
            {
                return
                 delegate(ILBlock.Prestatement p)
                 {
                     #region remove redundant returns
                     if (p.Instruction != null)
                         if (p.Instruction == OpCodes.Ret)
                             if (p.Instruction.Next == null)
                                 if (p.Instruction.StackBeforeStrict.Length == 0)
                                 {
                                     return true;
                                 }
                     #endregion

                     return false;
                 };
            }
        }

        public override bool SupportsInlineArrayInit
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsForStatements
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineThisReference
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineAssigments
        {
            get
            {
                return true;
            }
        }

        public override void WriteReturnParameter(ILBlock.Prestatement _p, ILInstruction _i)
        {
            if (_i.OwnerMethod is MethodInfo)
            {
                if ((_i.OwnerMethod as MethodInfo).ReturnType == typeof(bool))
                {
                    if (_i.InlineAssigmentValue != null)
                    {
                        if (_i.InlineAssigmentValue.Instruction.IsStoreLocal)
                        {

                            WriteReturnParameter(_p, _i.InlineAssigmentValue.Instruction.StackBeforeStrict[0].SingleStackInstruction);

                            return;
                        }
                    }

                    if (_i == OpCodes.Ldc_I4_0)
                    {
                        WriteKeywordFalse();

                        return;
                    }

                    if (_i == OpCodes.Ldc_I4_1)
                    {
                        WriteKeywordTrue();

                        return;
                    }
                }
            }

            base.WriteReturnParameter(_p, _i);
        }

        public override bool SupportsInlineExceptionVariable
        {
            get
            {
                return true;
            }
        }

        public override void WriteTypeConstructionVerified()
        {
            Write("new Object()");
        }

        public override void WriteInstanceOfOperator(ILInstruction value, Type type)
        {
            EmitInstruction(null, value);

            Write(" instanceof ");

            WriteDecoratedTypeName(type);
        }

        //protected override bool IsTypeCastRequired(Type e, ILFlow.StackItem s)
        //{
        //    if (e == typeof(int) && s.SingleStackInstruction.TargetInteger != null)
        //        return false;

        //    return true;
        //}


        protected override void WriteTypeOf(ILBlock.Prestatement p, ILInstruction i)
        {
            Emit(p, i.StackBeforeStrict[0]);

            Write(".class");
        }

    }
}
