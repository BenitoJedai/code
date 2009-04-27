using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.Threading
{
	[Script]
	public abstract class ThreadedAction
	{
		public abstract void Invoke();
	}

	[Script]
	public abstract class ConditionalThreadedAction : ThreadedAction
	{
		[Script]
		public interface ICondition
		{
			bool Value { get; }
		}

		public ICondition Condition;

		[Script]
		public class While : ICondition, IDisposable
		{
			public While()
			{
				this.Value = true;
			}

			#region IDisposable Members

			public void Dispose()
			{
				this.Value = false;
			}

			#endregion

			#region ICondition Members

			public bool Value
			{
				get;
				set;
			}

			#endregion
		}
		public sealed override void Invoke()
		{
			if (Condition.Value)
				ConditionalInvoke();

		}

		public abstract void ConditionalInvoke();
	}

	[Script]
	public class ThreadedActionInvoker
	{
		public ThreadedAction Handler;
		public int Delay;

		public void DelayThenInvoke()
		{
			System.Threading.Thread.Sleep(Delay);
			Handler.Invoke();
		}
	}

	[Script]
	internal class __ThreadedActionInvoker : ThreadedActionInvoker, java.lang.Runnable
	{
		#region Runnable Members

		public void run()
		{
			this.DelayThenInvoke();
		}

		#endregion
	}

	[Script(Implements = typeof(DelayedActionExtensions))]
	internal class __DelayedActionExtensions
	{
		public static ThreadedAction AtDelay(int ms, ThreadedAction handler)
		{
			var x = new __ThreadedActionInvoker { Delay = ms, Handler = handler };

			var t = new java.lang.Thread(x);
			t.setDaemon(true);


			t.start();

			return handler;
		}
	}

	public static class DelayedActionExtensions
	{
		public static ThreadedAction AtDelay(this int ms, ThreadedAction handler)
		{
			var x = new ThreadedActionInvoker { Delay = ms, Handler = handler };

			var t = new System.Threading.Thread(x.DelayThenInvoke);
			t.IsBackground = true;
			t.Start();
			return handler;
		}
	}
}
