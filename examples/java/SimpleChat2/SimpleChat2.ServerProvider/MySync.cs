using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SimpleChat.Library;

namespace SimpleChat2.ServerProvider
{
	public partial class MySync : Component
	{
		public readonly SynchronizedActionQueue Queue = new SynchronizedActionQueue();
 
		public MySync()
		{
			InitializeComponent();
		}

		public MySync(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.Queue.Invoke();
		}
	}
}
