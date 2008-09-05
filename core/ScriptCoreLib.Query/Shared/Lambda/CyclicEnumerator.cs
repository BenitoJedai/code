using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public static class CyclicEnumeratorExtensions
	{
		public static IEnumerable<T> AsCyclicEnumerable<T>(this IEnumerable<T> source)
		{
			return new CyclicEnumerator<T>(source);
		}
	}

	[Script]
	public class CyclicEnumerator<T> : IEnumerator<T>, IEnumerable<T>
	{
		readonly IEnumerable<T> Source;

		IEnumerator<T> Stream;

		public CyclicEnumerator(IEnumerable<T> Source)
		{
			this.Source = Source.AsEnumerable();
		}

		#region IEnumerator<T> Members

		public T Current
		{
			get { return Stream.Current; }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (Stream != null)
				Stream.Dispose();
		}

		#endregion

		#region IEnumerator Members

		object System.Collections.IEnumerator.Current
		{
			get { return Stream.Current; }
		}

		public bool MoveNext()
		{
			if (Stream == null)
				Stream = Source.GetEnumerator();

			if (Stream.MoveNext())
				return true;

			Stream = Source.GetEnumerator();

			if (Stream.MoveNext())
				return true;

			return false;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return new CyclicEnumerator<T>(Source);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
