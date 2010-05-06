using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageCode : IEnumerable
	{
		// this type shall enable collection initializer
		// and method call replay

		public readonly ArrayList History = new ArrayList();

		public void Add(string Comment)
		{
			this.History.Add(Comment);
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
