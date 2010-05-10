using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionDocumentViewerTab
	{
		string InternalText;
		public string Text
		{
			get
			{
				return InternalText;
			}
			set
			{
				InternalText = value;
				if (TextChanged != null)
					TextChanged();
			}
		}

		public event Action Activated;
		public void RaiseActivated()
		{
			if (Activated != null)
				Activated();
		}

		public event Action TextChanged;
		public static implicit operator SolutionDocumentViewerTab(string Text)
		{
			return new SolutionDocumentViewerTab { Text = Text };
		}

		public Action Activate;
	}

}
