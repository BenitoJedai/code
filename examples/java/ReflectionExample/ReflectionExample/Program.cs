using System.Threading;
using System;

using ScriptCoreLib;
using System.Reflection;


namespace ReflectionExample
{
	[Script]
	public class CoolClass1
	{
		public void Invoke(string e)
		{
			Console.WriteLine("CoolClass1: " + e);
		}
	}

	[Script]
	public class CoolClass2
	{
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
		public static void ByInstance(object e)
		{
			var t = e.GetType();

			Console.WriteLine("ByInstance Name: " + t.Name);
			Console.WriteLine("ByInstance Fullname: " + t.FullName);


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
		}

		public static void Main(string[] args)
		{
			var t = typeof(CoolClass1);

			Console.WriteLine("Name: " + t.Name);
			Console.WriteLine("Fullname: " + t.FullName);

			ByInstance(new CoolClass1());
			ByInstance(new CoolClass2());
		}
	}
}
