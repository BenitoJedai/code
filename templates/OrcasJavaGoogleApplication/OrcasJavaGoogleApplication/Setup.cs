using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

using ScriptCoreLib;
using OrcasJavaGoogleApplication.Server;

namespace OrcasJavaGoogleApplication
{
	class Setup
	{
		public const string SettingsFileName = "setup.settings.cmd";

		public static void DefineEntryPoint(IEntryPoint e)
		{
			e["java/WEB-INF/web.xml"] =
@"<?xml version='1.0' encoding='ISO-8859-1'?>
<web-app 
   xmlns='http://java.sun.com/xml/ns/javaee' 
   xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
   xsi:schemaLocation='http://java.sun.com/xml/ns/javaee http://java.sun.com/xml/ns/javaee/web-app_2_5.xsd'
   version='2.5'> 
  <display-name>{ProjectName}</display-name>
  <servlet>
    <servlet-name>{ServletName}</servlet-name>
    <servlet-class>{ServletNamespace}.{ServletName}</servlet-class>
  </servlet>
  <servlet-mapping>
    <servlet-name>{ServletName}</servlet-name>
    <url-pattern>/</url-pattern>
  </servlet-mapping>
</web-app>
"
.Replace("{ProjectName}", Path.GetFileNameWithoutExtension(typeof(HelloAppEngineServlet).Assembly.Location))
.Replace("{ServletName}", typeof(HelloAppEngineServlet).Name)
.Replace("{ServletNamespace}", typeof(HelloAppEngineServlet).Namespace)
;

		}


		public static void WriteSettings(StringWriter w, object v)
		{
			foreach (PropertyInfo z in v.GetType().GetProperties())
			{
				w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
			}
		}

		public static void Main(string[] e)
		{

		}
	}
}
