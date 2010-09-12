using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoIfExpression
	{
        /// <summary>
        /// See more: http://en.csharp-online.net/ECMA-334:_9.5.4_Conditional_compilation_directives
        /// </summary>
        public bool IsConditionalCompilationDirective;

		public object Expression;

        SolutionProjectLanguageCode InternalTrueCase;
        public SolutionProjectLanguageCode TrueCase
        {
            get
            {
                return InternalTrueCase;
            }
            set
            {
                InternalTrueCase = value.With(n => n.OwnerIfExpression = this);
            }
        }

        SolutionProjectLanguageCode InternalFalseCase;
        public SolutionProjectLanguageCode FalseCase
        {
            get
            {
                return InternalFalseCase;
            }
            set
            {
                InternalFalseCase = value.With(n => n.OwnerIfExpression = this);
            }
        }
	}
}
