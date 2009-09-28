using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib;
using System.Xml.Serialization;
using System.Xml;
using jsc.Loader;

namespace jsc.Languages.JavaScript
{
	static class EntryPointProvider
	{
		#region ClickOnceTemplate
		static string _ClickOnceTemplate;

		static public string ClickOnceTemplate
		{
			get
			{
				if (_ClickOnceTemplate == null)
					_ClickOnceTemplate = "jsc.Languages.JavaScript.$ClickOnce$.js".GetResourceFileContent()
						.Replace("\r\n", "")
						.Replace("\t", "")
						.ReplaceSpace("{", "}", ",", "=");

				return _ClickOnceTemplate;
			}
		}
		#endregion


		#region ScriptedLoadingTemplate
		static string _ScriptedLoadingTemplate;

		static public string ScriptedLoadingTemplate
		{
			get
			{
				if (_ScriptedLoadingTemplate == null)
					_ScriptedLoadingTemplate = "jsc.Languages.JavaScript.$ScriptedLoading$.js".GetResourceFileContent()
						.Replace("\r\n", "")
						.Replace("\t", "")
						.ReplaceSpace("{", "}", ",", "=");

				return _ScriptedLoadingTemplate;
			}
		}
		#endregion

		static string GetEntrypointScript(this Assembly ass, string BaseURL, string code, string template)
		{
			var w = new StringWriter();

			//w.Write("((function(_7,_8){var _1=0,_2='onreadystatechange',_3=document.getElementsByTagName('HEAD')[0],_4,_5;for(_4 in _7){_5=document.createElement('SCRIPT');_5.src=_7[_4];_3.appendChild(_5);_5[_2 in _5?_2:'onload']=function(){var _6=_5.readyState;if(_6==null||_6=='loaded'||_6=='complete')if(++_1==_7.length)_8();};}})([");
			//w.Write("((function(h,i){var a=0,b='onreadystatechange',c=document.getElementsByTagName('HEAD')[0],d,e;for(d in h){e=document.createElement('SCRIPT');e.src=h[d];c.appendChild(e);e[b in e?b:'onload']=function(){var f=e.readyState;if(f==null||f=='loaded'||f=='complete')if(++a==h.length)i();};}})([");

			var a = LoaderStrategy.LoadReferencedAssemblies(ass, new[] { ScriptType.JavaScript }).Select(k => Path.GetFileName(k.Location)).ToArray();

			for (int i = 0; i < a.Length; i++)
			{
				if (i > 0)
					w.Write(",");
				w.Write("'" + BaseURL + a[i] + ".js'");
			}

			//w.Write("],function(){" + code + "}))");

			return template
				.ReplaceSpace("    ", "   ", "  ", " ")
				.Replace("$references$", w.ToString())
				.Replace("$done$", code);
		}

		static void WriteEntryPointHTMLTemplate(StreamWriter w, ScriptApplicationEntryPointAttribute _ScriptApplicationEntryPoint, string Title, Action WriteScript, Action WriteBody)
		{
			w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			w.WriteLine("<html>");
			w.WriteLine("<head>");

			w.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");


			w.WriteLine("<title>" + Title + "</title>");

			w.WriteLine("<!-- created at " + System.DateTime.Now.ToString() + " -->");

			WriteScript();

			w.WriteLine("<script></script>");

			w.WriteLine("</head>");

			if (_ScriptApplicationEntryPoint.Background)
				w.Write("<body style='margin: 0; overflow: hidden; background: #" + _ScriptApplicationEntryPoint.BackgroundColor.ToString("X6") + ";'>");
			else
				w.Write("<body style='margin: 0; overflow: hidden;'>");

			w.WriteLine("<noscript>ScriptApplication cannot run without JavaScript!</noscript>");


			if (_ScriptApplicationEntryPoint.AlignToCenter)
			{
				w.WriteLine("<div style='width: 100%; height: 100%; overflow: hidden;'>");
				w.WriteLine("<div style='position:absolute; top: 50%; left: 50%; width: " + _ScriptApplicationEntryPoint.Width + "px; height: " + _ScriptApplicationEntryPoint.Height + "px; margin-left: -" + (_ScriptApplicationEntryPoint.Width / 2) + "px; margin-top: -" + (_ScriptApplicationEntryPoint.Height / 2) + "px;'>");
				WriteBody();

				w.WriteLine("</div>");
				w.WriteLine("</div>");

			}
			else
			{
				WriteBody();
			}




			w.WriteLine("</body>");
			w.WriteLine("</html>");
		}


		static void DefineSpawnPoint(this Assembly ass, ScriptApplicationEntryPointAttribute s, StreamWriter w, string alias, string mime, string data)
		{
			WriteEntryPointHTMLTemplate(w, s,
				ass.GetName().Name,
				() =>
					SharedHelper.DefineScript(w,
						//SharedHelper.LoadReferencedAssemblies(ass, true).Select(k => new FileInfo(k.Location).Name).ToArray()),
						LoaderStrategy.LoadReferencedAssemblies(ass, new[] { ScriptType.JavaScript }).Reverse().Distinct().Select(k => Path.GetFileName(k.Location)).ToArray()
					),

				() => w.WriteLine("<script type='" + mime + "' class='" + alias + "'>" + (string.IsNullOrEmpty(data) ? "" : "\n" + data + "\n") + "</script>")
			);



		}

		static public void WriteEntryPoints(this Assembly a, DirectoryInfo dir)
		{
			if (ScriptAttribute.OfProvider(a) == null)
				return;

			// xxx

			foreach (var v in from i in ScriptAttribute.FindTypes(a, ScriptType.JavaScript)
							  let s = (ScriptAttribute)i.GetCustomAttributes(typeof(ScriptAttribute), false).SingleOrDefault()
							  where s != null
							  select i)
			{
				var s = (ScriptApplicationEntryPointAttribute)v.GetCustomAttributes(typeof(ScriptApplicationEntryPointAttribute), false).SingleOrDefault();

				if (s != null)
				{
					var field = v.GetField("DefaultData");

					var data = field == null ? null : field.GetValue(null);

					//DefineSpawnPointXML(e, v.Name, data);
					//DefineSpawnPointJSON(e, v.Name, data);

					Console.WriteLine("entrypoint: " + v.Name);



					#region IsClickOnce
					if (s.IsClickOnce)
					{
						using (var w = dir.CreateFile(v.Name + ".ClickOnce.htm"))
						{
							var done = "/* ctor not found */";
							var ctor = default(ConstructorInfo);

							#region ctor(typeof(DefaultData))
							if (data != null && (ctor = v.GetConstructor(field.FieldType)) != null)
								using (var x = new IdentWriter())
								{
									var h = new IL2ScriptWriterHelper(x);

									x.Write("new ");
									h.WriteWrappedConstructor(ctor);
									x.Write("(");

									x.Write(ScriptCoreLib.Tools.JSONSerializer.Serialize(data, '\''));
									//x.Write("null");

									x.Write(");");
									//done = "alert('" + x.ToString() + "');";

									done = x.ToString();
								}
							#endregion
							#region ctor()
							else if ((ctor = v.GetConstructor()) != null)
								using (var x = new IdentWriter())
								{
									var h = new IL2ScriptWriterHelper(x);

									x.Write("new ");
									h.WriteWrappedConstructor(ctor);
									x.Write("();");

									done = x.ToString();
								}
							#endregion


							WriteEntryPointHTMLTemplate(
								w, s,
								a.GetName().Name,
								() => { },
								delegate
								{
									w.Write("<a href=\"javascript:");
									w.Write(a.GetEntrypointScript("", done, ClickOnceTemplate));
									w.Write("\">Click to open <b>" + v.Name + "</b></a>");
								}
							);
						}

						// ...
					}
					#endregion

					#region ScriptedLoading
					if (s.ScriptedLoading)
					{
						using (var w = dir.CreateFile(v.Name + ".htm"))
						{
							var done = "/* ctor not found */";
							var ctor = default(ConstructorInfo);

							#region ctor(typeof(DefaultData))
							if (data != null && (ctor = v.GetConstructor(field.FieldType)) != null)
								using (var x = new IdentWriter())
								{
									var h = new IL2ScriptWriterHelper(x);

									x.Write("new ");
									h.WriteWrappedConstructor(ctor);
									x.Write("(");

									x.Write(ScriptCoreLib.Tools.JSONSerializer.Serialize(data, '\''));
									//x.Write("null");

									x.Write(");");
									//done = "alert('" + x.ToString() + "');";

									done = x.ToString();
								}
							#endregion
							#region ctor()
							else if ((ctor = v.GetConstructor()) != null)
								using (var x = new IdentWriter())
								{
									var h = new IL2ScriptWriterHelper(x);

									x.Write("new ");
									h.WriteWrappedConstructor(ctor);
									x.Write("();");

									done = x.ToString();
								}
							#endregion


							WriteEntryPointHTMLTemplate(
								w, s,
								a.GetName().Name,
								() => { },
								delegate
								{
									w.Write("<script>");
									w.Write(a.GetEntrypointScript("", done, ScriptedLoadingTemplate));
									w.Write("</script>");
								}
							);
						}

						// ...
					}
					#endregion

					// ScriptedLoading uses defalu html file name

					Func<string, StreamWriter> CreateFile =
						suffix => dir.CreateFile(v.Name + (string.IsNullOrEmpty(suffix) ? "" : "." + suffix) + ".htm");


					if ((s.Format & SerializedDataFormat.xml) == SerializedDataFormat.xml)
						using (var w = CreateFile(!s.ScriptedLoading && SerializedDataFormat.xml == s.DefaultFormat ? "" : "xml"))
						{
							a.DefineSpawnPoint(s, w, v.Name, "text/xml", data.SerializeToXML());
						}

					if ((s.Format & SerializedDataFormat.json) == SerializedDataFormat.json)
						using (var w = CreateFile(!s.ScriptedLoading && SerializedDataFormat.json == s.DefaultFormat ? "" : "json"))
						{
							a.DefineSpawnPoint(s, w, v.Name, "text/json", data == null ? null : data.SerializeToJSON());
						}
				}
			}
		}
	}


}
