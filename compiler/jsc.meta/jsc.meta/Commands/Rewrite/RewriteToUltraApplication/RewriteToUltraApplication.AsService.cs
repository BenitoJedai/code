using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.IO;
using jsc.meta.Library;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.WebService;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraApplication
{
	partial class RewriteToUltraApplication
	{
		[Obfuscation(Feature = "invalidmerge")]
		public class AsService
		{
			internal Type PrimaryApplication;

			/// <summary>
			/// This method is to be used within ASP.NET Web Application
			/// to enable Ultra Application features, where javascript, flash, java and
			/// serverside will have seamless integration.
			/// 
			/// jsc. meta could recompile that ASP.NET Web Application at a later
			/// stage for other platforms. Other platforms cannot run jsc at runtime
			/// thus this method need to be erased and replaced with the actual results.
			/// </summary>
			/// <param name="Context"></param>
			/// <param name="PrimaryApplication"></param>
			public static void BeginRequest(System.Web.HttpApplication Context, Type PrimaryApplication)
			{
				if (!Lookup.ContainsKey(PrimaryApplication))
					Lookup[PrimaryApplication] = new AsService { PrimaryApplication = PrimaryApplication };

				Lookup[PrimaryApplication].BeginRequest(Context);

			}

			internal readonly object SyncLock = new object();
			internal Thread Builder;
			internal readonly Stopwatch BuilderStopwatch = new Stopwatch();

			internal WebServiceInterface WebService;

			public class WebServiceInterface
			{
				public readonly RewriteToJavaScriptDocument.AtWebServiceReadyArguments Arguments;

				public readonly Assembly WebServiceAssembly;
				public readonly Type WebServiceGlobalType;

				public class Global
				{
					public Action<object, EventArgs> Application_BeginRequest;
					public Action<HttpApplication> SetApplication;
					public Func<bool> FileExists;
				}

				public readonly Func<Global> CreateGlobal;


				public WebServiceInterface(RewriteToJavaScriptDocument.AtWebServiceReadyArguments a)
				{
					this.Arguments = a;

					this.WebServiceAssembly = Assembly.LoadFrom(Arguments.Assembly.FullName);
					this.WebServiceGlobalType = WebServiceAssembly.GetType(a.GlobalType);

					var _Application_BeginRequest = WebServiceGlobalType.GetMethod("Application_BeginRequest",
						new[] { typeof(object), typeof(EventArgs) }
					);

					var _SetApplication = WebServiceGlobalType.GetMethod("SetApplication",
						new[] { typeof(HttpApplication) }
					);

					var _FileExists = WebServiceGlobalType.GetMethod("FileExists",
						new Type[] { }
					);

					if (_FileExists == null)
						throw new NullReferenceException();

					this.CreateGlobal =
						delegate
						{
							var Instance = Activator.CreateInstance(this.WebServiceGlobalType);

							return new Global
							{
								Application_BeginRequest =
									(o, sender) => _Application_BeginRequest.Invoke(Instance,
										new[] { o, sender }),

								SetApplication =
									(value) => _SetApplication.Invoke(Instance, new[] { value }),

								FileExists =
									() => (bool)_FileExists.Invoke(Instance,
										new Type[] { }
									)

							};
						};





				}
			}

			public string BuilderStatus = "Initializing...";

			public void BeginRequest(System.Web.HttpApplication Context)
			{
				var AfterSyncLock = default(Action);

				lock (this.SyncLock)
				{
					if (InternalGlobalExtensions.IsDefaultPathOrSpecialPath(Context.Request.Path))
					{
						// so can we display the actual page or do we have to wait?


						// are you ready? no? ok...

						#region Builder
						if (Builder == null)
						{
							// start building! 
							BuilderStopwatch.Start();
							Builder = new Thread(
								delegate()
								{
									var r = new jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument
									{
										assembly = new FileInfo(this.PrimaryApplication.Assembly.Location),

										DisableWebServiceJava = true,
										DisableWebServicePHP = true,
										DisableWebServiceTypeMerge = true
										//IsRewriteOnly = true
									};

									r.AtWebServiceReady +=
										a =>
										{
											// what if there was no web service defined? :)

											WebService = new WebServiceInterface(a);
										};

									r.ProccessStatusChanged +=
										e =>
										{
											BuilderStatus = e;
										};

									r.Invoke();

									lock (SyncLock)
									{
										BuilderStopwatch.Stop();
									}

									// Debugger.Break();
								}
							)
							{
								Name = "RewriteToJavaScriptDocument.AsService.Builder"
							};

							Builder.Start();
						}
						#endregion


						if (BuilderStopwatch.IsRunning)
						{
							// chicken and egg...
							// we could show here a precompiled Ultra application! :)
							// it would actually speed up this compiltion too!
							// todo: add a post build event to prebuild and package this loader...



							Context.Response.Write("<meta http-equiv='refresh' content='9' />");
							Context.Response.Write("<title>Loading...</title>");

							Context.Response.Write(WebElements.PageShadowContainer.ToString());

							Context.Response.Write("<body style='margin: 0; padding: 0;'>");
							Context.Response.Write("<p><a href='http://jsc-solutions.net'><img style='border: 0;' src='http://www.jsc-solutions.net/assets/ScriptCoreLib/jsc.png' /></a></p>");

							Context.Response.Write("<div style='border-left: 96px solid #efefef; padding-left: 4em; margin-left: 4em;'>");

							Context.Response.Write("<h2> Application is loading! Please wait...</h2>");
							Context.Response.Write("<p>It may take several minutes... Feel free to go and get a coffee! <img style='border: 0;' src='http://www.jsc-solutions.net/assets/ScriptCoreLib/loading.gif' /></p>");
							Context.Response.Write("<h3>What's it doing now?</h3>");
							Context.Response.Write("<p>Converting <a href='http://en.wikipedia.org/wiki/Common_Intermediate_Language'>.NET Byte Code</a> to javascript and other languages...</p>");
							Context.Response.Write("<code>" + InternalGlobalExtensions.escapeXML(this.BuilderStatus) + "</code>");
							Context.Response.Write("<h3>For how long has it been doing that?</h3>");
							Context.Response.Write("<code>" + BuilderStopwatch.Elapsed.ToString() + "</code>");
							//Context.Response.Write("<h4>Could it be faster?</h4>");
							//Context.Response.Write("<p><strong>Yes! Contact <a href='info@jsc-solutions.net'>sales</a></strong> to purchase a faster version*</p>");
							//Context.Response.Write("<p></p>");
							//Context.Response.Write("<p></p>");
							//Context.Response.Write("<p></p>");
							//Context.Response.Write("<p><small>* Additional development is required by our end</small></p>");
							Context.Response.Write("</body>");


							Context.CompleteRequest();
						}
						else
						{

							AfterSyncLock =
								delegate
								{
									var Global = this.WebService.CreateGlobal();

									Global.SetApplication(Context);
									Global.Application_BeginRequest(new object(), new EventArgs());
								};

						}
					}
					else
					{
						if (BuilderStopwatch.IsRunning)
						{
							// do nothing
						}
						else
						{
							if (Builder != null)
								AfterSyncLock =
									delegate
									{

										var Global = this.WebService.CreateGlobal();

										Global.SetApplication(Context);

										var Exists = Global.FileExists();

										if (Exists)
										{
											var FilePath = Context.Request.Path.Substring(1);
											var LocalFile = Path.Combine(this.WebService.Arguments.Assembly.Directory.Parent.FullName, FilePath);

											// The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.

											var Bytes = default(byte[]);

											using (var fs = Win32File.OpenRead(LocalFile))
												Bytes = fs.ToBytes();

											// http://codelog.climens.net/2009/10/28/getting-mime-type-in-net-from-file-extension/
											// http://svn.apache.org/repos/asf/httpd/httpd/trunk/docs/conf/mime.types

											if (Path.GetExtension(LocalFile) == ".swf")
												Context.Response.ContentType = "application/x-shockwave-flash";

											if (Path.GetExtension(LocalFile) == ".jar")
												Context.Response.ContentType = "application/java-archive";


											Context.Response.BinaryWrite(Bytes);
											Context.CompleteRequest();
										}
									};
						}
					}

				}

				if (AfterSyncLock != null)
					AfterSyncLock();
			}


			readonly static Dictionary<Type, AsService> Lookup = new Dictionary<Type, AsService>();
		}

	}
}
