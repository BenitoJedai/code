using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Ultra.Studio.Formatting;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionFile
	{
		public HTMLElementFormatting HTMLElementFormatting = new HTMLElementFormatting();
		public XElementFormatting XElementFormatting = new XElementFormatting();

		public SolutionFile()
		{
			this.IndentStack = new Stack<Action>();
			this.IndentStack.Push(delegate { });
		}

		public SolutionFile DependentUpon;

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
				Clear();
				this.Write(SolutionFileTextFragment.None, value);
			}
		}

		public void Clear()
		{
			this.InternalContent = new StringBuilder();
			this.WriteHistory.Clear();
		}

		public SolutionBuilder Context;
		public SolutionProjectLanguageType ContextType;


		// does our java output support generics already?
		public readonly List<SolutionFileWriteArguments> WriteHistory = new List<SolutionFileWriteArguments>();

		public Stack<Action> IndentStack;




		public void WriteLine(SolutionFileTextFragment Fragment, string Text)
		{
			Write(Fragment, Text);
			Write(SolutionFileTextFragment.None, Environment.NewLine);
		}

		public SolutionFile Write(SolutionFileTextFragment Fragment, string Text)
		{
			this.Write(
				new SolutionFileWriteArguments { Fragment = Fragment, Text = Text }
			);

            return this;
		}

        public SolutionFile Write(SolutionFileWriteArguments a)
		{
            if (a.Text == null)
                return this;

			if (a.Text != Environment.NewLine)
			{
				var r = a.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
				if (r.Length > 1)
				{
					for (int i = 0; i < r.Length; i++)
					{
						Write(
							new SolutionFileWriteArguments
							{
								Fragment = a.Fragment,
								Text = r[i],
								Tag = a.Tag
							}
						);

						Write(SolutionFileTextFragment.None, Environment.NewLine);

					}
					return this;
				}
			}

			WriteHistory.Add(a);

			InternalContent.Append(a.Text);

            return this;
		}

        public void WriteLine(SolutionFileWriteArguments Text)
		{
			Write(Text);
			WriteLine();
		}

		public void WriteLine()
		{
			WriteLine(SolutionFileTextFragment.None, "");
		}

        public SolutionFile WriteSpace()
		{
			Write(" ");

            return this;
		}


        public SolutionFile WriteDirective()
        {
            var n = new SolutionFileWriteArguments { Fragment = SolutionFileTextFragment.Keyword, Text = "#"};

            Write(n);

            return this;
        }


        public SolutionFile WriteSpace(SolutionFileWriteArguments a)
        {
            Write(a);
            WriteSpace();

            return this;
        }

        public SolutionFile WriteSpace(params SolutionFileWriteArguments[] a)
        {
            a.WithEach(k => WriteSpace(k));

            return this;
        }

        public SolutionFile WriteSpaces(SolutionFileWriteArguments a)
        {
            WriteSpace();
            Write(a);
            WriteSpace();

            return this;
        }
	}
}
