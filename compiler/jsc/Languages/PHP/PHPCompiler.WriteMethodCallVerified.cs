using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

using jsc.CodeModel;
using jsc;

using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
    {
        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            bool bBase = false;

            if (m.IsInstanceConstructor())
                if (i != null)
                // CLR 4 seems to now tell that static ctor IsConstructor
                {
                    var _BaseType = i.OwnerMethod.DeclaringType.BaseType;
                    _BaseType = ResolveImplementation(_BaseType) ?? _BaseType;

                    _BaseType = _BaseType.IsGenericType ?
                        _BaseType.GetGenericTypeDefinition() :
                        _BaseType;

                    var m_DeclaringType = m.DeclaringType.IsGenericType ?
                        m.DeclaringType.GetGenericTypeDefinition() :
                        m.DeclaringType;

                    if (m_DeclaringType == _BaseType)
                    {

                        bBase = true;
                    }
                    else
                        Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
                }

            ScriptAttribute ma = ScriptAttribute.OfProvider(m);
            bool dStatic = ma != null && ma.DefineAsStatic;



            ILFlowStackItem[] s = i == null ? null : i.StackBeforeStrict;

            int offset = 1;



            if (m.IsStatic || dStatic || bBase)
            {
                if (bBase)
                {
                    WriteTypeBaseType();
                    Write("::");

                }



                offset = !m.IsStatic && (dStatic || bBase) ? 1 : 0;

            }
            else
            {


                Emit(p, s[0]);

                Write("->");
            }


            if (m.DeclaringType.ToScriptAttributeOrDefault().IsNative)
            {
                Write(m.Name);
            }
            else
                WriteMethodName(m);

            WriteParameterInfoFromStack(m, p, s, offset);

        }

    }
}
