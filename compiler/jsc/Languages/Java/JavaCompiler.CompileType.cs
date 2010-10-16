
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        public Action CompileType_WriteAdditionalMembers;

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            if (z.Name.Contains("<PrivateImplementationDetails>") || (z.DeclaringType != null && z.DeclaringType.Name.Contains("<PrivateImplementationDetails>")))
                return false;

            // why would we do that?
            //if (IsEmptyImplementationType(z))
            //    return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();
            WriteCommentLine(Path.GetFileName(z.Assembly.Location));

            if (z.Namespace != null)
            {
                this.WriteIndent();
                this.WriteKeywordSpace(Keywords._package);

                var _namespace = string.Join(".", NamespaceFixup(z.Namespace, z).Split('.').Select(k => GetSafeLiteral(k)).ToArray());

                this.Write(_namespace + ";");

                //this.Write(NamespaceFixup(z.Namespace, z) + ";");
                this.WriteLine();
                this.WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);

            var z_Implements = za.Implements;
            var z_NonPrimitiveValueType = z_Implements != null && z_Implements.IsValueType && !z_Implements.IsPrimitive;


            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion

            CompileType_WriteAdditionalMembers = delegate { };

            WriteTypeSignature(z, za);

            using (CreateScope())
            {
                WriteTypeFields(z, za);
                WriteLine();
                WriteTypeStaticConstructor(z, za);
                WriteLine();

                // why was this check here?
                //if (za.Implements == null)
                //{
                WriteTypeInstanceConstructors(z);
                WriteLine();
                //}

                WriteTypeInstanceMethods(z, za);
                WriteLine();
                WriteTypeStaticMethods(z, za);

                if (za.Implements == typeof(Delegate))
                {
                    DelegateImplementationProvider.WriteExtensionMethodSupport(this, z);
                }

                CompileType_WriteAdditionalMembers();


                if (z_NonPrimitiveValueType)
                {
                    // define ctor as methods
                    WriteIndent();
                    WriteCommentLine("NonPrimitiveValueType");

                    foreach (var NonPrimitiveValueTypeConstructor in z.GetInstanceConstructors())
                    {
                        InternalWriteMethodSignature(
                            NonPrimitiveValueTypeConstructor,
                            false,
                            "NonPrimitiveValueTypeConstructor",
                            false
                        );

                        WriteMethodBody(NonPrimitiveValueTypeConstructor);
                    }
                }
            }

            //Thread.Sleep(100);

            return true;
        }

        protected override bool WriteMethodCustomBody(MethodBase m)
        {
            if (m.DeclaringType.IsDelegate())
            {
                if (m.IsConstructor)
                {
                    DelegateImplementationProvider.WriteConstructor(this, (ConstructorInfo)m);
                    return true;
                }

                if (m.Name == "BeginInvoke")
                {
                    DelegateImplementationProvider.WriteBeginInvoke(this, (MethodInfo)m);
                    return true;
                }

                if (m.Name == "EndInvoke")
                {
                    DelegateImplementationProvider.WriteEndInvoke(this, (MethodInfo)m);
                    return true;
                }

                if (m.Name == "Invoke")
                {
                    DelegateImplementationProvider.WriteInvoke(this, (MethodInfo)m);
                    return true;

                }
            }

            if ((m.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PinvokeImpl)
            {
                var DllImport = m.GetCustomAttributes<DllImportAttribute>().Single();
                var ReturnType = ((MethodInfo)m).ReturnType;
                // cool.
                // do we have Platform Invocation Services?

                Action<MethodBase> WriteInvoke =
                    TargetMethod =>
                    {
                        this.WriteIndent();

                        this.WriteKeywordSpace(Keywords._return);
                        this.WriteDecoratedTypeName(TargetMethod.DeclaringType);
                        this.Write(".");
                        this.WriteDecoratedMethodName(TargetMethod, false);
                        this.Write("(");

                        this.WriteQuotedLiteral(DllImport.Value);
                        this.WriteSpace(", ");
                        this.WriteQuotedLiteral(DllImport.EntryPoint);
                        this.WriteLine(",");
                        this.Ident++;
                        
                        this.WriteIndent();
                        this.WriteKeywordSpace(Keywords._new);
                        this.WriteDecoratedTypeName(typeof(object));
                        this.WriteSpace();
                        this.Write("[]");
                        this.WriteSpace();
                        this.WriteLine();

                        var IntPtrToPointerToken = ((Func<IntPtr, object>)PlatformInvocationServices.IntPtrToPointerToken).Method;

                        
                        using (this.CreateScope())
                        {

                            var p = m.GetParameters();
                            for (int i = 0; i < p.Length; i++)
                            {
                                if (i > 0)
                                    this.WriteLine(",");

                                this.WriteIndent();

                                if (p[i].ParameterType == typeof(IntPtr))
                                {
                                    this.WriteDecoratedTypeName(IntPtrToPointerToken.DeclaringType);
                                    this.Write(".");
                                    this.WriteDecoratedMethodName(IntPtrToPointerToken, false);
                                    this.Write("(");
                                    this.WriteDecoratedMethodParameter(p[i], typeof(IntPtr));
                                    this.Write(")");
                                }
                                else
                                {
                                    this.WriteDecoratedMethodParameter(p[i], typeof(object));
                                }
                            }

                            this.WriteLine();
                        }
                        this.Ident--;

                        this.WriteIndent();
                        this.Write(")");
                        this.Write(";");

                        this.WriteLine();
                    };

                if (ReturnType == typeof(int))
                {
                    Func<string, string, object[], int> _InvokeInt32 = PlatformInvocationServices.InvokeInt32;

                    var _Resolved_InvokeInt32 = this.ResolveImplementationMethod(_InvokeInt32.Method.DeclaringType, _InvokeInt32.Method);

                    if (_Resolved_InvokeInt32 == null)
                        throw new NotSupportedException("PlatformInvocationServices.InvokeInt32 implementation was not found.");


                    WriteInvoke(_Resolved_InvokeInt32);

                    return true;
                }

                if (ReturnType == typeof(string))
                {
                    Func<string, string, object[], string> _InvokeString = PlatformInvocationServices.InvokeString;

                    var _Resolved = this.ResolveImplementationMethod(_InvokeString.Method.DeclaringType, _InvokeString.Method);

                    if (_Resolved == null)
                        throw new NotSupportedException("PlatformInvocationServices.InvokeString implementation was not found.");

                    WriteInvoke(_Resolved);

                    return true;
                }

                if (ReturnType == typeof(IntPtr))
                {
                    Func<string, string, object[], IntPtr> _InvokeIntPtr = PlatformInvocationServices.InvokeIntPtr;

                    var _Resolved = this.ResolveImplementationMethod(_InvokeIntPtr.Method.DeclaringType, _InvokeIntPtr.Method);

                    if (_Resolved == null)
                        throw new NotSupportedException("PlatformInvocationServices.InvokeIntPtr implementation was not found.");

                    WriteInvoke(_Resolved);

                    return true;
                }


                throw new NotSupportedException("PlatformInvocationServices for " + ReturnType.FullName + " not implemented");
            }

            return false;
        }
    }
}
