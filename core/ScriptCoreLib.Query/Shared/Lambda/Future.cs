using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public class Future
	{
		List<Action> _Continue = new List<Action>();

		public void Continue(Action e)
		{
			if (_Continue != null)
			{
				_Continue.Add(e);
				return;
			}

			e();
		}

		public bool CanSignal
		{
			get
			{
				return (_Continue != null);
			}
		}

		public void Signal()
		{
			if (CanSignal)
			{
				_Continue.Do();
				_Continue.Clear();
				_Continue = null;
			}
		}
	}

	[Script]
	public class Future<T> : Future
	{
		T _Value;

		public T Value
		{
			get
			{
				return _Value;
			}

			set
			{
				if (!CanSignal)
					throw new Exception("Value can be set only once!");

				_Value = value;


				this.Signal();



			}
		}



		public void Continue(Action<T> e)
		{
			if (CanSignal)
			{
				Continue(() => e(this.Value));
				return;
			}

			e(this.Value);
		}

		public static implicit operator Action<T>(Future<T> e)
		{
			return value => e.Value = value;
		}
	}


	[Script]
	public class FutureAction<T> : Future<Action<T>>
	{
		public void Continue(T e)
		{
			this.Continue(Handler => Handler(e));
		}

		public static implicit operator Action<T>(FutureAction<T> e)
		{
			return e.Continue;
		}

		public Action this[T e]
		{
			get
			{
				return () => this.Continue(e);
			}
		}
	}

	[Script]
	public class FutureStream
	{
		Future Gate;

		public FutureStream()
		{
			this.Gate = new Future();
		}



		/// <summary>
		/// Returns an action to signal this event
		/// </summary>
		/// <param name="PublishSignalNext"></param>
		/// <returns></returns>
		public Action Continue(Action<Action> PublishSignalNext)
		{

			var Next = new Future();
			var Previous = Gate;

			Gate = Next;

			Previous.Continue(
				delegate
				{
					PublishSignalNext(Next.Signal);
				}
			);


			return Previous.Signal;
		}
	}

	[Script]
	public static class FutureExtensions
	{
		public static Action<Action> ForEach<T>(this IEnumerable<T> source, Action<Action> ready, Action<T, Action> handler, Action done)
		{
			return source.ForEach(ready, (value, i, SignalNext) => handler(value, SignalNext), done);
		}

		public static Action<Action> ForEach<T>(this IEnumerable<T> source, Action<T, Action> handler)
		{
			return source.ForEach(null, (value, i, SignalNext) => handler(value, SignalNext), null);
		}

		public static Action<Action> ForEach<T>(this IEnumerable<T> source, Action<T, int, Action> handler)
		{
			return source.ForEach(null, handler, null);
		}

		public static Action<Action> ForEach<T>(this IEnumerable<T> source, Action<Action> ready, Action<T, int, Action> handler, Action done)
		{
			var c = new FutureStream();
			var e = default(IEnumerator<T>);
			var i = -1;
			var r = new Future();

			var MoveNext = default(Action<Action>);

			//  
			MoveNext =
				SignalNext =>
				{
					if (e.MoveNext())
					{
						i++;
						c.Continue(MoveNext);

						handler(e.Current, i, SignalNext);
					}
					else
					{
						e.Dispose();
						e = null;
						c = null;
						MoveNext = null;

						if (done != null)
							done();

						r.Signal();
					}
				};

			var SignalFirst = c.Continue(
					SignalNext =>
					{
						e = source.AsEnumerable().GetEnumerator();

						MoveNext(SignalNext);
					}
				);

			if (ready != null)
				ready(SignalFirst);
			else
				SignalFirst();

			return r.Continue;
		}

		/// <summary>
		/// Returns an action to continue when done
		/// </summary>
		/// <param name="e"></param>
		/// <param name="condition"></param>
		/// <returns></returns>
		public static Action<Action> While(this Action<Action> e, Func<bool> condition)
		{
			var c = new FutureStream();
			var r = new Future();

			var MoveNext = default(Action<Action>);

			MoveNext =
				SignalNext =>
				{
					if (condition())
					{
						// when SignalNext is called MoveNext is called too...
						c.Continue(MoveNext);

						e(SignalNext);
					}
					else
					{
						// we need to signal r to indicate we are done
						r.Signal();
					}
				};




			// we could just return s so the caller can initiate the loop on its own sometime later
			// yet for now we just start it here
			c.Continue(MoveNext)();
			
			return r.Continue;
		}
	}
}
