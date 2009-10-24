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

		private void RemoteUsers_SizeChanged(object sender, EventArgs e)
		{
			var Width = this.Size.Width;
			var Height = this.Size.Height;

			var p = new Point(0, 48);
			var s = new Size(Width, Height - 48);

			this.treeView1.Location = p;
			this.treeView1.Size = s;
		}
	}
}
