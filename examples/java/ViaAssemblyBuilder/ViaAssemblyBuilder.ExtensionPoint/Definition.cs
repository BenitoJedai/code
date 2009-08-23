﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViaAssemblyBuilder.ExtensionPoint
{
	public static class Definition
	{
		public static void Invoke()
		{
			
			Console.WriteLine("At ViaAssemblyBuilder.ExtensionPoint");
			Console.WriteLine("We do not reference any java specific API but we must keep in mind they still must exist.");
			Console.WriteLine("We do not know about the application or the meta compiler but the meta compiler knows us but not the application.");

		}
	}
}
