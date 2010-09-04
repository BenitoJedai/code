using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public class PseudoCallExpression
	{

		public SolutionFileComment Comment;

        object InternalObject;
        public object Object
        {
            get
            {
                if (this.InternalObject == null)
                    if (this.GetObject != null)
                        this.InternalObject = this.GetObject();

                return this.InternalObject;
            }
            set
            {
                this.InternalObject = value;
            }
        }

        public Func<object> GetObject;

		public SolutionProjectLanguageMethod Method;

		public object[] ParameterExpressions = new object[0];


		public bool IsAttributeContext;

		public static implicit operator Uri(PseudoCallExpression that)
		{
			if (that.Comment == null)
				return null;

			return that.Comment.Link;
		}

		/// <summary>
		/// Visual Basic can inline xml. When this field is set, 
		/// this expression is equal to the XLinq field.
		/// </summary>
		public XElement XLinq;

        public override string ToString()
        {
            return this.Method.ToString();
        }
	}
}
