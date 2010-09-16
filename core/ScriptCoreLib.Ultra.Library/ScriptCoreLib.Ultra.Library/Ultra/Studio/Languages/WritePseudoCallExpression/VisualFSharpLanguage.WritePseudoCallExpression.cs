using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    partial class VisualFSharpLanguage
    {
        public override void WritePseudoCallExpression(SolutionFile File, ScriptCoreLib.Ultra.Studio.PseudoExpressions.PseudoCallExpression Lambda, SolutionBuilder Context)
        {

            if (Lambda.Method.IsConstructor)
            {
                File.Write(Keywords.@new);
                File.WriteSpace();
                WriteTypeName(File, Lambda.Method.DeclaringType);
                InternalWriteParameterList(File, Lambda, Context);
                return;
            }

            if (Lambda.Method.OperatorName != null)
            {
                if (Lambda.ParameterExpressions.Length == 2)
                {
                    WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                    File.WriteSpaces(Lambda.Method.OperatorName);
                    WritePseudoExpression(File, Lambda.ParameterExpressions[1], Context);

                    return;
                }
            }


            var Objectless = true;

            if (Lambda.Method.IsExtensionMethod)
            {
                WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                Objectless = false;
            }
            else
            {
                if (Lambda.Method.IsStatic)
                {
                    if (Lambda.Method.DeclaringType != null)
                    {
                        WriteTypeName(File, Lambda.Method.DeclaringType);
                        Objectless = false;
                    }
                }
                else
                {
                    if (Lambda.Object != null)
                    {
                        var Constructor = Lambda.Object as PseudoExpressions.PseudoCallExpression;
                        if (Constructor != null)
                        {
                            if (!Constructor.Method.IsConstructor)
                                Constructor = null;
                        }

                        if (Constructor != null)
                        {
                            File.Write("(");
                        }
                        WritePseudoExpression(File, Lambda.Object, Context);
                        if (Constructor != null)
                        {
                            File.Write(")");
                        }
                        Objectless = false;
                    }
                }
            }



            var Target = Lambda.Method.Name;

            if (Lambda.Method.IsProperty)
            {
                Target = Target.SkipUntilIfAny("set_").SkipUntilIfAny("get_");

            }

            if (!Objectless)
            {
                File.Write(".");
            }

            File.Write(
                new SolutionFileWriteArguments
                {
                    Fragment = SolutionFileTextFragment.None,
                    Text = Target,
                    Tag = Lambda.Method
                }
            );

            if (Lambda.Method.IsProperty)
            {
                if (Lambda.ParameterExpressions.Length == 1)
                {
                    File.WriteSpace();
                    if (Lambda.IsAttributeContext)
                        File.Write("=");
                    else
                        File.Write("<-");
                    File.WriteSpace();
                    WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                }

            }
            else
            {
                InternalWriteParameterList(File, Lambda, Context);
                ;
            }
        }

    }
}
