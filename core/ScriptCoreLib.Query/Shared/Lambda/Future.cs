using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public interface IFutureContinue
	{
		void InternalContinue(Action e);
	}

	[Script]
	public class Future : IFutureContinue
	{
		internal List<Action> _Continue = new List<Action>();

		public void InternalContinue(Action e)
		{
			this.Continue(e);
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
				var c = _Continue;
				_Continue = null;

				c.Do();
				c.Clear();

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
				this.Continue(() => e(this.Value));
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
		public static void Continue(this Future f, Action e)
		{
			if (f != null)
				if (f._Continue != null)
				{
					f._Continue.Add(e);
					return;
				}

			e();
		}

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

	[Script]
	public class FutureLock : IFutureContinue
	{
		Future Lock;

		public void InternalContinue(Action e)
		{
			Continue(e);
		}

		public void Continue(Action e)
		{
			Lock.Continue(e);
		}

		public bool IsAcquired;

		public void Acquire(Action e)
		{
			if (Pending != null)
				Pending();

			Continue(
				delegate
				{
					Lock = new Future();

					IsAcquired = true;
					if (Acquired != null)
						Acquired();

					e();
				}
			);
		}

		/// <summary>
		/// Returns a handler that will acquire a lock after locks named in the parameters
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Action<Action> this[params IFutureContinue[] e]
		{
			get
			{
				return 
					done =>
					{
						e.ForEach(
							(v, next) =>
							{
								v.InternalContinue(next);
							}
						)(() => this.Acquire(done));
					};
			}
		}

		public void Release()
		{
			var x = Lock;

			Lock = null;

			IsAcquired = false;
			if (Released != null)
				Released();

			if (x == null)
				return;

			x.Signal();
		}

		public event Action Released;
		public event Action Acquired;
		public event Action Pending;
	}

}
