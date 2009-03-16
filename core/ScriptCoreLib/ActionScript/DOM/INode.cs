using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM
{
	[Script]
	public class INode
	{
		public string tag { get; set; }

		protected virtual void INode_appendChild(INode child)
		{
		}

		public void appendChild(INode child)
		{
			INode_appendChild(child);
		}
	}
}
