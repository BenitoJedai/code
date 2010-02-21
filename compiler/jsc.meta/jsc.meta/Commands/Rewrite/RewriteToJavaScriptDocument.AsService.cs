using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using jsc.meta.Commands.Rewrite.Templates;
using System.Threading;
using System.Diagnostics;
using System.Web;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
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
										DisableWebServicePHP = true
										//IsRewriteOnly = true
									};

									r.AtWebServiceReady +=
										a =>
										{
											WebService = new WebServiceInterface(a);
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
							Context.Response.Write("<meta http-equiv='refresh' content='1' />");
							Context.Response.Write("<p>Application is loading! Please wait...</p>");
							Context.Response.Write(BuilderStopwatch.Elapsed.ToString());
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

											if (File.Exists(LocalFile))
											{
												Context.Response.TransmitFile(LocalFile);
												Context.CompleteRequest();
											}
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
