using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.Microsoft.VisualBasic.CompilerServices
{
	[Script(Implements = typeof(global::Microsoft.VisualBasic.CompilerServices.Operators))]
	internal class __Operators
	{
		public static int CompareString(string Left, string Right, bool TextCompare)
		{
			int num2;
			if (Left == Right)
			{
				return 0;
			}
			if (Left == null)
			{
				if (Right.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			if (Right == null)
			{
				if (Left.Length == 0)
				{
					return 0;
				}
				return 1;
			}

			// todo: We are diverting from BCL behaviour :)
			num2 = Left.CompareTo(Right);

			if (num2 == 0)
			{
				return 0;
			}
			if (num2 > 0)
			{
				return 1;
			}
			return -1;
		}





	}
}
