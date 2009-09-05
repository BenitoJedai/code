using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ArgumentsViaReflection.Library
{
	public class FuncEnumerator : IEnumerator
	{
		/// <summary>
		/// Null marks end of sequence
		/// </summary>
		/// <returns></returns>
		public delegate object GetNextTargetDelegate();

		readonly GetNextTargetDelegate GetNextTarget;

		object InternalCurrent;

		public FuncEnumerator(GetNextTargetDelegate e)
		{
			this.GetNextTarget = e;
		}

	
		#region IEnumerator Members

		public object Current
		{
			get { return InternalCurrent; }
		}

		public bool MoveNext()
		{
			InternalCurrent = this.GetNextTarget();

			return InternalCurrent != null;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
