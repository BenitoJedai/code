using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    public partial class VisualBasicLanguage
    {
        static bool IsExtensionMethod(SolutionProjectLanguageMethod m)
        {
            // it seems Visual Basic only supports extensions that also return a value which is used
            return false;
        }

        public override void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda, SolutionBuilder Context)
        {
            if (Lambda.Method.Name == SolutionProjectLanguageMethod.op_Implicit)
            {
                WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                return;
            }

            if (Lambda.Method.IsConstructor)
            {
                File.Write(Keywords.New);
                File.WriteSpace();
                WriteTypeName(File, Lambda.Method.DeclaringType);
                InternalWriteParameterList(File, Lambda, Context);
                return;
            }

            var Objectless = true;

            if (IsExtensionMethod(Lambda.Method))
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
                        WritePseudoExpression(File, Lambda.Object, Context);
                        Objectless = false;
                    }
                }
            }


            if (Lambda.Method.Name == "Invoke")
            {
                // in c# we can omit the .Invoke on a delegate
            }
            else
            {
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
            }

            if (Lambda.Method.IsProperty)
            {

                if (Lambda.ParameterExpressions.Length == 1)
                {
                    File.WriteSpace();

                    if (Lambda.IsAttributeContext)
                    {
                        File.Write(":=");
                    }
                    else
                    {
                        File.Write("=");
                    }

                    File.WriteSpace();
                    WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                }

            }
            else
            {

                InternalWriteParameterList(File, Lambda, Context);
            }


        }

        private void InternalWriteParameterList(SolutionFile File, PseudoCallExpression Lambda, SolutionBuilder Context)
        {
            File.Write("(");

            #region HasComplexParameter
            var HasComplexParameter = Lambda.ParameterExpressions.Any(
                k =>
                {
                    if (k is XElement)
                        return true;

                    // anonymous method!
                    if (k is SolutionProjectLanguageMethod)
                        return true;


                    var Call = k as PseudoCallExpression;
                    if (Call != null)
                    {
                        // what? :) 
                        if (Call.XLinq != null)
                            return true;
                    }

                    
                    return false;
                }
            );
            #endregion

            Action Body =
                delegate
                {
                    var Parameters = Lambda.ParameterExpressions.ToArray();

                    var FirstParameter = 0;

                    if (IsExtensionMethod(Lambda.Method))
                        FirstParameter = 1;

                    for (int i = FirstParameter; i < Parameters.Length; i++)
                    {
                        if (i > 0)
                        {
                            if (HasComplexParameter)
                            {
                                File.WriteLine(",");
                                File.WriteIndent();
                            }
                            else
                            {
                                File.WriteSpace(",");
                            }
                        }

                        var Parameter = Parameters[i];

                        WritePseudoExpression(File, Parameter, Context);
                    }
                };

            if (HasComplexParameter)
            {
                File.WriteLine();
                File.Indent(this,
                    delegate
                    {
                        if (Lambda.ParameterExpressions.FirstOrDefault() is XElement)
                        {
                            // xlinq has no indent...
                        }
                        else
                        {
                            File.WriteIndent();
                        }

                        Body();

                        File.WriteLine();
                    }
                );
                File.WriteIndent();
            }
            else
            {
                Body();
            }

            File.Write(")");
        }


    }
}
