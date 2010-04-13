using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace ScriptCoreLib.JavaScript.BCLImplementation.Microsoft.VisualBasic
{
	[Script(Implements = typeof(global::Microsoft.VisualBasic.Interaction))]
	internal class __Interaction
	{
		public static MsgBoxResult MsgBox(object Prompt, MsgBoxStyle Buttons, object Title)
		{
			Native.Window.alert(Convert.ToString(Prompt));

			return MsgBoxResult.Ok;
		}
	}
}
