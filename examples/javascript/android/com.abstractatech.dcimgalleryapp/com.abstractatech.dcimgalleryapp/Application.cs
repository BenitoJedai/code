using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.dcimgalleryapp.Design;
using com.abstractatech.dcimgalleryapp.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using DiagnosticsConsole;
using ScriptCoreLib.Ultra.WebService;

namespace com.abstractatech.dcimgalleryapp
{
	using ystring = Action<string>;
	using System.Threading.Tasks;



	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		//com.abstractatech.dcimgalleryapp.Assets.Publish ref0;

		static Application()
		{
			// X:\jsc.svn\examples\javascript\Test\TestChromeStackFrames\TestChromeStackFrames\Application.cs

			//I/chromium(30870): [INFO:CONSOLE(50800)] "0ms NewInstanceConstructor restore fields..", source: http://192.168.1.228:13883/view-source (50800)
			//I/chromium(30870): [INFO:CONSOLE(51092)] "Uncaught Error: InvalidOperationException", source: http://192.168.1.228:13883/view-source (51092)
			// android floa faults?


			//		I/chromium(32085): [INFO:CONSOLE(50800)] "18ms {{ message = Uncaught Error: InvalidOperationException, error = Error: InvalidOperationException, stack = Error: InvalidOperationException
			//I/chromium(32085):     at kAsABqEShzuSUuAZjYIdtQ (http://192.168.1.228:26501/view-source:34623:55)
			//I/chromium(32085):     at vw0ABrkV9zepFYr_a8mjvTg (http://192.168.1.228:26501/view-source:34678:9)
			//I/chromium(32085):     at tx8ABsg9tT_aQKItT0_aEVWQ (http://192.168.1.228:26501/view-source:51092:13)
			//I/chromium(32085):     at BgYABpy_aQje8vXLgiLNaLw (http://192.168.1.228:26501/view-source:16001:79)
			//I/chromium(32085):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.type$cnqFo9QlKTGMCod2TxSFYg.BQAABtQlKTGMCod2TxSFYg (http://192.168.1.228:26501/view-source:72971:41)
			//I/chromium(32085):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.type$X_a1ZMtTPtzu_ahhJMj_bfMGQ.AwAABtTPtzu_ahhJMj_bfMGQ (http://192.168.1.228:26501/view-source:73043:10)
			//I/chromium(32085):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.$ctor$.f (http://192.168.1.228:26501/view-source:31:23)
			//I/chromium(32085):     at EgEABoPJXDeM0OAvrVUGow (http://192.168.1.228:26501/view-source:77680:9)
			//I/chromium(32085):     at http://192.168.1.228:26501/view-source:7654:84 }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "23ms {{ frameIndex = 1, location_line = 34623, displayName = ScriptCoreLib.JavaScript.BCLImplementation.System.__Exception.InternalConstructor }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "25ms {{ frameIndex = 2, location_line = 34678, displayName = ScriptCoreLib.JavaScript.BCLImplementation.System.__InvalidOperationException.InternalConstructor }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "26ms {{ frameIndex = 3, location_line = 51092, displayName = ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32.Parse }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "34ms {{ frameIndex = 4, location_line = 16001, displayName = null }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "36ms {{ frameIndex = 5, location_line = 72971, displayName = com.abstractatech.dcimgalleryapp.ApplicationWebService..ctor }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "37ms {{ frameIndex = 6, location_line = 73043, displayName = com.abstractatech.dcimgalleryapp.Application..ctor }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "38ms {{ frameIndex = 7, location_line = 31, displayName = X_a1ZMtTPtzu_ahhJMj_bfMGQ.$ctor$.f }}", source: http://192.168.1.228:26501/view-source (50800)
			//I/chromium(32085): [INFO:CONSOLE(50800)] "39ms {{ frameIndex = 8, location_line = 77680, displayName = com.abstractatech.dcimgalleryapp.ApplicationBootstrap.<.cctor>b__0 }}", source: http://192.168.1.228:26501/view-source (50800)

			Action<string> InspectStackTrace =
				StackTrace =>
				{
					var StackTraceLines = StackTrace.Split(new[] { "\n" }, StringSplitOptions.None);

					for (int frameIndex = 1; frameIndex < StackTraceLines.Length; frameIndex++)
					{
						//Console.WriteLine(new { frameIndex });
						//at _0gAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:75912:5)
						//at Aq_afTERoTzCRSXcBCi_akMQ.type$Aq_afTERoTzCRSXcBCi_akMQ.qgAABkRoTzCRSXcBCi_akMQ(https://192.168.43.252:13078/view-source:75171:32)

						var StackTraceLine = StackTraceLines[frameIndex];

						var ExternalTarget = StackTraceLine.TakeUntilOrEmpty(" (").SkipUntilOrEmpty("at ");

						// nameless caller?
						//Console.WriteLine(new { frameIndex, ExternalTarget });


						var locationURI_line_column = StackTraceLine.SkipUntilOrEmpty(" (").TakeUntilOrEmpty(")");


						//                { { at_function = ggAABovarz_arNO9UfOoNzA , locationURI_line_column = https://192.168.43.252:2906/view-source:74473:5 }}
						//                    at_function = DstVs7OHoT6VDScV62l1nQ.type$DstVs7OHoT6VDScV62l1nQ.fgAABrOHoT6VDScV62l1nQ , locationURI_line_column = https://192.168.43.252:2906/view-source:74413:30 }}


						var locationURI = locationURI_line_column.TakeUntilLastOrEmpty(":").TakeUntilLastOrEmpty(":");

						var location_line = int.Parse(locationURI_line_column.TakeUntilLastOrEmpty(":").SkipUntilLastOrEmpty(":"));
						var location_column = locationURI_line_column.SkipUntilLastOrEmpty(":");

						//var f = IFunction.ByName(ExternalTarget);

						var displayName = ExternalTarget;

						if (!string.IsNullOrEmpty(ExternalTarget))
						{

							var f = IFunction.Of(Native.self, ExternalTarget);
							if (f != null)
							{
								displayName = f.displayName;
							}

							// DstVs7OHoT6VDScV62l1nQ.TypeName = "___ctor_b__2_d__0";
							// for types we should also start using displayName?
							//   var type$DstVs7OHoT6VDScV62l1nQ = DstVs7OHoT6VDScV62l1nQ.prototype;

							// TestChromeStackFrames.Application+<>c__DisplayClass1+<<_ctor>b__2>d__0.MoveNext
							//type$DstVs7OHoT6VDScV62l1nQ.fgAABrOHoT6VDScV62l1nQ = function()

							var type_constructor = ExternalTarget.TakeUntilOrNull(".");
							var type_prototype = ExternalTarget.SkipUntilOrEmpty(".").TakeUntilOrEmpty(".");
							var type_prototype_method = ExternalTarget.SkipUntilOrEmpty(".").SkipUntilOrEmpty(".");

							if (type_constructor != null)
							{
								var __constructor = IFunction.Of(Native.self, type_constructor);
								var __method = IFunction.Of(__constructor.prototype, type_prototype_method);

								//__constructor.prototype[]

								if (__method != null)
								{
									displayName = __method.displayName;
								}
							}

						}

						Console.WriteLine(new { frameIndex, location_line, displayName });


						//new IHTMLPre { displayName }.AttachToDocument().title = new
						//{
						//	locationURI,
						//	location_line,
						//	location_column,
						//	ExternalTarget,
						//	type_constructor,
						//	type_prototype,
						//	type_prototype_method
						//}.ToString();


						//var source = await new WebClient().DownloadStringTaskAsync(locationURI);

						//var sourceLine = source.ToLines()[location_line - 1];

						//new IHTMLPre { sourceLine }.AttachToDocument();
					}
				};

			// // ScriptCoreLib.JavaScript.BCLImplementation.System.__Int32.Parse
			Native.window.onerror +=
				e =>
				{
					Console.WriteLine(new
					{
						e.message,
						e.error,
						e.error.stack,
					}
					);

					InspectStackTrace(
						(string)e.error.stack
					);

					//I/chromium(31271): [INFO:CONSOLE(50800)] "0ms NewInstanceConstructor restore fields..", source: http://192.168.1.228:16928/view-source (50800)
					//I/chromium(31271): [INFO:CONSOLE(50800)] "8ms {{ message = Uncaught Error: InvalidOperationException, error = Error: InvalidOperationException }}", source: http://192.168.1.228:16928/view-source (50800)
					//W/JsDialogHelper(31271): Cannot create a dialog, the WebView context is not an Activity
					//I/chromium(31271): [INFO:CONSOLE(51092)] "Uncaught Error: InvalidOperationException", source: http://192.168.1.228:16928/view-source (51092)

					//Native.window.alert(new { e.message, e.error });

					//I/chromium(31583): [INFO:CONSOLE(50800)] "0ms NewInstanceConstructor restore fields..", source: http://192.168.1.228:12531/view-source (50800)
					//I/chromium(31583): [INFO:CONSOLE(50800)] "16ms {{ message = Uncaught Error: InvalidOperationException, error = Error: InvalidOperationException, stack = 
					// Error: InvalidOperationException
					//I/chromium(31583):     at kAsABqEShzuSUuAZjYIdtQ (http://192.168.1.228:12531/view-source:34623:55)
					//I/chromium(31583):     at vw0ABrkV9zepFYr_a8mjvTg (http://192.168.1.228:12531/view-source:34678:9)
					//I/chromium(31583):     at tx8ABsg9tT_aQKItT0_aEVWQ (http://192.168.1.228:12531/view-source:51092:13)
					//I/chromium(31583):     at BgYABpy_aQje8vXLgiLNaLw (http://192.168.1.228:12531/view-source:16001:79)
					//I/chromium(31583):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.type$cnqFo9QlKTGMCod2TxSFYg.BQAABtQlKTGMCod2TxSFYg (http://192.168.1.228:12531/view-source:72971:41)
					//I/chromium(31583):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.type$X_a1ZMtTPtzu_ahhJMj_bfMGQ.AwAABtTPtzu_ahhJMj_bfMGQ (http://192.168.1.228:12531/view-source:73043:10)
					//I/chromium(31583):     at X_a1ZMtTPtzu_ahhJMj_bfMGQ.$ctor$.f (http://192.168.1.228:12531/view-source:31:23)
					//I/chromium(31583):     at CQEABoPJXDeM0OAvrVUGow (http://192.168.1.228:12531/view-source:77516:9)
					//I/chromium(31583):     at http://192.168.1.228:12531/view-source:7654:84 }}", source: http://192.168.1.228:12531/view-source (50800)


					e.preventDefault();
				};

		}

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefault page)
		{


			// does not work for android 2.2?
			//Action Toggle = ApplicationContent.BindKeyboardToDiagnosticsConsole();

			//page.Tilde.onclick +=
			//    delegate
			//    {
			//        Toggle();
			//    };

			Native.Document.title = "DCIM Gallery App";

			if (Native.window.parent != Native.window.self)
			{
				Native.document.body.style.backgroundColor = JSColor.Transparent;
			}

			// see also. http://en.wikipedia.org/wiki/Design_rule_for_Camera_File_system


			var container = new IHTMLCenter().AttachToDocument();

			#region AddThumbnailTo
			Action<string, IHTMLElement> AddThumbnailTo =
				(path, div) =>
				{
					new IHTMLImage { }.AttachTo(div).With(
							 img =>
							 {
								 // portrait mode only!

								 div.style.color = JSColor.Red;
								 img.src = "/thumb/" + path;

								 #region onload +=
								 img.InvokeOnComplete(
									 delegate
									 {
										 div.style.color = JSColor.Green;

										 IHTMLPre p = null;

										 img.onclick += delegate
										 {
											 if (p == null)
											 {
												 img.src = "/io/" + path;
												 img.style.width = "100%";
												 div.style.display = IStyle.DisplayEnum.block;

												 p = new IHTMLPre { }.AttachTo(div);
												 GetEXIF("/io/" + path,
													 x =>
													 {
														 p.innerText = x;
													 }
												 );


											 }
											 else
											 {

												 p.Orphanize();
												 p = null;
												 img.src = "/thumb/" + path;
												 img.style.width = "";
											 }

										 };
									 }
								 );
								 #endregion





							 }
						 );
				};
			#endregion

			#region yfile
			ystring yfile = path =>
			{
				new IHTMLDiv { innerText = path }.With(
					div =>
					{
						if (path.ToLower().EndsWith(".jpg"))
						{
							div.innerText = "";
							div.AttachTo(container);
							// hide path

							//new IHTMLBreak().AttachTo(div);
							div.style.display = IStyle.DisplayEnum.inline_block;


							AddThumbnailTo(path, div);
						}
					}
				);
			};
			#endregion



			#region TakePicture
			page.icon.style.cursor = IStyle.CursorEnum.pointer;
			page.icon.onclick +=
				delegate
				{
					this.TakePicture("",
						path =>
						{
							Console.WriteLine(new { path });

							new IHTMLDiv { innerText = path }.With(
								div =>
								{
									if (path.ToLower().EndsWith(".jpg"))
									{
										div.innerText = "";

										container.insertBefore(div, container.firstChild);

										//div.AttachTo(container);
										// hide path

										//new IHTMLBreak().AttachTo(div);
										div.style.display = IStyle.DisplayEnum.inline_block;


										AddThumbnailTo(path, div);
									}
								}
							);
						}
					);

				};
			#endregion



			new IHTMLButton { innerText = "more" }.AttachToDocument().With(
				async more =>
				{
					more.style.margin = "1em";

					while (true)
					{


						more.disabled = true;
						more.innerText = "checking for more...";


						await this.File_list(
							yfile: yfile
						);

						this.skip += this.take;
						more.innerText = "more " + new { this.skip };
						more.disabled = false;

						#region either onclick or onscrollToBottom
						await Task.WhenAny(
							Native.window.async.onscrollToBottom,
							more.async.onclick
						);
						#endregion

					}
				}
			);



		}

	}












	public static class DownloadSDKFunction
	{
		// - The digital signature of the object did not verify.

		public static void DownloadSDK(WebServiceHandler h)
		{
			const string _download = "/download/";

			var path = h.Context.Request.Path;

			if (path == "/crx")
			{
				// https://code.google.com/p/chromium/issues/detail?id=128748

				h.Context.Response.Redirect("/download/foo.crx");
				h.CompleteRequest();
				return;
			}

#if HASPublish
            var p = new com.abstractatech.dcimgalleryapp.Assets.Publish();
            //var p2 = new WithClickOnceLANLauncher.Assets.Publish2();

            if (path == "/download")
            {
                //I/System.Console( 3016):        at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection.System_Collections_ICollection_get_Count(__Dictionary_2___KeyCollection.java:83)
                //I/System.Console( 3016):        at ScriptCoreLib.Extensions.IEnumerableExtensions.AsEnumerable(IEnumerableExtensions.java:25)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.DownloadSDKFunction.DownloadSDK(DownloadSDKFunction.java:73)
                //I/System.Console( 3016):        at WithClickOnceLANLauncher.ApplicationWebService.DownloadSDK(ApplicationWebService.java:44)

                var key = p.Keys.ICollectionAsEnumerable().Select(k => p[(string)k]).First(k => k.EndsWith(".application")).SkipUntilLastIfAny("/");

                Console.WriteLine(
                    new
                    {
                        key
                    }
                );

                h.Context.Response.Redirect("/download/" + key);
                h.CompleteRequest();
                return;
            }


            //if (path == "/download/jsc-web-installer.exe")
            //{
            //    // http://msdn.microsoft.com/en-us/library/h4k032e1.aspx
            //    // is chrome happier if we rename it?
            //    path = "/download/setup.exe";
            //}

            if (path == "/download/")
            {
                var href = "http://www.jsc-solutions.net/download/jsc-web-installer.exe";

                var html = @"
                    <meta http-equiv='Refresh' target='_top' content='1;url=" + href + @"' />

                    
                    <center>
                    
                    <br />
                    <br />
                    <br />

<a href='" + href + @"'>Thank you for downloading JSC!</a>
                     
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

<div><i>Note that recent versions of <b>Google Chrome</b> may need additional time to verify.</i></div>                  
                                       
                                       </center>";

                h.Context.Response.ContentType = "text/html";

                var bytes = Encoding.UTF8.GetBytes(html);
                h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                h.CompleteRequest();
                return;
            }


            // we will compare the win32 relative paths here...
            var publish = path.SkipUntilOrEmpty("/download/").Replace("/", @"\");

            // 	- Exception reading manifest from http://192.168.1.100:24257/#/download/Application%20Files/WithClickOnceLANLauncherClient_1_0_0_2/WithClickOnceLANLauncherClient.exe.manifest: the manifest may not be valid or the file could not be opened.
            // did publish work and were it compiled into AssetsLibrary correctly?

            if (p.ContainsKey(publish))
            {
                var f = p[publish];


                var ext = "." + f.SkipUntilLastOrEmpty(".").ToLower();

                // http://en.wikipedia.org/wiki/Mime_type
                // http://msdn.microsoft.com/en-us/library/ms228998.aspx

                var ContentType = "application/octet-stream";

                if (ext == ".application")
                {
                    ContentType = "application/x-ms-application";
                }
                else if (ext == ".manifest")
                {
                    ContentType = "application/x-ms-manifest";
                }
                else if (ext == ".htm")
                {
                    ContentType = "text/html";
                }
                else if (ext == ".crx")
                {
                    // http://feedback.livereload.com/knowledgebase/articles/85889-chrome-extensions-apps-and-user-scripts-cannot
                    // http://stackoverflow.com/questions/12049366/re-enabling-extension-installs

                    ContentType = "application/x-chrome-extension";
                    // Resource interpreted as Document but transferred with MIME type application/x-chrome-extension: "http://192.168.1.106:16507/download/foo.crx".

                    //h.Context.Response.AddHeader(
                    //    "Content-Disposition", "attachment; filename=\"xfoo.crx\"");


                }


                h.Context.Response.ContentType = ContentType;




                DownloadSDKFile(h, f);


            }
#endif



			return;
		}

		private static void DownloadSDKFile(WebServiceHandler h, string fpath, string folder = "/download")
		{
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20130605-lan-clickonce

			Console.WriteLine("download: " + fpath);

			if (fpath.EndsWith(".application"))
			{
				var bytes_application = System.IO.File.ReadAllText(fpath);

				var HostUri = new
				{
					Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
					Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
				};

				var x = bytes_application.Replace(
					"127.0.0.1:8181",

					// change path by adding a sub folder
					HostUri.Host + ":" + HostUri.Port + folder
				);
				Console.WriteLine(x);
				h.Context.Response.Write(x);
				h.CompleteRequest();
				return;
			}


			var bytes = System.IO.File.ReadAllBytes(fpath);
			h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
			h.CompleteRequest();
		}
	}

}
