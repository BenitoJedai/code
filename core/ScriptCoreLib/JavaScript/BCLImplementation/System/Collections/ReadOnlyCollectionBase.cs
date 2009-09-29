using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.ReadOnlyCollectionBase))]
	internal class __ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		public virtual IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}



		#region ICollection Members

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsSynchronized
		{
			get { throw new NotImplementedException(); }
		}

		public object SyncRoot
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}
