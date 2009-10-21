// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreeCellEditor

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreeCellEditor.html
	[Script(IsNative = true)]
	public interface TreeCellEditor : CellEditor
	{
		/// <summary>
		/// Sets an initial <I>value</I> for the editor.
		/// </summary>
		Component getTreeCellEditorComponent(JTree @tree, object @value, bool @isSelected, bool @expanded, bool @leaf, int @row);

	}
}
