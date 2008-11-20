using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using WebApplication.Client.Java;
using System.IO;
using System.Linq;

namespace WebApplication
{
	static class Settings
	{

		public static void DefineEntryPoint(IEntryPoint e)
		{
			CreatePHPIndexPage(e, Server.Application.Filename, Server.Application.Application_Entrypoint);


		
			e[typeof(ApplicationApplet).Name + ".htm"] =
				new XElement("body",
					new XAttribute("style", "margin: 0;"),
					ToElementWithAttributes("applet",
						new AppletElementInfo
						{
							code = typeof(ApplicationApplet).FullName,
							codebase = "bin",
							archive = typeof(ApplicationApplet).Name + "Package.jar",
							width = ApplicationApplet.DefaultWidth,
							height = ApplicationApplet.DefaultHeight,
							mayscript = "true"
						}
				/*, ToParameters(
					new {
						foo = "unused",
						bar = "unused"
					}
				).ToArray()*/
					)
				).ToString();

		}

		#region PHP Section
		private static void CreatePHPIndexPage(IEntryPoint e, string file_name, Action entryfunction)
		{

			var w = new ScriptCoreLib.Shared.TextWriter();

			w.WriteLine("<?");

			SharedHelper.PHPInclude(w, SharedHelper.LocalModulesOf(Assembly.GetExecutingAssembly(), ScriptType.PHP));

			w.WriteLine(entryfunction.Method.Name + "();");

			w.Write("?>");

			e[file_name] = w.Text;
		}
		#endregion


		#region Java Applet Section

		class AppletElementInfo
		{
			public string code { get; set; }
			public string codebase { get; set; }
			public string archive { get; set; }
			public int width { get; set; }
			public int height { get; set; }
			public string mayscript { get; set; }
		}

		class ParamInfo
		{
			public string name { get; set; }
			public object value { get; set; }
		}


		#region Applet HTML

		public static XElement ToElementWithAttributes(string tag, object attr, params object[] c)
		{
			return new XElement(tag,
				ToAttributes(attr).Concat(
					new object[] {
                        ""
                        }
				), c
			);
		}

		public static IEnumerable<XElement> ToParameters(object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				yield return new XElement("param",
						ToAttributes(
							new ParamInfo
							{
								name = z.Name,
								value = z.GetValue(v, null)
							}
						)
					);
			}
		}

		public static IEnumerable<object> ToAttributes(object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				yield return new XAttribute(z.Name, z.GetValue(v, null));
			}
		}

		#endregion

		public static void WriteSettings(StringWriter w, object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
			}
		}

		#endregion
	}

}
