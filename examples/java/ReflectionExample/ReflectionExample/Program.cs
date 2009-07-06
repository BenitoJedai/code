using System.Threading;
using System;

using ScriptCoreLib;
using System.Reflection;


namespace ReflectionExample
{
	[Script, Serializable]
	public class CoolClass1
	{
		public string Field1;
		public string Field2;

		public void Invoke(string e)
		{
			Console.WriteLine("CoolClass1: " + e);
		}
	}

	[Script, Serializable]
	public class CoolClass3
	{
		public string Field1;
		public string Field2;

		private void Ken(string e)
		{
			Console.WriteLine("CoolClass3: " + e);
		}
	}

	[Script, Serializable]
	public class CoolClass2
	{
		public string Field1;
		public string Field2;

		public static void StaticInvoke(string e)
		{
			Console.WriteLine("static CoolClass2: " + e);
		}

		public void Invoke(string e)
		{
			Console.WriteLine("CoolClass2: " + e);
		}
	}

	


	[Script]
	public class Program
	{
		public static object ByInstance(object e)
		{
			var t = e.GetType();

			Console.WriteLine("ByInstance Name: " + t.Name);
			Console.WriteLine("ByInstance Fullname: " + t.FullName);

			foreach (var f in t.GetFields())
			{
				Console.WriteLine("field: " + f.Name + " as " + f.FieldType.FullName);

				if (f.FieldType.Equals(typeof(string)))
				{
					var text = (string)f.GetValue(e);

					Console.WriteLine(" = " + text);

					text += " via reflection";

					f.SetValue(e, text);
				}
			}

			foreach (var m in t.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly))
			{
				Console.WriteLine(m.Name);

				if (m.Name == "Invoke")
				{
					foreach (var p in m.GetParameters())
					{
						Console.WriteLine("Param " + p.Position + " " + p.ParameterType.FullName);
					}

					m.Invoke(e, new[] { "reflection!" });
				}
			}


			foreach (var m in t.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly))
			{
				Console.WriteLine(m.Name);

				if (m.Name == "StaticInvoke")
				{
					foreach (var p in m.GetParameters())
					{
						Console.WriteLine("Param " + p.Position + " " + p.ParameterType.FullName);
					}

					m.Invoke(null, new[] { "reflection!" });
				}
			}

			return e;
		}

		public static void Main(string[] args)
		{
			

			var x = ByInstance(new CoolClass1 { Field1 = "A", Field2 = "B" });
			ByInstance(x);
			ByInstance(new CoolClass2());
		}
	}
}
