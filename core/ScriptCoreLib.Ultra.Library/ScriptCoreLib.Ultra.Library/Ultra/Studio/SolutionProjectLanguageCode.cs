using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageCode : IEnumerable
	{
		// this type shall enable collection initializer
		// and method call replay

        // union? :)
        public PseudoIfExpression OwnerIfExpression;
        public SolutionProjectLanguageMethod OwnerMethod;

		public readonly ArrayList History = new ArrayList();

		public void Add(string Comment)
		{
			this.History.Add(Comment);
		}

		public void Add(SolutionFileComment Comment)
		{
			this.History.Add(Comment);
		}

		public void Add(PseudoCallExpression e)
		{
			// no DLR yet. we use our lite version instead.
			this.History.Add(e);
		}


		public void Add(PseudoIfExpression e)
		{
			// no DLR yet. we use our lite version instead.
            this.History.Add(e);
		}


        public bool IsLambdaExpression
        {
            get
            {
                if (this.OwnerMethod == null)
                    return false;

                if (!this.OwnerMethod.IsLambda)
                    return false;

                if (this.History.Count != 1)
                    return false;

                return true;
            }
        }

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

        public bool IsConditionalCompilationDirectiveCode
        {
            get
            {
                if (this.OwnerIfExpression == null)
                    return false;

                if (this.OwnerIfExpression.IsConditionalCompilationDirective)
                    return true;

                return false;
            }
        }
    }
}
