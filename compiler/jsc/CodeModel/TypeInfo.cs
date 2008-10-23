using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.CodeModel
{
    public class TypeInfo
    {
        public Type Value;

        public TypeInfo(Type e)
        {
            Value = e;
        }

        public bool IsCompilerGenerated
        {
            get
            {
                return ScriptAttribute.IsCompilerGenerated(Value);
            }
        }

        public bool IsScript
        {
            get
            {
                return ScriptAttribute.Of(Value) != null;
            }
        }

        public Func<TypeInfo, string> TargetFileNameHandler;

        public string TargetFileName
        {
            get
            {
                
                return TargetFileNameHandler(this);
            }
        }

        public string AssamblyFileName
        {
            get
            {
                return new FileInfo(Value.Assembly.Location).Name;
            }
        }

        public string Win32TypeName
        {
            get
            {
                //if (Value.FullName == null)
                {
                    Type p = Value;

					string u = CompilerBase.GetSafeLiteral(Value.Name, CompilerBase.DefaultIsSafeLiteralChar);

                    while (p.IsNested)
                    {
                        p = p.DeclaringType;

                        u = CompilerBase.GetSafeLiteral(p.Name, CompilerBase.DefaultIsSafeLiteralChar) + "+" + u;
                    }

                    if (p.Namespace != null)
						u = CompilerBase.GetSafeLiteral(p.Namespace, CompilerBase.DefaultIsSafeLiteralChar) + "." + u;

                    return Helper.GetSafeWin32FileName(u);
                }

                //return Helper.GetSafeWin32FileName(Value.FullName);
            }
        }
    }
}
