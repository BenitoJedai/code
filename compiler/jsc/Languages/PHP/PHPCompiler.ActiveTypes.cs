using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using jsc.CodeModel;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
    {


		TypeInfo[] ActiveTypes
		{
			get
			{
				var u = GetActiveTypes().Select(k => TypeInfoOf(k)).ToArray();

				var a = new List<TypeInfo>();

				foreach (var i in u)
				{
					// scan from the end to front

					int j = a.Count - 1;

					for (; j >= 0; j--)
					{
						if (i.ResolvedBaseTypes.Contains(a[j].ResolvedValue))
						{
							// we found the first dependency
							break;
						}
					}

					// add after last detected dependency
					a.Insert(j + 1, i);
				}

				var value = a.ToArray();

				return value;
			}
		}


    }
}
