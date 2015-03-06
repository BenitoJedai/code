using chrome;
using ChromeCSVFileHandler;
using ChromeCSVFileHandler.Design;
using ChromeCSVFileHandler.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeCSVFileHandler
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application
	{
		public readonly ApplicationWebService service = new ApplicationWebService();


		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			//'title' is not a recognized file handler property.
			// https://developer.chrome.com/apps/manifest/file_handlers
			// need to keep it?

			// X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				chrome.Notification.DefaultTitle = "ChromeCSVFileHandler";

				#region __Form
				{
					var windows = new List<AppWindow>();


					__Form.InternalHTMLTargetAttachToDocument =
					   async (that, yield) =>
					   {

						   //Error in event handler for app.runtime.onLaunched: Error: Invalid value for argument 2. Property 'transparentBackground': Expected 'boolean' but got 'integer'.
						   var transparentBackground = true;


						   // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/common/extensions/api/app_window.idl
						   var xappwindow = await chrome.app.window.create(
								 Native.document.location.pathname,
								 new
								 {
									 frame = "none"
									 //,transparentBackground
								 }
							);


						   // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

						   Console.WriteLine("appwindow loading... " + new { xappwindow });
						   Console.WriteLine("appwindow loading... " + new { xappwindow.contentWindow });

						   // our window frame non client area plus inner body margin
						   //xappwindow.resizeTo(
						   // DefaultWidth + 32,
						   // DefaultHeight + 64
						   //);

						   xappwindow.With(
							   appwindow =>
							   {

								   #region onload
								   Action<IEvent> onload =

										delegate
										{
											var c = that;
											var f = (Form)that;
											var ff = c;

											windows.Add(appwindow);

											// http://sandipchitale.blogspot.com/2013/03/tip-webkit-app-region-css-property.html

											(ff.CaptionForeground.style as dynamic).webkitAppRegion = "drag";

											//(ff.ResizeGripElement.style as dynamic).webkitAppRegion = "drag";
											// cant have it yet
											ff.ResizeGripElement.Orphanize();

											f.StartPosition = FormStartPosition.Manual;


											f.Left = 0;
											f.Top = 0;


											f.FormClosing +=
												delegate
												{
													Console.WriteLine("FormClosing");
													appwindow.close();
												};

											appwindow.onRestored.addListener(
												new Action(
													delegate
													{
														that.CaptionShadow.Hide();

													}
												)
											);

											appwindow.onMaximized.addListener(
											new Action(
													delegate
													{
														that.CaptionShadow.Show();

													}
											)
											);

											appwindow.onClosed.addListener(
												new Action(
													delegate
													{
														Console.WriteLine("onClosed");
														windows.Remove(appwindow);

														f.Close();
													}
											)
											);

											// wont fire yet
											//appwindow.contentWindow.onbeforeunload +=
											//    delegate
											//    {
											//        Console.WriteLine("onbeforeunload");
											//    };

											//appwindow.onBoundsChanged.addListener(
											//        new Action(
											//        delegate
											//        {
											//            Console.WriteLine("appwindow.onBoundsChanged");

											//            f.SizeTo(
											//                appwindow.contentWindow.Width,
											//                appwindow.contentWindow.Height
											//            );
											//        }
											//    )
											//);


											appwindow.contentWindow.onresize +=
													//appwindow.onBoundsChanged.addListener(
													//    new Action(
													delegate
													{

														Console.WriteLine("appwindow.contentWindow.onresize SizeTo " +
															new
															{
																appwindow.contentWindow.Width,
																appwindow.contentWindow.Height
															}
															);

														f.Width = appwindow.contentWindow.Width;
														f.Height = appwindow.contentWindow.Height;

													}
											//)
											//)
											;

											f.Width = appwindow.contentWindow.Width;
											f.Height = appwindow.contentWindow.Height;


											//Console.WriteLine("appwindow contentWindow onload");


											that.HTMLTarget.AttachTo(
												appwindow.contentWindow.document.body
											);



											yield(false);
											//Console.WriteLine("appwindow contentWindow onload done");
										};
								   #endregion

								   //Uncaught TypeError: Cannot read property 'contentWindow' of undefined 



								   appwindow.contentWindow.onload +=
									   onload;
							   }
						   );





					   };


				}
				#endregion

				// http://developer.chrome.com/apps/manifest/file_handlers.html
				// https://code.google.com/p/chromium/issues/detail?id=192536
				chrome.app.runtime.Launched +=
					  async e =>
					  {
						  var n = new chrome.Notification
						  {
							  Message = "Launched " + new { e.id, e.items },
						  };

						  if (e.items != null)
						  {
							  var x = e.items[0];


							  n.Message = "Launched " + new
							  {
								  x.type,
								  x.entry.name,
								  x.entry.isFile,
							  };

							  Action<FileEntry> read = async entry =>
							  {
								  Console.WriteLine("before file");
								  var f = await entry.file();
								  Console.WriteLine("after file");


								  Console.WriteLine("before readAsText");
								  var result = await f.readAsText();
								  Console.WriteLine("after readAsText");

								  Console.WriteLine(new { result });


								  //                                  result = "Column 1","Column 2",
								  //"#0","John Doe, Canada | 600 USD",

								  var data = new DataTable();

								  var re = new StringReader(result);


								  var Headers = re.ReadLine();


								  ("\"," + Headers + ",\"").Split(new[] { "\",\"" }, StringSplitOptions.None).WithEach(
								  ColumnName =>
								  {
									  data.Columns.Add(
										  new DataColumn { ColumnName = ColumnName }
									  );
								  }
							  );

								  var Line = re.ReadLine();

								  while (Line != null)
								  {
									  var Row = data.NewRow();

									  ("\"," + Line + ",\"").Split(new[] { "\",\"" }, StringSplitOptions.None).WithEachIndex(
										  (value, index) =>
										  {
											  if (index < data.Columns.Count)
												  Row[index] = value;
										  }
									 );

									  data.Rows.Add(Row);




									  Line = re.ReadLine();
								  }

								  //content

								  var content = new ApplicationControl();

								  content.dataGridView1.DataSource = data;

								  var cf = new Form();

								  content.Dock = DockStyle.Fill;
								  cf.Controls.Add(content);

								  cf.Show();
							  };

							  read(x.entry);


							  // faki dudli doo. it works after a few hours.


							  //                                        { result = "Column 1","Column 2",
							  //"#0","John Doe, Canada | 600 USD",
							  //"#1","John Doe, Canada | 600 USD",
							  //"#2","John Doe, Canada | 600 USD",
							  //"#3","John Doe, Canada | 600 USD",
							  //"#4","John Doe, Canada | 600 USD",
							  //"#5","John Doe, Canada | 600 USD",
							  //"#6","John Doe, Canada | 600 USD",
							  //"#7","John Doe, Canada | 600 USD",
							  //"#8","John Doe, Canada | 600 USD",
							  //"#9","John Doe, Canada | 600 USD",
							  //"#10","John Doe, Canada | 600 USD",
							  //"#11","John Doe, Canada | 600 USD",
							  //"#12","John Doe, Canada | 600 USD",
							  //"#13","John Doe, Canada | 600 USD",
							  //"#14","John Doe, Canada | 600 USD",
							  //"#15","John Doe, Canada | 600 USD",
							  //"#16","John Doe, Canada | 600 USD",
							  //"#17","John Doe, Canada | 600 USD",
							  //"#18","John Doe, Canada | 600 USD",
							  //"#19","John Doe, Canada | 600 USD",
							  //"#20","John Doe, Canada | 600 USD",
							  //"#21","John Doe, Canada | 600 USD",
							  //"#22","John Doe, Canada | 600 USD",
							  //"#23","John Doe, Canada | 600 USD",
							  //"#24","John Doe, Canada | 600 USD",
							  //"#25","John Doe, Canada | 600 USD",
							  //"#26","John Doe, Canada | 600 USD",
							  //"#27","John Doe, Canada | 600 USD",
							  //"#28","John Doe, Canada | 600 USD",
							  //"#29","John Doe, Canada | 600 USD",
							  //"#30","John Doe, Canada | 600 USD",
							  //"#31","John Doe, Canada | 600 USD",
							  // } 







							  //Expando.Of(file).GetMemberNames().WithEach(
							  //    FileEntryMember =>
							  //        Console.WriteLine(new { FileEntryMember })
							  //    );


						  }
						  // http://developer.chrome.com/apps/first_app.html#open
						  // http://stackoverflow.com/questions/19227472/how-to-open-a-chrome-packaged-app-with-a-parameter-on-windows/19446501#19446501


					  };

				return;
			}




		}

	}
}
