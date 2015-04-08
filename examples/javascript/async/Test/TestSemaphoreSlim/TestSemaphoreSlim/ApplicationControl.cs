using TestSemaphoreSlim;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace TestSemaphoreSlim
{
	public partial class ApplicationControl : UserControl
	{
		public ApplicationControl()
		{
			this.InitializeComponent();
		}

		private void ApplicationControl_Load(object sender, System.EventArgs e)
		{



			Console.WriteLine("will spin up worker 1 and await for data");



			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150408
			var sInput = new SemaphoreSlim(0);
			var sOutput = new SemaphoreSlim(0);

			Task.Run(
				async delegate
				{
					Console.WriteLine("worker activated " + new { sInput, Thread.CurrentThread.ManagedThreadId });

					next:
					await sInput.WaitAsync();

					Console.WriteLine("work complete " + new { Thread.CurrentThread.ManagedThreadId });


					sOutput.Release();
					// both threads continue working... unlike in a hop where only one logical thread continues..
					goto next;
				}
			);

			this.Click += async delegate
			{
				Console.WriteLine("will send data to the thread...");

				// signal the other thread
				sInput.Release();


				await sOutput.WaitAsync();

				Console.WriteLine("ui work complete...");
			};
		}
	}
}

//---------------------------
//Microsoft Visual Studio
//---------------------------
//The following breakpoint cannot be set:

//At ApplicationControl.cs, line 42 character 6 ('TestSemaphoreSlim.ApplicationControl.ApplicationControl_Load(object sender, System.EventArgs e)')

//The breakpoint failed to bind.
//---------------------------
//OK   
//---------------------------
