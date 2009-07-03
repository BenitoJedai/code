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
	public class CoolClass3
	{
		private void Ken(string e)
		{
			Console.WriteLine("CoolClass3: " + e);
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
	public class DelegateHint
	{
		public MethodInfo Method;

		public DelegateHint(Type Context, string MethodName, BindingFlags Flags, params Type[] Parameters)
		{
			
			var Methods = Context.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | Flags);

			Console.WriteLine("DelegateHint.Methods " + Methods.Length);

			foreach (var m in Methods)
			{

				if (Method == null)
					if (m.Name == MethodName)
					{
						Method = m;

						Console.WriteLine("DelegateHint.Methods " + m.Name);


						var p = m.GetParameters();
						for (int i = 0; i < Parameters.Length; i++)
						{
							if (!Parameters[i].Equals(p[i].ParameterType))
							{
								Console.WriteLine("DelegateHint.Methods " + Parameters[i].FullName + " vs " + p[i].ParameterType.FullName);

								Method = null;
								break;
							}
						}
					}
			}

		
		}
	}

	[Script]
	public class StringDelegate
	{
		public object Target;
		public MethodInfo Method;


		public StringDelegate(object Target, DelegateHint m)
		{
			this.Target = Target;
			this.Method = m.Method;
		}

		public void Invoke(string e)
		{
			Method.Invoke(Target, new object[] { e });
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
			var h = new StringDelegate(new CoolClass3(), new DelegateHint(typeof(CoolClass3), "Ken", BindingFlags.Instance, typeof(string)));

			h.Invoke("hello world");


			ByInstance(new CoolClass1());
			ByInstance(new CoolClass2());
		}
	}
}
