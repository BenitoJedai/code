using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	public interface PTextNode : PNode
	{
		// to be used from flash or java applet
		// to be defined as async API


		void dummy();
	}

	public delegate void PTextNodeAction(PTextNode e);

	public class PITextNode : PINode, PTextNode
	{
		internal ITextNode InternalTextNode;



		#region PTextNode Members

		public void dummy()
		{
		}

		#endregion

		public static implicit operator PITextNode(ITextNode i)
		{
			var v = new PITextNode
			{
				InternalTextNode = i,
				InternalNode = i
			};

			return v;
		}
	}
}
