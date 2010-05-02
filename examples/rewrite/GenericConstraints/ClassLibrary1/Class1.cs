using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericConstraints;

namespace ClassLibrary1
{
	public class Class1 : Sprite, ISaveActionWhenReady
	{
		static void Test()
		{
			new Class1().AddSaveTo(null, null);

		}
	}
}
