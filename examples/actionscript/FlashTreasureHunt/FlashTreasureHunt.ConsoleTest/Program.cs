using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FlashTreasureHunt.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var swf = @"..\..\..\bin\Game.swf";

			#region launcher
			ThreadPool.QueueUserWorkItem(
				delegate
				{
					Thread.Sleep(1000);

					var x = new Launcher();

					var app = new ApplicationContext(x);

					Action<Action> Invoke =
						h =>
						{
							if (x.IsDisposed)
								return;
							x.Invoke(h);
						};


					x.button1.Enabled = File.Exists(swf);

					x.button1.Click +=
						delegate
						{

							var p = Process.Start(swf);

							x.checkedListBox1.Items.Add(p.Id);

							p.EnableRaisingEvents = true;


							p.Exited +=
								delegate
								{
									Invoke(
										delegate
										{
											x.checkedListBox1.Items.Remove(p.Id);
										}
									);
								};

							x.FormClosing +=
								delegate
								{
									if (p.HasExited)
										return;

									p.Kill();

									//p.CloseMainWindow();
								};

						};

					x.FormClosed +=
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


					Application.Run(app);


				}
			);
			#endregion

			Nonoba.DevelopmentServer.Server.StartWithDebugging();
		}
	}
}
