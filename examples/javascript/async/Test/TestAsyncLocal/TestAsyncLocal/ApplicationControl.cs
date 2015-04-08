using TestAsyncLocal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace TestAsyncLocal
{
	public partial class ApplicationControl : UserControl
	{
		public ApplicationControl()
		{
			this.InitializeComponent();
		}

		// will events do thread jumps?
		public event Action<string> SpecialEvent;

		private void ApplicationControl_Load(object sender, System.EventArgs e)
		{
			Console.WriteLine("enter ApplicationControl_Load " + new { Thread.CurrentThread.ManagedThreadId });

			this.SpecialEvent += value =>
			{
				Console.WriteLine("SpecialEvent " + new { value, Thread.CurrentThread.ManagedThreadId });
			};

			IProgress<string> progress = new Progress<string>(
				handler: value =>
				{
					Console.WriteLine("Progress " + new { value, Thread.CurrentThread.ManagedThreadId });

				}
			);


			progress.Report("hello from UI");

			var loc1 = new AsyncLocal<string>(
				// would we be able to send the delegate over to worker?

				// called by ExecutionContext

				//>	TestAsyncLocal.exe!TestAsyncLocal.ApplicationControl.ApplicationControl_Load.AnonymousMethod__1_1(System.Threading.AsyncLocalValueChangedArgs<string> value) Line 40	C#
				// 	mscorlib.dll!System.Threading.AsyncLocal<T>.System.Threading.IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)	Unknown
				// 	mscorlib.dll!System.Threading.ExecutionContext.OnAsyncLocalContextChanged(System.Threading.ExecutionContext previous, System.Threading.ExecutionContext current)	Unknown
				// 	mscorlib.dll!System.Threading.ExecutionContext.SetExecutionContext(System.Threading.ExecutionContext executionContext, bool preserveSyncCtx)	Unknown
				// 	mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)	Unknown
				// 	mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)	Unknown
				// 	mscorlib.dll!System.Threading.Tasks.Task.ExecuteWithThreadLocal(System.Threading.Tasks.Task currentTaskSlot)	Unknown
				// 	mscorlib.dll!System.Threading.Tasks.Task.ExecuteEntry(bool bPreventDoubleExecution)	Unknown
				// 	mscorlib.dll!System.Threading.Tasks.Task.System.Threading.IThreadPoolWorkItem.ExecuteWorkItem()	Unknown
				// 	mscorlib.dll!System.Threading.ThreadPoolWorkQueue.Dispatch()	Unknown
				// 	mscorlib.dll!System.Threading._ThreadPoolWaitCallback.PerformWaitCallback()	Unknown


				valueChangedHandler: value =>
				{
					Console.WriteLine("AsyncLocal " + new { value.ThreadContextChanged, value.CurrentValue, value.PreviousValue, Thread.CurrentThread.ManagedThreadId });
				}
			);

			loc1.Value = "hello from UI";

			var s = new SemaphoreSlim(1);

			this.SpecialEvent("hello from UI");

			Task.Run(
				delegate
				{
					Console.WriteLine("enter worker " + new { loc1, progress, Thread.CurrentThread.ManagedThreadId });

					//this.InvokeRequired
					this.SpecialEvent("hello from UI " + new { this.InvokeRequired });

					progress.Report("hello from worker");

					s.Release();

					loc1.Value = "hello from worker / " + new { loc1.Value };

				}
			);

			//enter ApplicationControl_Load { ManagedThreadId = 3 }
			//AsyncLocal { ThreadContextChanged = False, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 3 }
			//SpecialEvent { value = hello from UI, ManagedThreadId = 3 }
			//ApplicationForm.Load
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 5 }
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 4 }
			//SemaphoreSlim WaitAsync { CurrentCount = 0, ManagedThreadId = 5 }
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = , PreviousValue = hello from UI, ManagedThreadId = 5 }
			//enter worker { loc1 = System.Threading.AsyncLocal`1[System.String], progress = System.Progress`1[System.String], ManagedThreadId = 4 }
			//SpecialEvent { value = hello from UI { InvokeRequired = True }, ManagedThreadId = 4 }
			//AsyncLocal { ThreadContextChanged = False, CurrentValue = hello from worker / { Value = hello from UI }, PreviousValue = hello from UI, ManagedThreadId = 4 }
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = , PreviousValue = hello from worker / { Value = hello from UI }, ManagedThreadId = 4 }
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = , PreviousValue = hello from UI, ManagedThreadId = 3 }
			//Progress { value = hello from UI, ManagedThreadId = 3 }
			//AsyncLocal { ThreadContextChanged = True, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 3 }
			//Progress { value = hello from worker, ManagedThreadId = 3 }

			s.WaitAsync().ContinueWith(
				t =>
				{
					Console.WriteLine("SemaphoreSlim WaitAsync " + new { s.CurrentCount, Thread.CurrentThread.ManagedThreadId });

				}
			);




		}
	}
}
