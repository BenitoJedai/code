using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleChat
{
	public partial class RemoteUsers : UserControl
	{
		public RemoteUsers()
		{
			InitializeComponent();
		}

		int InternalMessageHeight;

		public int MessageHeight
		{
			get
			{
				return InternalMessageHeight;
			}
			set
			{
				InternalMessageHeight = value;
				Update2();
			}
		}

		private void RemoteUsers_SizeChanged(object sender, EventArgs e)
		{
			Update2();
		}

		private void Update2()
		{
			var Width = this.Size.Width;
			var Height = this.Size.Height;

			var p = new Point(0, MessageHeight);
			var s = new Size(Width, Height - MessageHeight);

			this.treeView1.Location = p;
			this.treeView1.Size = s;

			
		}

		public string Message
		{
			get
			{
				return this.label1.Text;
			}
			set
			{
				this.label1.Text = value;
			}
		}
	}
}
