using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FlashConsoleWorm.Test
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var swf = @"..\..\..\FlashConsoleWorm\bin\debug\web\Client.swf";

			#region launcher
			ThreadPool.QueueUserWorkItem(
				delegate
				{
					Thread.Sleep(1000);

					var x = new Launcher();


					x.button1.Click +=
						delegate
						{

							var p = Process.Start(swf);

							x.checkedListBox1.Items.Add(p.Id);

							x.FormClosing +=
								delegate
								{
									p.CloseMainWindow();
								};

						};

					x.FormClosing +=
						delegate
						{
							foreach (Form f in Application.OpenForms)
							{
								if (f == x)
									continue;

								var k = f;

								f.Invoke(
									new Action(
											k.Close
									)
								);
							}
						};


					Application.Run(new ApplicationContext(x));


				}
			);
			#endregion

			Nonoba.DevelopmentServer.Server.StartWithDebugging();
		}
	}
}
