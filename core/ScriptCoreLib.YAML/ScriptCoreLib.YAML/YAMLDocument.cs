using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ScriptCoreLib.YAML
{

	// see: http://yaml.org/spec/current.html
	public class YAMLDocument
	{
		public const string ContentType = "text/x-yaml";

		const string Indent = " ";
		const string Assignment = ": ";

		public static object FromScalars(Type t, StringReader r, ContinueAtDelegate c)
		{
			var n = Activator.CreateInstance(t);
			var f = t.GetFields();

			var x = r.ReadLine();

			while (x != null)
			{
				if (x.StartsWith(Indent))
				{
					var i = x.IndexOf(Assignment);

					var FieldName = x.Substring(Indent.Length, i - Indent.Length);
					var FieldValue = x.Substring(i + Assignment.Length);

					t.SetFieldValue(FieldName, n, FieldValue);
					x = r.ReadLine();
				}
				else
				{
					c(x);
					x = null;
				}
			}

			return n;
		}

		public static Array FromMappingsSequence(Type t, string data)
		{
			var r = new StringReader(data);

			var a = Array.CreateInstance(t, 0);

			var x = default(string);

			Action next = () => x = r.ReadLine();
			Action revert = next.ApplyAction(k => next = k);
			next();

			while (x != null)
			{
				revert();

				if (x == "-")
				{
					var length = ((object[])a).Length;
					var b = Array.CreateInstance(t, length + 1);
					Array.Copy(a, b, length);
					((object[])b)[length] = FromScalars(t, r, n => next = () => x = n);
					a = b;
				}

				next();
			}

			return a;
		}

		public static string WriteMappingsSequence(Type t, params object[] data)
		{
			// fields must only be instance and string

			var w = new StringBuilder();
			var f = t.GetFields();

			foreach (var k in data)
			{
				w.AppendLine("-");

				foreach (var i in f)
				{
					w.AppendLine(Indent + i.Name + Assignment + i.GetValue(k));
				}
			}

			return w.ToString();
		}

		public delegate void ContinueAtDelegate(string e);
	}

}
