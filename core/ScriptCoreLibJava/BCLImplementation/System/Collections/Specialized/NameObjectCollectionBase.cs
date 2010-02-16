using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using System.Runtime.Serialization;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized
{
	[Script(Implements = typeof(global::System.Collections.Specialized.NameObjectCollectionBase))]
	internal abstract class __NameObjectCollectionBase : ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		#region IDeserializationCallback Members

		public void OnDeserialization(object sender)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

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
