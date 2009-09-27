using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SwingForms.Library;
using System.Runtime.InteropServices;
using javax.swing;
using System.Windows.Forms;
using System.Drawing;

namespace SwingForms
{
	public partial class Program
	{


		public static void Main(string[] args)
		{
			var f = new Form
			{
				Text = "hi!"
			};

			f.FormClosing +=
				delegate
				{
					Console.WriteLine("FormClosing");
				};

			f.FormClosed +=
				delegate
				{
					Console.WriteLine("FormClosed");
				};

			var b1 = new Button { Text = "hello world 3434534534" };

			b1.Location = new Point(16, 16);
			b1.Size = new Size(200, 32);

			b1.Click += delegate
			{
				b1.Text = "hi";

				new MyMessageBox().ShowDialog();
				//var f2 = new Form { Text = "Message1" };

				//f2.ShowDialog();
			};

			f.Controls.Add(b1);

			Application.Run(f);


			Console.WriteLine("done!");



		}


	}
}
