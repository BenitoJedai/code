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

		private void ApplicationControl_Load(object sender, System.EventArgs e)
		{
			Console.WriteLine("enter ApplicationControl_Load " + new { Thread.CurrentThread.ManagedThreadId });

			var loc1 = new AsyncLocal<string>(
				valueChangedHandler: value =>
				{
					Console.WriteLine(new { value.ThreadContextChanged, value.CurrentValue, value.PreviousValue, Thread.CurrentThread.ManagedThreadId });
				}
			);

			loc1.Value = "hello from UI";

			Task.Run(
				delegate
				{
					Console.WriteLine("enter worker " + new { loc1, Thread.CurrentThread.ManagedThreadId });

					loc1.Value = "hello from worker / " + new { loc1.Value };

				}
			);

			//enter ApplicationControl_Load { ManagedThreadId = 3 }
			//{ ThreadContextChanged = False, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 3 }
			//ApplicationForm.Load
			//{ ThreadContextChanged = True, CurrentValue = hello from UI, PreviousValue = , ManagedThreadId = 4 }
			//enter worker { loc1 = System.Threading.AsyncLocal`1[System.String], ManagedThreadId = 4 }
			//{ ThreadContextChanged = False, CurrentValue = hello from worker / { Value = hello from UI }, PreviousValue = hello from UI, ManagedThreadId = 4 }
			//{ ThreadContextChanged = True, CurrentValue = , PreviousValue = hello from worker / { Value = hello from UI }, ManagedThreadId = 4 }








		}
	}
}
