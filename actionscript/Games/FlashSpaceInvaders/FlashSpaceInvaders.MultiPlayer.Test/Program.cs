using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace FlashSpaceInvaders.MultiPlayer.Test
{
	class Program
	{
		
		static void Main(string[] args)
		{
			
			var swf = @"..\..\..\FlashSpaceInvaders\bin\debug\web\FlashSpaceInvaders.swf";

			#region launcher
			ThreadPool.QueueUserWorkItem(
				delegate
				{
					Thread.Sleep(1000);

					var x = new Launcher();

					var app = new ApplicationContext(x);

					Action<Action> Invoke =
						h => x.Invoke(h);

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
									p.Kill();

									//p.CloseMainWindow();
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


					Application.Run(app);


				}
			);
			#endregion

			Nonoba.DevelopmentServer.Server.StartWithDebugging();
		}
	}
}
