using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Threading;

namespace ScriptCoreLibJava.Threading
{
	[Script]
	[Obsolete("You can now use new System.Threading.Thread(); instead!")]
	public abstract class ThreadedAction
	{
		public abstract void Invoke();
	}


	[Script]
	public class ThreadedActionInvoker
	{
		public ThreadedAction Handler;
		public int Delay;

		public System.Threading.Thread Thread;

		public void DelayThenInvoke()
		{
			if (Delay > 0)
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

	[Script(Implements = typeof(ThreadedActionRoutedExtensions))]
	internal static class __ThreadedActionRoutedExtensions
	{

		public static ThreadedActionInvoker ToThreadedActionInvoker(ThreadedAction handler)
		{
			var x = new __ThreadedActionInvoker
			{
				Handler = handler
			};

			object t = new __Thread { InternalValue = new java.lang.Thread(x) };

			x.Thread = (System.Threading.Thread)t;

			return x;
		}
	}

	public static class ThreadedActionRoutedExtensions
	{
		public static ThreadedActionInvoker ToThreadedActionInvoker(this ThreadedAction handler)
		{
			var x = new ThreadedActionInvoker
			{
				Handler = handler
			};

			x.Thread = new System.Threading.Thread(x.DelayThenInvoke);

			return x;
		}
	}

	[Script]
	public static class DelayedActionExtensions
	{
		public static void Tester()
		{
			Console.WriteLine();
		}

		public static ThreadedActionInvoker AtDelay(this int ms, ThreadedAction handler)
		{
			var x = handler.ToThreadedActionInvoker();

			x.Delay = ms;

			x.Thread.Name = "AtDelay " + ms; 
			x.Thread.IsBackground = true;
			x.Thread.Start();

			return x;
		}

		
		public static void WithTimeout(this int ms, ThreadedAction handler)
		{
			var x = handler.ToThreadedActionInvoker();

			x.Delay = 0;

			x.Thread.Name = "WithTimeout " + ms;
			x.Thread.IsBackground = true;
			x.Thread.Start();

			if (x.Thread.Join(ms))
				return;

			
			x.Thread.Abort();
		}
	}
}
