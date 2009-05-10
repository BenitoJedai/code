using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public static class CyclicEnumeratorExtensions
	{
		public static Action ToCyclicAction<T>(this IEnumerable<T> source, Action<T> handler)
		{
			var e = source.AsCyclicEnumerator();

			return delegate
			{
				if (e.MoveNext())
					handler(e.Current);
			};
		}

		public static IEnumerator<T> AsCyclicEnumerator<T>(this IEnumerable<T> source)
		{
			return source.AsCyclicEnumerable().GetEnumerator();
		}

		public static IEnumerable<T> AsCyclicEnumerable<T>(this IEnumerable<T> source)
		{
			return new CyclicEnumerator<T>(() => source);
		}

		public static IEnumerable<T> AsCyclicEnumerable<T>(this Func<IEnumerable<T>> source)
		{
			return new CyclicEnumerator<T>(source);
		}
	}

	[Script]
	public class CyclicEnumerator<T> : IEnumerator<T>, IEnumerable<T>
	{

		IEnumerator<T> Stream;

		readonly Func<IEnumerable<T>> GetSource;

		public CyclicEnumerator(Func<IEnumerable<T>> GetSource)
		{
			this.GetSource = GetSource;

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
			{
				var Source = GetSource();

				if (Source == null)
					return false;

				Stream = Source.AsEnumerable().GetEnumerator();
			}

			if (Stream.MoveNext())
				return true;

			{
				var Source = GetSource();

				if (Source == null)
					return false;

				Stream = Source.AsEnumerable().GetEnumerator();
			}

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
			return new CyclicEnumerator<T>(GetSource);
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
