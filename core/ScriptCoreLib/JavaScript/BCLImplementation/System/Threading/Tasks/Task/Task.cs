using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
	// with 4.6!
	// http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Task.cs
	// this seems out of sync for 4.6?
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Tasks/Task.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/Task.cs
	// https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/threading/Tasks/Future.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Threading/Tasks/Task.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Threading/Tasks/Task.cs
	// http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx

	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs

	// Task<TResult> (aka. promise).

	[Script(Implements = typeof(global::System.Threading.Tasks.Task))]
	public partial class __Task
	{
		// http://blogs.msdn.com/b/flaviencharlon/archive/2012/08/06/task-lt-t-gt-vs-iobservable-lt-t-gt-when-to-use-what.aspx

		// http://blogs.msdn.com/b/vcblog/archive/2014/11/12/resumable-functions-in-c.aspx
		// X:\jsc.svn\examples\javascript\Test\Test453NamedParameter\Test453NamedParameter\Class1.cs


		public override string ToString()
		{
			return new { IsCompleted }.ToString();
		}

		public Action InternalDispose;
		public void Dispose()
		{
			if (InternalDispose != null)
				InternalDispose();

		}




		//script: error JSC1000: No implementation found for this native method, please implement [static System.Threading.Tasks.Task.Run(System.F

		[Obsolete("jsc would have to write all application into a global async")]
		public void Wait()
		{

			throw new NotImplementedException();
		}




		// http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
		// !supported in: 4.5
		public __TaskAwaiter GetAwaiter()
		{
			//Console.WriteLine("__Task.GetAwaiter");

			// see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

			var awaiter = new __TaskAwaiter
			{
				InternalIsCompleted = () => this.IsCompleted,
			};

			this.InternalYield += delegate
			{
				//Console.WriteLine("__Task.GetAwaiter InternalYield");

				if (awaiter.InternalOnCompleted != null)
					awaiter.InternalOnCompleted();
			};

			return awaiter;
		}

		public bool IsCompleted { get; internal set; }




		#region Factory
		public static TaskFactory InternalFactory
		{
			get
			{
				return new __TaskFactory();
			}
		}


		public static TaskFactory Factory
		{
			get
			{
				return InternalFactory;
			}
		}
		#endregion



		public __Task()
		{

		}

		public __Task(Action action)
		{
			this.InternalStart = delegate
			{
				// worker?
				action();
				// OnComplete
			};
		}

		public Action InternalStart;



		public void InternalOnCompleted(Action continuation)
		{
			//Console.WriteLine("__Task<TResult>.InternalOnCompleted " + new { IsCompleted });
			if (IsCompleted)
			{
				continuation();
				return;
			}

			InternalYield += continuation;
		}


		public Action InternalYield;

		public void Start()
		{
			//Console.WriteLine("__Task.Start");

			if (InternalStart == null)
				throw new InvalidOperationException("Start may not be called on a continuation task.");


			InternalStart();
		}




		public void InternalSetCompleteAndYield()
		{
			this.IsCompleted = true;

			if (this.InternalYield != null)
				this.InternalYield();
		}

		public static implicit operator Task(__Task e)
		{
			return (Task)(object)e;
		}

		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\YieldAwaitable.cs

		public static implicit operator __Task(Task e)
		{
			return (__Task)(object)e;
		}
	}




	// until we support generic type info
	[Script]
	internal delegate object FuncOfObjectToObject(object e);

	// Func<Task<TResult>, object, TNewResult>
	// Func<Task<TResult>, TNewResult>
	[Script]
	internal delegate object FuncOfTaskToObject(Task task);


	// tested by?
	[Script]
	internal delegate object FuncOfTaskOfObjectArrayToObject(Task<object>[] task);



	// http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Future.cs
	[Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
	internal partial class __Task<TResult> : __Task,
		//SUPPORT_IOBSERVABLE
		IObservable<TResult>
	{
		public override string ToString()
		{
			return new { IsCompleted, this.Result }.ToString();
		}

		// see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx


		public __TaskAwaiter<TResult> GetAwaiter()
		{
			// see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

			var awaiter = new __TaskAwaiter<TResult>
			{
				InternalIsCompleted = () => this.IsCompleted,
				InternalGetResult = () => this.Result
			};

			this.InternalYield += delegate
			{
				if (awaiter.InternalOnCompleted != null)
					awaiter.InternalOnCompleted();
			};

			return awaiter;
		}


		public static TaskFactory<TResult> Factory
		{
			get
			{
				return new TaskFactory<TResult>();
			}
		}



		public TResult Result { get; internal set; }


		public void InternalSetCompleteAndYield(TResult value)
		{

			// or throw?
			if (IsCompleted)
				return;

			// http://stackoverflow.com/questions/12100022/taskcompletionsource-when-to-use-setresult-versus-trysetresult-etc

			//Console.WriteLine("__Task<TResult> InternalSetCompleteAndYield");

			this.Result = value;

			this.InternalSetCompleteAndYield();
		}

		public IDisposable Subscribe(global::System.IObserver<TResult> observer)
		{
			// tested by?
			throw new NotImplementedException();
		}

		public static implicit operator Task<TResult>(__Task<TResult> e)
		{
			return (Task<TResult>)(object)e;
		}

		public static implicit operator __Task<TResult>(Task<TResult> e)
		{
			return (__Task<TResult>)(object)e;
		}
	}
}
