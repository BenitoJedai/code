using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JSON;
using System.Net.Sockets;
using System.Net;

namespace SimpleChat2
{
	public static class MyDataExtensions
	{
		public static MyData[] Concat(this MyData[] source, MyData e)
		{
			var a = new MyData[source.Length + 1];

			Array.Copy(source, a, source.Length);

			a[source.Length] = e;

			return a;
		}

		public static bool Contains(this MyData[] source, string Name)
		{
			var r = false;
			foreach (var item in source)
			{
				if (item.Name == Name)
				{
					r = true;
					break;
				}
			}
			return r;
		}
		public static MyData[] ConcatDistinct(this MyData[] source, MyData e)
		{
			if (source.Contains(e.Name))
				return source;

			return source.Concat(e);
		}
	}

	public class MyData
	{
		public string Name;
		public string Target;

		

		public static MyData[] Parse(string source)
		{
			var a = new ArrayList();

			JSONDocument.ParseMatrix(source,
				delegate
				{
					var n = new MyData();

					a.Add(n);

					return new JSONDocument.ParseArguments[] 
							{
								(JSONDocument.StringAction)(Name => n.Name = Name),
								(JSONDocument.StringAction)(Target => n.Target = Target)
							};
				}
			);

			return (MyData[])a.ToArray(typeof(MyData));
		}

		public static string ToString(MyData[] a)
		{
			return JSONDocument.ToString(
				delegate
				{
					var i = -1;

					return delegate
					{
						i++;

						if (i < a.Length)
						{
							var c = a[i];

							return new[] { c.Name, c.Target };
						}

						return null;
					};
				}
			);
		}
	}


}
