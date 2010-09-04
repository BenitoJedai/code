using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionFileWriteArguments
	{
		public SolutionFileTextFragment Fragment;
		public string Text = "";

		public object Tag;


        public static implicit operator SolutionFileWriteArguments(string u)
        {
            return new SolutionFileWriteArguments
            {
                Fragment = SolutionFileTextFragment.None,
                Text = u
            };
        }

		public static implicit operator SolutionFileWriteArguments(Uri u)
		{
			return new SolutionFileWriteArguments
			{
				Fragment = SolutionFileTextFragment.Keyword,
				Tag = u,
				Text = u.ToString()
			};
		}

		public class BeginRegion : SolutionFileWriteArguments
		{

		}

		public class EndRegion : SolutionFileWriteArguments
		{

		}
	}
}
