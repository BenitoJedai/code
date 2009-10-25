using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JSON;

namespace SimpleChat2
{

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
