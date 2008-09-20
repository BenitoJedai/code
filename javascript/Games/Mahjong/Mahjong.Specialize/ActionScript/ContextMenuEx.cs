using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.ui;

namespace Mahjong.Specialize.ActionScript
{
	[Script]
	public class ContextMenuEx : IEnumerable<ContextMenuItem>
	{
		readonly ContextMenu Element = new ContextMenu();

		public ContextMenuItem Add(string Text, Action Handler)
		{
			var n = new ContextMenuItem(Text);

			n.menuItemSelect +=
				delegate
				{
					Handler();
				};

			Element.customItems = Element.customItems.Concat(new[] { n }).ToArray();


			return n;
		}

		#region IEnumerable<ContextMenuItem> Members

		public IEnumerator<ContextMenuItem> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		public static implicit operator ContextMenu(ContextMenuEx e)
		{
			return e.Element;
		}
	}

}
