using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/threading/thread.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Thread.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Thread.cs

	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Threading/Thread.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Activator.cs
	// https://github.com/konsoletyper/teavm/blob/master/teavm-classlib/src/main/java/org/teavm/classlib/java/lang/TThread.java

	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Thread.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs
	// X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Thread.cs

	[Script(Implements = typeof(global::System.Threading.Thread))]
	internal class __Thread
	{
		// https://github.com/kswoll/WootzJs/wiki/Limitations

		// More generally, the cost of context switches is why high-performance code is often thread-per-core and not thread-per-logical-task.
		// http://danluu.com/new-cpu-features/

		// http://nexusilluminati.blogspot.com/2013/10/the-big-lie-of-genetics-exposed-human.html
		//somehow the cells in your hand knew they were supposed to grow into a hand, not a foot or an ear, for example.



		// https://zproxy.wordpress.com/2009/06/22/new-example-threadingexample/

		// how can we move data from thread to thread?
		// some components, like UI may not be movable.
		// will we need a proxy type then?
		// much like the tainted data concept.

		// can we make threads from service workers?

		// "C:\Program Files (x86)\SketchUp\SketchUp 2014\Tools\RubyStdLib\thread.rb"
		// X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\BCLImplementation.cs
		// tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs

		#region CurrentThread
		static __Thread InternalCurrentThreadValue;

		public static __Thread InternalCurrentThread
		{
			get
			{
				// Web Worker needs to be told which it is

				if (InternalCurrentThreadValue == null)
				{
					InternalCurrentThreadValue = new __Thread(default(ThreadStart));

					// service worker would be 0 ?
					if (Native.window != null)
						InternalCurrentThreadValue.ManagedThreadId = 1;


				}

				return InternalCurrentThreadValue;
			}
		}

		public static Thread CurrentThread
		{
			get
			{
				return (Thread)(object)InternalCurrentThread;
			}
		}
		#endregion



		[Obsolete("jsc updated for roslyn while yet? should be")]
		public static void Sleep(int ms)
		{
			// tested by?

			// unless jsc now has learned how to do a global async?
			// fake sleep. keep cpu busy!
			throw null;

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111
#if false
            Console.WriteLine("sleep " + new { ms, Thread.CurrentThread.ManagedThreadId });

            var e = new Stopwatch();
            e.Start();

            if (ms > 0)
                while (e.ElapsedMilliseconds < ms)
                {
                    Thread.Yield();
                }

            Console.WriteLine("end of sleep " + new { ms, e.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId });
#endif

		}


		public static bool Yield()
		{
			// how do we yield? :P

			return false;
		}

		public int ManagedThreadId { get; set; }

		// test for terminated threads?
		public bool IsAlive { get; set; }

		// all non ui threads
		public bool IsBackground { get; set; }

		#region ThreadStart
		public ThreadStart InternalMethod;

		public __Thread(ThreadStart t)
		{
			InternalMethod = t;
		}

		public void Start()
		{
			// WebWorker?
			// did jsc rewriter detect the threadstart correctly?
			IsAlive = true;
			InternalMethod();
			IsAlive = false;

		}
		#endregion

		#region ParameterizedThreadStart
		public ParameterizedThreadStart InternalParameterizedThreadStart;


		// tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs
		public __Thread(ParameterizedThreadStart t)
		{
			// it seems we should support scope sharing
			// yet we implemented it in Task.Run instead.
			// do we need to move it around?
			// check with java and actionscript

			InternalParameterizedThreadStart = t;
		}

		public void Start(object e)
		{
			// what about serviceworker?

			// WebWorker?
			// did jsc rewriter detect the threadstart correctly?

			IsAlive = true;
			InternalParameterizedThreadStart(e);
			IsAlive = false;
		}
		#endregion


        // Error	39	'ScriptCoreLib.JavaScript.BCLImplementation.System.__Thread.CurrentContext.get' must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.	X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs	179	42	ScriptCoreLib

		public ExecutionContext ExecutionContext { get; set;}

        public static Context CurrentContext { get; set; }
	}


}
