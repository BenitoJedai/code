using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.Lambda
{
	partial class LambdaExtensions
	{
		[Script]
		public sealed class ConcatStream<T> : IEnumerable<T>
		{
			public IEnumerable<T> Source;

			readonly Queue<T> Queue = new Queue<T>();

			public void Add(T e)
			{
				Queue.Enqueue(e);
			}

			#region IEnumerable<T> Members

			public IEnumerator<T> GetEnumerator()
			{
				return new Enumerator(Source.GetEnumerator(), Queue);
			}

			#endregion

			#region IEnumerable Members

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#endregion


			[Script]
			internal class Enumerator : IEnumerator<T>
			{
				readonly IEnumerator<T> Source;
				readonly Queue<T> Queue;

				public Enumerator(IEnumerator<T> Source, Queue<T> Queue)
				{
					this.Source = Source;
					this.Queue = Queue;
				}

				#region IEnumerator<T> Members

				T InternalCurrent;

				public T Current
				{
					get { return InternalCurrent; }
				}

				#endregion

				#region IDisposable Members

				public void Dispose()
				{

				}

				#endregion

				#region IEnumerator Members

				object System.Collections.IEnumerator.Current
				{
					get { return this.Current; }
				}


				public bool MoveNext()
				{
					InternalCurrent = default(T);

					if (this.Source.MoveNext())
					{
						InternalCurrent = Source.Current;
						return true;
					}

					if (this.Queue.Count > 0)
					{
						InternalCurrent = Queue.Dequeue();
						return true;
					}

					return false;
				}

				public void Reset()
				{
					throw new NotImplementedException();
				}

				#endregion
			}
		}


		public static ConcatStream<T> ToConcatStream<T>(this IEnumerable<T> source)
		{
			return new ConcatStream<T> { Source = source };
		}
	}
}
