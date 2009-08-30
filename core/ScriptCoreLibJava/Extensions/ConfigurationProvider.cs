using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace ScriptCoreLibJava.Extensions
{
	public static class ConfigurationProvider
	{
		[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
		public sealed class UrlPatternAttribute : Attribute
		{
			// http://www2.roguewave.com/support/docs/leif/leif/html/bobcatug/7-3.html

			// This is a positional argument
			public UrlPatternAttribute()
				: this("/")
			{
			}

			public UrlPatternAttribute(string PatternString)
			{
				this.PatternString = PatternString;

			}

			public string PatternString
			{
				get;
				private set;
			}


		}


		public static string ToServletConfiguration(this Assembly e)
		{
			var w = new StringBuilder();

			w.AppendLine(@"<?xml version='1.0' encoding='ISO-8859-1'?>
<!-- ConfigurationProvider.ToServletConfiguration -->
<web-app 
   xmlns='http://java.sun.com/xml/ns/javaee' 
   xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
   xsi:schemaLocation='http://java.sun.com/xml/ns/javaee http://java.sun.com/xml/ns/javaee/web-app_2_5.xsd'
   version='2.5'>");

			w.AppendLine("<display-name>" +  Path.GetFileNameWithoutExtension(e.Location) + "</display-name>");

			var servlets = new Dictionary<string, string>();

			// bring on all the sattelite servlets
			#region seek for servlets with assigned pattern
			foreach (var a in ScriptCoreLib.SharedHelper.LoadReferencedAssemblies(e, false))
			{
				ExtractServlets(w, servlets, a);
			}
			#endregion

			// overwrite with our servlets as the last step...
			ExtractServlets(w, servlets, e);

			foreach (var k in servlets.Values)
			{
				w.AppendLine(k);
			}


			w.AppendLine("</web-app>");

			return w.ToString();
		}

		private static void ExtractServlets(StringBuilder w, Dictionary<string, string> servlets, Assembly a)
		{
			w.AppendLine("/* " + a.GetName().Name + " */");

			foreach (var t in a.GetTypes())
			{
				var p = t.GetCustomAttributes(typeof(UrlPatternAttribute), false);

				if (p.Length != 1)
					continue;

				var pattern = p[0] as UrlPatternAttribute;

				servlets[pattern.PatternString] = @"<servlet>
    <servlet-name>{ServletName}</servlet-name>
    <servlet-class>{ServletNamespace}.{ServletName}</servlet-class>
  </servlet>
  <servlet-mapping>
    <servlet-name>{ServletName}</servlet-name>
    <url-pattern>{Pattern}</url-pattern>
  </servlet-mapping>

"
.Replace("{ServletName}", t.Name)
.Replace("{ServletNamespace}", t.Namespace)
.Replace("{Pattern}", pattern.PatternString)
				;
			}
		}
	}
}
