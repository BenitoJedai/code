using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionFile
	{
		public string Name;

		StringBuilder InternalContent = new StringBuilder();
		public string Content
		{
			get
			{
				return this.InternalContent.ToString();
			}
			set
			{
				this.InternalContent = new StringBuilder();
				this.WriteHistory.Clear();
				this.Write(SolutionFileTextFragment.None, value);
			}
		}

		public SolutionBuilder Context;

		public class WriteArguments
		{
			public SolutionFileTextFragment Fragment;
			public string Text;
		}

		// does our java output support generics already?
		public readonly List<WriteArguments> WriteHistory = new List<WriteArguments>();

		public int CurrentIndent { get; set; }

	

		public void WriteLine(SolutionFileTextFragment Fragment, string Text)
		{
			Write(Fragment, Text);
			Write(SolutionFileTextFragment.None, Environment.NewLine);
		}

		public void Write(SolutionFileTextFragment Fragment, string Text)
		{
			WriteHistory.Add(
				new WriteArguments { Fragment = Fragment, Text = Text }
			);

			InternalContent.Append(Text);
		}

		public void WriteLine()
		{
			WriteLine(SolutionFileTextFragment.None, "");
		}
	}
}
