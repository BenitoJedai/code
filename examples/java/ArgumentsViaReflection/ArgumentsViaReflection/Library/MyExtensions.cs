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

		public static void AsParametersTo(this string[] args, ParameterDispatcher e)
		{
			if (args.Length > 0)
			{
				var OperationName = args[0];

				if (args.Length > 1)
				{
					var x = new string[args.Length - 1];
					Array.Copy(args, 1, x, 0, args.Length - 1);
					args = x;
				}

				args.AsParametersTo(e.Arguments);

				e.Invoke(OperationName);
			}
		}

		public static void AsParameterTo(this string arg, object e)
		{
			if (!arg.StartsWith("/"))
				return;

			arg = arg.Substring(1);

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
				var v = "/" + arg.Substring(i + 1);

				Trace("AsParameterTo: namespace " + k);


				var f = t.GetField(k);

				if (f != null)
				{
					if (f.FieldType.IsArray)
					{
						var et = f.FieldType.GetElementType();

						var x = Activator.CreateInstance(et);

						var a = (Array)f.GetValue(e);


						if (a == null)
						{
							a = Array.CreateInstance(et, 1);
						}
						else
						{
							var n = Array.CreateInstance(et, a.Length + 1);
							a.CopyTo(n, 0);
							a = n;
						}
						a.SetValue(x, a.Length - 1);
						f.SetValue(e, a);

						v.AsParameterTo(x);

					}
					else if (f.FieldType.IsClass)
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

			if (f.FieldType.Equals(typeof(bool)))
			{
				f.SetValue(e, bool.Parse(value));
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
