using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	public interface PTextNode
	{
		// to be used from flash or java applet
		// to be defined as async API


		void dummy();
	}

	public delegate void PTextNodeAction(PTextNode e);

	public class PITextNode : PTextNode
	{
		internal ITextNode InternalTextNode;



		#region PTextNode Members

		public void dummy()
		{
		}

		#endregion
	}
}
