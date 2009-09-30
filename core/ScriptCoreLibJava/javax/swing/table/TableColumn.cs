// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.TableColumn

using ScriptCoreLib;
using java.beans;
using java.lang;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/TableColumn.html
	[Script(IsNative = true)]
	public class TableColumn
	{
		/// <summary>
		/// Cover method, using a default model index of 0,
		/// default width of 75, a <code>null</code> renderer and a
		/// <code>null</code> editor.
		/// </summary>
		public TableColumn()
		{
		}

		/// <summary>
		/// Cover method, using a default width of 75, a <code>null</code>
		/// renderer and a <code>null</code> editor.
		/// </summary>
		public TableColumn(int @modelIndex)
		{
		}

		/// <summary>
		/// Cover method, using a <code>null</code> renderer and a
		/// <code>null</code> editor.
		/// </summary>
		public TableColumn(int @modelIndex, int @width)
		{
		}

		/// <summary>
		/// Creates and initializes an instance of
		/// <code>TableColumn</code> with <code>modelIndex</code>.
		/// </summary>
		public TableColumn(int @modelIndex, int @width, TableCellRenderer @cellRenderer, TableCellEditor @cellEditor)
		{
		}

		/// <summary>
		/// Adds a <code>PropertyChangeListener</code> to the listener list.
		/// </summary>
		public void addPropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// As of Java 2 platform v1.3, this method is not called by the <code>TableColumn</code>
		/// constructor.
		/// </summary>
		protected TableCellRenderer createDefaultHeaderRenderer()
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>as of Java 2 platform v1.3</I>
		/// </summary>
		public void disableResizedPosting()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>as of Java 2 platform v1.3</I>
		/// </summary>
		public void enableResizedPosting()
		{
		}

		/// <summary>
		/// Returns the <code>TableCellEditor</code> used by the
		/// <code>JTable</code> to edit values for this column.
		/// </summary>
		public TableCellEditor getCellEditor()
		{
			return default(TableCellEditor);
		}

		/// <summary>
		/// Returns the <code>TableCellRenderer</code> used by the
		/// <code>JTable</code> to draw
		/// values for this column.
		/// </summary>
		public TableCellRenderer getCellRenderer()
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Returns the <code>TableCellRenderer</code> used to draw the header of the
		/// <code>TableColumn</code>.
		/// </summary>
		public TableCellRenderer getHeaderRenderer()
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Returns the <code>Object</code> used as the value for the header
		/// renderer.
		/// </summary>
		public object getHeaderValue()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the <code>identifier</code> object for this column.
		/// </summary>
		public object getIdentifier()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the maximum width for the <code>TableColumn</code>.
		/// </summary>
		public int getMaxWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum width for the <code>TableColumn</code>.
		/// </summary>
		public int getMinWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the model index for this column.
		/// </summary>
		public int getModelIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the preferred width of the <code>TableColumn</code>.
		/// </summary>
		public int getPreferredWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the <code>PropertyChangeListener</code>s added
		/// to this TableColumn with addPropertyChangeListener().
		/// </summary>
		public PropertyChangeListener[] getPropertyChangeListeners()
		{
			return default(PropertyChangeListener[]);
		}

		/// <summary>
		/// Returns true if the user is allowed to resize the
		/// <code>TableColumn</code>'s
		/// width, false otherwise.
		/// </summary>
		public bool getResizable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the width of the <code>TableColumn</code>.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Removes a <code>PropertyChangeListener</code> from the listener list.
		/// </summary>
		public void removePropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Sets the editor to used by when a cell in this column is edited.
		/// </summary>
		public void setCellEditor(TableCellEditor @cellEditor)
		{
		}

		/// <summary>
		/// Sets the <code>TableCellRenderer</code> used by <code>JTable</code>
		/// to draw individual values for this column.
		/// </summary>
		public void setCellRenderer(TableCellRenderer @cellRenderer)
		{
		}

		/// <summary>
		/// Sets the <code>TableCellRenderer</code> used to draw the
		/// <code>TableColumn</code>'s header to <code>headerRenderer</code>.
		/// </summary>
		public void setHeaderRenderer(TableCellRenderer @headerRenderer)
		{
		}

		/// <summary>
		/// Sets the <code>Object</code> whose string representation will be
		/// used as the value for the <code>headerRenderer</code>.
		/// </summary>
		public void setHeaderValue(object @headerValue)
		{
		}

		/// <summary>
		/// Sets the <code>TableColumn</code>'s identifier to
		/// <code>anIdentifier</code>.
		/// </summary>
		public void setIdentifier(object @identifier)
		{
		}

		/// <summary>
		/// Sets the <code>TableColumn</code>'s maximum width to
		/// <code>maxWidth</code>; also adjusts the width and preferred
		/// width if they are greater than this value.
		/// </summary>
		public void setMaxWidth(int @maxWidth)
		{
		}

		/// <summary>
		/// Sets the <code>TableColumn</code>'s minimum width to
		/// <code>minWidth</code>; also adjusts the current width
		/// and preferred width if they are less than this value.
		/// </summary>
		public void setMinWidth(int @minWidth)
		{
		}

		/// <summary>
		/// Sets the model index for this column.
		/// </summary>
		public void setModelIndex(int @modelIndex)
		{
		}

		/// <summary>
		/// Sets this column's preferred width to <code>preferredWidth</code>.
		/// </summary>
		public void setPreferredWidth(int @preferredWidth)
		{
		}

		/// <summary>
		/// Sets whether this column can be resized.
		/// </summary>
		public void setResizable(bool @isResizable)
		{
		}

		/// <summary>
		/// This method should not be used to set the widths of columns in the
		/// <code>JTable</code>, use <code>setPreferredWidth</code> instead.
		/// </summary>
		public void setWidth(int @width)
		{
		}

		/// <summary>
		/// Resizes the <code>TableColumn</code> to fit the width of its header cell.
		/// </summary>
		public void sizeWidthToFit()
		{
		}

	}
}
