using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;
using System.Reflection;
using System.Collections;

namespace ArchiveExample
{
	[Script]
	public static class Extensions
	{
		public static MethodInfo[] GetPropertyGetMethodsForType(this Type Context, Type PropertyType)
		{
			var a = new ArrayList();

			const string get_ = "get_";

			foreach (var m in Context.GetMethods())
			{
				//Console.WriteLine("> found: " + m.Name);

				if (m.Name.StartsWith(get_))
				{
					var p = m.GetParameters();

					if (p.Length == 0)
					{
						if (m.ReturnType.Equals(PropertyType))
						{
							a.Add(m);
						}
						else
						{
							//Console.WriteLine("> ret " + m.ReturnType.FullName + " vs " + PropertyType.FullName);

						}
					}
					else
					{
						//Console.WriteLine("> not parameterless");
					}
				}
				else
				{
					//Console.WriteLine("> not get_");
				}
			}

			return (MethodInfo[])a.ToArray(typeof(MethodInfo));
		}

		public static short AssertEqualsTo(this short value, short e)
		{
			if (value != e)
				throw new InvalidOperationException();

			return value;
		}

		public static int AssertEqualsTo(this int value, int e)
		{
			if (value != e)
				throw new InvalidOperationException();

			return value;
		}

		public static MemoryStream ReadToMemoryStream(this BinaryReader e, int length)
		{
			return new MemoryStream(e.ReadBytes(length));
		}

		public static string ReadUTF8String(this BinaryReader e, int length)
		{
			var b = new byte[length];

			e.Read(b, 0, length);

			//return Encoding.UTF8.GetString(b);
			return Encoding.ASCII.GetString(b);
		}
	}
}
