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
using ScriptCoreLib.CSharp.Extensions;


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
				var ResolvedTarget = this.ResolveImplementation(x) ?? x;

				if (x.IsArray || x.ToScriptAttributeOrDefault().IsArray ||
					(ResolvedTarget.ToScriptAttributeOrDefault().ImplementationType != null && ResolvedTarget.ToScriptAttributeOrDefault().ImplementationType.ToScriptAttributeOrDefault().IsArray))
				{
					// http://help.adobe.com/en_US/AS3LCR/Flash_10.0/compilerWarnings.html#1113
					// Array(x) behaves the same as new Array(x). To cast a value to type
					// Array use the expression x as Array instead of Array(x).
					Write("(");
					EmitFirstOnStack(e);
					
					WriteSpace();
					WriteKeywordSpace(Keywords._as);

					Write("Array");
					Write(")");

				}
				else
				{
					//this.WriteBoxedComment("cast type: " + x.FullName);


					Write("(");
					WriteDecoratedTypeNameOrImplementationTypeName(x, true, true, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, x));
					Write("(");
					EmitFirstOnStack(e);
					Write(")");
					Write(")");
				}
            }
        }

        public override void ConvertTypeAndEmit(CodeEmitArgs e, string x)
        {
			//this.WriteBoxedComment("cast string");

			if (x == "Array")
			{
				Write("(");
				EmitFirstOnStack(e);

				WriteSpace();
				WriteKeywordSpace(Keywords._as);

				Write("Array");
				Write(")");
				return;
			}

            Write("(" + x + "(");
            EmitFirstOnStack(e);
            Write("))");
        }

    }
}
