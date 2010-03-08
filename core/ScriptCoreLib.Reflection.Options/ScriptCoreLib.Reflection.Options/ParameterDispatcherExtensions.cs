using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ScriptCoreLib.Reflection.Options
{
	public static class ParameterDispatcherExtensions
	{
		public static void AsParametersTo(this string[] args, params Action[] Handlers)
		{
			var a = new ParameterDispatcher();

			foreach (var h in Handlers)
			{
				a.Add(h);
			}

			args.AsParametersTo(a);
		}


		public static void AsParametersTo(this string[] args, object e)
		{
			foreach (var k in args)
			{
				k.AsParameterTo(e);
			}
		}

		internal static void AsParametersTo(this string[] args, ParameterDispatcher e)
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

				var Operation = e[OperationName];

				if (Operation == null)
					Operation = e[null];

				object Arguments = null;

				if (Operation != null)
					if (Operation.GetArguments != null)
						Arguments = Operation.GetArguments();

				if (Arguments != null)
					args.AsParametersTo(Arguments);

				if (Operation != null)
					if (Operation.Handler != null)
						Operation.Handler(Operation);
			}
		}

		internal static void AsParameterTo(this string arg, object e)
		{
			if (!arg.StartsWith("/"))
				return;

			arg = arg.Substring(1);

			Trace("AsParameterTo: " + arg);

			var t = e.GetType();
			var i = arg.IndexOf(".");
			var j = arg.IndexOf(":");

			if (i < 0 || i > j)
			{

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
					SetField(e, v, f, false);
				}
				else
				{
					Trace("AsParameterTo: no field found for " + k);
				}
			}
		}

		private static void SetField(object e, string v, FieldInfo f, bool HasImplicitValue)
		{
			if (f.FieldType.IsArray)
			{
				var et = f.FieldType.GetElementType();

				var x = default(object);

				if (HasImplicitValue)
				{
					x = CreateInstanceFromString(et, v);
				}
				else
				{
					x = Activator.CreateInstance(et);
				}


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

				if (!HasImplicitValue)
					v.AsParameterTo(x);

			}
			else if (f.FieldType.IsClass)
			{
				var x = f.GetValue(e);

				if (HasImplicitValue)
				{
					x = CreateInstanceFromString(f.FieldType, v);
					f.SetValue(e, x);
				}
				else if (x == null)
				{
					x = Activator.CreateInstance(f.FieldType);
					f.SetValue(e, x);
				}

				if (!HasImplicitValue)
					v.AsParameterTo(x);
			}
			else
			{
				Trace("AsParameterTo: not a class " + f.FieldType.FullName);
			}
		}

		public static object CreateInstanceFromString(Type t, string arg0)
		{
			// is this the correct way for all supported platforms?

			var m = t.GetMethods(BindingFlags.Static | BindingFlags.Public);
			var ctor = default(MethodInfo);

			foreach (var item in m)
			{
				if (item.ReturnType.Equals(t))
				{
					var p = item.GetParameters();

					if (p.Length == 1)
						if (p[0].ParameterType.Equals(typeof(string)))
						{
							ctor = item;
							break;
						}
				}
			}

			var x = default(object);

			if (ctor != null)
				x = ctor.Invoke(null, new object[] { arg0 });

			return x;
		}

		internal static void AsParameterTo(this string value, object e, FieldInfo f)
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

			if (f.FieldType.Equals(typeof(Uri)))
			{
				f.SetValue(e, new Uri(value));
				return;
			}



			SetField(e, value, f, true);

			//Trace("AsParameterTo: unknown data type " + f.FieldType.FullName);
		}

		internal static void Trace(string e)
		{
			//Console.WriteLine(e);
		}
	}
}
