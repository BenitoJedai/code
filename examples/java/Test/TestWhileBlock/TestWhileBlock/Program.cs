using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestWhileBlock
{
	[Script]
	class Program
	{
		static void Main(string[] args)
		{
			uint num = (uint)4;
			while (num >= 0x80)
			{
				Implementation();
			}
		}

		static void Implementation()
		{
		}
	}
}
