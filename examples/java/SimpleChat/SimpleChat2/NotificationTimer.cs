using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleChat2
{
	public partial class NotificationTimer : Component
	{
		public Control Target { get; set; }

		public int Interval
		{
			get
			{
				return timer1.Interval;
			}
			set
			{
				timer1.Interval = value;
			}
		}

		public bool TimerEnabled { get; set; }

		public string Text
		{
			get
			{
				if (Target == null)
					return "";


				return Target.Text;
			}
			set
			{
				if (Target == null)
					return;

				Console.WriteLine(value);
				Target.Text = value;

				if (TimerEnabled)
				{
					Target.Show();
					timer1.Start();
				}
			}
		}

		public NotificationTimer()
		{
			InitializeComponent();
		}

		public NotificationTimer(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			Target.Hide();
		}
	}
}
