using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;

namespace ArgumentsViaReflection.Library
{
	public static class MyExtensions
	{
		public static void Sleep(this int delay)
		{
			Thread.Sleep(delay);

		}

		public static void AsParametersTo(this string[] args, object e)
		{
			foreach (var k in args)
			{
				k.AsParameterTo(e);
			}
		}

		public static void AsParameterTo(this string arg, object e)
		{
			Trace("AsParameterTo: " + arg);

			var t = e.GetType();
			var i = arg.IndexOf(".");

			if (i < 0)
			{
				var j = arg.IndexOf(":");

				if (j > 0)
				{
					var k = arg.Substring(0, j);
					var v = arg.Substring(j + 1);

					var f = t.GetField(k);

					if (f != null)
					{
						v.AsParameterTo(e, f);
					}
				}
			}
			else
			{

				var k = arg.Substring(0, i);
				var v = arg.Substring(i + 1);

				Trace("AsParameterTo: namespace " + k);


				var f = t.GetField(k);

				if (f != null)
				{
					if (f.FieldType.IsClass)
					{
						var x = f.GetValue(e);

						if (x == null)
						{
							x = Activator.CreateInstance(f.FieldType);

							f.SetValue(e, x);
						}

						v.AsParameterTo(x);
					}
					else
					{
						Trace("AsParameterTo: not a class " + f.FieldType.FullName);
					}
				}
				else
				{
					Trace("AsParameterTo: no field found for " + k);
				}
			}
		}

		public static void AsParameterTo(this string value, object e, FieldInfo f)
		{
			if (f.FieldType.Equals(typeof(string)))
			{
				f.SetValue(e, value);
				return;
			}

			if (f.FieldType.Equals(typeof(int)))
			{
				f.SetValue(e, int.Parse(value));
				return;
			}

			if (f.FieldType.Equals(typeof(FileInfo)))
			{
				f.SetValue(e, new FileInfo(value));
				return;
			}

			if (f.FieldType.Equals(typeof(DirectoryInfo)))
			{
				f.SetValue(e, new DirectoryInfo(value));
				return;
			}

			Trace("AsParameterTo: unknown data type " + f.FieldType.FullName);
		}

		public static void Trace(string e)
		{
			//Console.WriteLine(e);
		}
	}
}
