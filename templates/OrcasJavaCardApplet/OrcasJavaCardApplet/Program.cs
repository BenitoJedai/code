using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Reflection;
using ScriptCoreLibJavaCard;

namespace OrcasJavaCardApplet
{
	class Program
	{


		public const string SettingsFileName = "setup.settings.cmd";

		public static void DefineEntryPoint(IEntryPoint e)
		{
			var Target = typeof(Cafebabe);

		
			var aid = new AIDAttribute.Info(Target);

			var settings = new
			{
				aid.AppletAID,
				aid.PackageAID,
				ProjectName = typeof(Cafebabe).Name,
				CompilandNamespace0 = typeof(Cafebabe).Namespace.Replace(".", "/"),
				CompilandNamespace1 = typeof(Cafebabe).Namespace,
				CompilandType = typeof(Cafebabe).Name,
				CompilandFullName = typeof(Cafebabe).Namespace + "." + typeof(Cafebabe).Name,
			};

			using (var w = new StringWriter())
			{
				w.WriteLine(":: settings for current project modified at " + DateTime.Now);

				WriteSettings(w, settings);

				e[SettingsFileName] = w.ToString();
			}

		
			// C:\util\java_card_kit-2_2_1\doc\en\guides
			// cJDC_Users_Guide.pdf


			//e["release/META-INF/MANIFEST.MF"] =
			//    "Main-Class: " + settings.CompilandFullName + Environment.NewLine +
			//    "Created-By: jsc.sf.net";
		}


		public static void WriteSettings(StringWriter w, object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
			}
		}

		static void Main(string[] args)
		{
			var i = new AIDAttribute.Info(typeof(Cafebabe));
		}
	}
}
