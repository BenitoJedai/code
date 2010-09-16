using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    public abstract class PreprocessorDirectives : SolutionProjectLanguage
    {
        // http://www.csharphelp.com/2005/12/c-languages-preprocessor-directives/

        public static Keyword
                @if = "#if",
                @else = "#else",
                @endif = "#endif";
    }

    public static class PreprocessorDirectivesExtensions
    {
        public static void WriteConditionalCompilation(this SolutionProjectLanguage Language, SolutionFile File, PseudoIfExpression If, SolutionBuilder Context)
        {
            File.WriteSpace(PreprocessorDirectives.@if);
            Language.WritePseudoExpression(File, If.Expression, Context);
            File.WriteLine();

            Language.WriteMethodBody(File, If.TrueCase, Context);

            if (If.FalseCase != null)
            {
                File.WriteLine(PreprocessorDirectives.@else);
                Language.WriteMethodBody(File, If.FalseCase, Context);
            }

            File.WriteLine(PreprocessorDirectives.@endif);
        }
    }
}
