using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SwingForms.Library;
using System.Runtime.InteropServices;
using javax.swing;
using System.Windows.Forms;

namespace SwingForms
{
	public partial class Program
	{


		public static void Main(string[] args)
		{
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

				f.Controls.Add(b1);

				Application.Run(f);

				//f.Show();

				Console.ReadLine();

				f.Dispose();
			}



		}


	}
}
