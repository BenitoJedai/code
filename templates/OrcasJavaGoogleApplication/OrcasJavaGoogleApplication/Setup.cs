using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

using ScriptCoreLib;
using OrcasJavaGoogleApplication.Server;
using ScriptCoreLibJava.Extensions;

namespace OrcasJavaGoogleApplication
{
	class Setup
	{
		public const string SettingsFileName = "setup.settings.cmd";

		public static void DefineEntryPoint(IEntryPoint e)
		{
			e["java/WEB-INF/web.xml"] = typeof(HelloAppEngineServlet).Assembly.ToServletConfiguration();
		}
	
		public static void Main(string[] e)
		{

		}
	}
}
