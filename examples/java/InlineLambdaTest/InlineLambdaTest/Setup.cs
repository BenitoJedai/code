using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

using ScriptCoreLib;
using System.Diagnostics;

namespace InlineLambdaTest
{
	class Setup
	{
		public const string SettingsFileName = "setup.settings.cmd";

		public static void DefineEntryPoint(IEntryPoint e)
		{

			var settings = new
			{
				PackageName = Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location) + ".jar",
				ProjectName = typeof(Program).Name,
				CompilandNamespace0 = typeof(Program).Namespace.Replace(".", "/"),
				CompilandNamespace1 = typeof(Program).Namespace,
				CompilandType = typeof(Program).Name,
				CompilandFullName = typeof(Program).Namespace + "." + typeof(Program).Name,
			};

			using (var w = new StringWriter())
			{
				w.WriteLine(":: settings for current project modified at " + DateTime.Now);

				WriteSettings(w, settings);

				e[SettingsFileName] = w.ToString();
			}


			e["release/META-INF/MANIFEST.MF"] =
				"Main-Class: " + settings.CompilandFullName + Environment.NewLine +
				"Created-By: jsc.sf.net";
		}


		public static void WriteSettings(StringWriter w, object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
			}
		}

		[DebuggerNonUserCode]
		public static void Main(string[] args)
		{
			Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "web/bin");

			Program.Main(args);
		}
	}
}
