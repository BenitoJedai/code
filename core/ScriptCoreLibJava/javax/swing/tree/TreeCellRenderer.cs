// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreeCellRenderer

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreeCellRenderer.html
	[Script(IsNative = true)]
	public interface TreeCellRenderer
	{
		/// <summary>
		/// Sets the value of the current tree cell to <code>value</code>.
		/// </summary>
		Component getTreeCellRendererComponent(JTree @tree, object @value, bool @selected, bool @expanded, bool @leaf, int @row, bool @hasFocus);

	}
}
