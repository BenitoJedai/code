using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class InteractiveComment : SolutionFileComment
	{
		public event Action Click;

		public void RaiseClick()
		{
			if (Click != null)
				Click();
		}

		public static implicit operator InteractiveComment(string Comment)
		{
			return new InteractiveComment
			{
				Comment = Comment,
				// we should redirec to wiki from there if the user
				// actually navigates there :)
				Link = new Uri("http://do.jsc-solutions.net/" + Comment.Replace(" ", "-")),
				MarginBottom = 1
			};
		}
	}

}
