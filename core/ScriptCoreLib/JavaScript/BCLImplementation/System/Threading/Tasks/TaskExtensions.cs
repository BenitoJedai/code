using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
	// http://referencesource.microsoft.com/#System.Core/System/Threading/Tasks/TaskExtensions.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskExtensionsImpl.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System.Core/System.Threading.Tasks/TaskExtensions.cs

	[Script(Implements = typeof(global::System.Threading.Tasks.TaskExtensions))]
	public static class __TaskExtensions
	{
		// could this be part of .async instead?
		// called by?
		//   // ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks.__Task`1+<>c__DisplayClass1_0.<ContinueWith>b__0
		public static Task<TResult> Unwrap<TResult>(Task<Task<TResult>> task)
		{
			Console.WriteLine("enter TaskExtensions.Unwrap");

			// X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

			//async worker done0:3872ms 
			//0:3872ms Task ContinueWithResult
			//0:3873ms async worker running ? { xTask = [object Object] }
			//Uncaught TypeError: undefined is not a function 
			var x = new TaskCompletionSource<TResult>();

			task.ContinueWith(
				r =>
				{
					Console.WriteLine("enter TaskExtensions.Unwrap Task<Task<TResult>> ContinueWith");

					var xTask = r.Result;

					//var isTaskOfT = xTask is Task<object>;
					//Console.WriteLine("async worker running ? " + new { xTask, isTaskOfT });

					// are we in a wrong function?
					if (!(((object)xTask) is Task))
					{
						throw new Exception("bugcheck TaskExtensions.Unwrap Task<Task>");
					}

					xTask.ContinueWith(
						rr =>
						{
							x.SetResult(
								rr.Result
							);
						}
					);
				}
			);

			return x.Task;
		}


		public static Task Unwrap(Task<Task> task)
		{
			Console.WriteLine("enter TaskExtensions.Unwrap");

			// X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs
			// X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

			//async worker done0:3872ms 
			//0:3872ms Task ContinueWithResult
			//0:3873ms async worker running ? { xTask = [object Object] }
			//Uncaught TypeError: undefined is not a function 
			var x = new TaskCompletionSource<object>();

			task.ContinueWith(
				(Task<Task> r) =>
				{
					Console.WriteLine("enter TaskExtensions.Unwrap Task<Task> ContinueWith");


					var xTask = r.Result;

					// are we in a wrong function?
					if (!(((object)xTask) is Task))
					{
						throw new Exception("bugcheck TaskExtensions.Unwrap Task<Task>");
					}

					xTask.ContinueWith(
						rr =>
						{
							x.SetResult(
								new object()
							);
						}
					);
				}
			);

			return x.Task;
		}
	}
}
