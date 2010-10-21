
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
using ScriptCoreLib.Shared;
using System.Runtime.InteropServices;
using System.Linq;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
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

                        if (ReturnType != typeof(void))
                        {
                            this.WriteKeywordSpace(Keywords._return);
                        }

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

                if (ReturnType == typeof(int) || (ReturnType.IsEnum && Enum.GetUnderlyingType(ReturnType) == typeof(int)))
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

                if (ReturnType == typeof(bool))
                {
                    Func<string, string, object[], bool> _Invoke = PlatformInvocationServices.InvokeBoolean;

                    var _Resolved = this.ResolveImplementationMethod(_Invoke.Method.DeclaringType, _Invoke.Method);

                    if (_Resolved == null)
                        throw new NotSupportedException("PlatformInvocationServices.InvokeBoolean implementation was not found.");

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

                if (ReturnType == typeof(void))
                {
                    Action<string, string, object[]> _Invoke = PlatformInvocationServices.InvokeVoid;

                    var _Resolved = this.ResolveImplementationMethod(_Invoke.Method.DeclaringType, _Invoke.Method);

                    if (_Resolved == null)
                        throw new NotSupportedException("PlatformInvocationServices.InvokeVoid implementation was not found.");

                    WriteInvoke(_Resolved);

                    return true;
                }

                throw new NotSupportedException("PlatformInvocationServices for " + ReturnType.FullName + " not implemented");
            }

            return false;
        }


    }
}
