using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class ParentRelation<TElement, TParent>
	{
		public TElement Element;
		public TParent Parent;
	}
}
