using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractiveOrdering.XBAP
{
	using global::ScriptCoreLib.CSharp.Avalon.Extensions;

	class Program
	{

		[STAThread]
		public static void Main()
		{
			AvalonExtensions.ToApplication<InteractiveOrdering.Shared.InteractiveOrderingCanvas>();
		}

	}


}
