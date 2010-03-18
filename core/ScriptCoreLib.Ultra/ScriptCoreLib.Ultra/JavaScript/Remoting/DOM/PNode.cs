using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	public interface PNode
	{
		// to be used from flash or java applet
		// to be defined as async API

		void appendChild(PNode e);
		void removeChild(PNode e);


		void get_parentNode(PNodeAction y);
	}

	public delegate void PNodeAction(PNode e);

	public class PINode : PNode
	{
		internal INode InternalNode;

		public void appendChild(PNode e)
		{
			var i = e as PINode;
			if (i == null)
				return;

			this.InternalNode.appendChild(i.InternalNode);
		}

		public void removeChild(PNode e)
		{
			var i = e as PINode;
			if (i == null)
				return;

			this.InternalNode.removeChild(i.InternalNode);
		}

		public void get_parentNode( PNodeAction y)
		{
			y((PINode)this.InternalNode.parentNode);
		}

		public static implicit operator PINode(INode i)
		{
			var v = new PINode
			{
				InternalNode = i
			};

			return v;
		}
	}
}
