using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace SimplePropertiesFile.Library
{
	public static class MyExtensions
	{
		public static void ToFields(this FileInfo f, object t)
		{
			var x = File.ReadAllText(f.FullName);
			var y = x.Split('\n');

			foreach (var k in y)
			{
				ToFields(t, k.Trim());
			}
		}

		static void ToFields(object t, string command)
		{
			if (string.IsNullOrEmpty(command))
				return;

			if (command.StartsWith("#"))
				return;

			var i = command.IndexOf("=");
			if (i < 0)
				return;

			var field = command.Substring(0, i).Trim();
			var value = command.Substring(i + 1).Trim();

			ToFields(t, field, value);
		}

		static void ToFields(object t, string field, string value)
		{
			var f = t.GetType().GetField(field);

			if (f == null)
				return;

			// only support for the following types are implemented at this time:
			if (f.FieldType.Equals(typeof(int)))
				f.SetValue(t, int.Parse(value));

		}

		public static void Sleep(this int delay)
		{
			Thread.Sleep(delay);

		}
	}
}
