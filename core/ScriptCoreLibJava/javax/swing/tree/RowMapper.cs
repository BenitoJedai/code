// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.RowMapper

using ScriptCoreLib;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/RowMapper.html
	[Script(IsNative = true)]
	public interface RowMapper
	{
		/// <summary>
		/// Returns the rows that the TreePath instances in <code>path</code>
		/// are being displayed at.
		/// </summary>
		int[] getRowsForPaths(TreePath[] @path);

	}
}
