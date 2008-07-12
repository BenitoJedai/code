using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using jsc.Script;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
        public void ConvertTypeAndEmit(CodeEmitArgs e, Type x)
        {

            var s = e.i.StackBeforeStrict.Single().SingleStackInstruction;

            if (s.OpCode == OpCodes.Box)
            {
                var BoxParam = s.StackBeforeStrict.Single();
                var BoxParamType = BoxParam.SingleStackInstruction.ReferencedType;

                if ((MySession.ResolveImplementation(BoxParamType) ?? BoxParamType) == x)
                {
                    Emit(e.p, BoxParam);

                    return;
                }
            }

            var r = s.ReferencedType;
            var ra = r.ToScriptAttribute();


            if (r == x || (ra != null && ra.IsArray))
            {
                EmitFirstOnStack(e);
                return;
            }

            // prevent compiler being funny: a.Add_100664081((*(e)));
            if (x.IsGenericParameter)
            {
                EmitFirstOnStack(e);
            }
            else
            {
                Write("(");
                WriteDecoratedTypeNameOrImplementationTypeName(x, true, true, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, x));
                Write("(");
                EmitFirstOnStack(e);
                Write(")");
                Write(")");
            }
        }

        public override void ConvertTypeAndEmit(CodeEmitArgs e, string x)
        {
            Write("(" + x + "(");
            EmitFirstOnStack(e);
            Write("))");
        }

    }
}
