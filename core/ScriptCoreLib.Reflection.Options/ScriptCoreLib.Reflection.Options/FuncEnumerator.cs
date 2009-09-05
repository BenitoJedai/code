using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Reflection.Options
{
	internal class FuncEnumerator : IEnumerator
	{
		/// <summary>
		/// Null marks end of sequence
		/// </summary>
		readonly ObjectFunc GetNextTarget;

		object InternalCurrent;

		public FuncEnumerator(ObjectFunc e)
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
