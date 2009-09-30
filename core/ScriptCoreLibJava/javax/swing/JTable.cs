// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JTable

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using java.util;
using javax.accessibility;
using javax.swing.@event;
using javax.swing.plaf;
using javax.swing.table;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JTable.html
	[Script(IsNative = true)]
	public class JTable : JComponent
	{
		/// <summary>
		/// Constructs a default <code>JTable</code> that is initialized with a default
		/// data model, a default column model, and a default selection
		/// model.
		/// </summary>
		public JTable()
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> with <code>numRows</code>
		/// and <code>numColumns</code> of empty cells using
		/// <code>DefaultTableModel</code>.
		/// </summary>
		public JTable(int @numRows, int @numColumns)
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> to display the values in the two dimensional array,
		/// <code>rowData</code>, with column names, <code>columnNames</code>.
		/// </summary>
		public JTable(Object[][] @rowData, object[] @columnNames)
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> that is initialized with
		/// <code>dm</code> as the data model, a default column model,
		/// and a default selection model.
		/// </summary>
		public JTable(TableModel @dm)
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> that is initialized with
		/// <code>dm</code> as the data model, <code>cm</code>
		/// as the column model, and a default selection model.
		/// </summary>
		public JTable(TableModel @dm, TableColumnModel @cm)
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> that is initialized with
		/// <code>dm</code> as the data model, <code>cm</code> as the
		/// column model, and <code>sm</code> as the selection model.
		/// </summary>
		public JTable(TableModel @dm, TableColumnModel @cm, ListSelectionModel @sm)
		{
		}

		/// <summary>
		/// Constructs a <code>JTable</code> to display the values in the
		/// <code>Vector</code> of <code>Vectors</code>, <code>rowData</code>,
		/// with column names, <code>columnNames</code>.
		/// </summary>
		public JTable(Vector @rowData, Vector @columnNames)
		{
		}

		/// <summary>
		/// Appends <code>aColumn</code> to the end of the array of columns held by
		/// this <code>JTable</code>'s column model.
		/// </summary>
		public void addColumn(TableColumn @aColumn)
		{
		}

		/// <summary>
		/// Adds the columns from <code>index0</code> to <code>index1</code>,
		/// inclusive, to the current selection.
		/// </summary>
		public void addColumnSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Calls the <code>configureEnclosingScrollPane</code> method.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Adds the rows from <code>index0</code> to <code>index1</code>, inclusive, to
		/// the current selection.
		/// </summary>
		public void addRowSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Updates the selection models of the table, depending on the state of the
		/// two flags: <code>toggle</code> and <code>extend</code>.
		/// </summary>
		public void changeSelection(int @rowIndex, int @columnIndex, bool @toggle, bool @extend)
		{
		}

		/// <summary>
		/// Deselects all selected columns and rows.
		/// </summary>
		public void clearSelection()
		{
		}

		/// <summary>
		/// Invoked when a column is added to the table column model.
		/// </summary>
		public void columnAdded(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Returns the index of the column that <code>point</code> lies in,
		/// or -1 if the result is not in the range
		/// [0, <code>getColumnCount()</code>-1].
		/// </summary>
		public int columnAtPoint(Point @point)
		{
			return default(int);
		}

		/// <summary>
		/// Invoked when a column is moved due to a margin change.
		/// </summary>
		public void columnMarginChanged(ChangeEvent @e)
		{
		}

		/// <summary>
		/// Invoked when a column is repositioned.
		/// </summary>
		public void columnMoved(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Invoked when a column is removed from the table column model.
		/// </summary>
		public void columnRemoved(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Invoked when the selection model of the <code>TableColumnModel</code>
		/// is changed.
		/// </summary>
		public void columnSelectionChanged(ListSelectionEvent @e)
		{
		}

		/// <summary>
		/// If this <code>JTable</code> is the <code>viewportView</code> of an enclosing <code>JScrollPane</code>
		/// (the usual situation), configure this <code>ScrollPane</code> by, amongst other things,
		/// installing the table's <code>tableHeader</code> as the <code>columnHeaderView</code> of the scroll pane.
		/// </summary>
		protected void configureEnclosingScrollPane()
		{
		}

		/// <summary>
		/// Maps the index of the column in the view at
		/// <code>viewColumnIndex</code> to the index of the column
		/// in the table model.
		/// </summary>
		public int convertColumnIndexToModel(int @viewColumnIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Maps the index of the column in the table model at
		/// <code>modelColumnIndex</code> to the index of the column
		/// in the view.
		/// </summary>
		public int convertColumnIndexToView(int @modelColumnIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the default column model object, which is
		/// a <code>DefaultTableColumnModel</code>.
		/// </summary>
		protected TableColumnModel createDefaultColumnModel()
		{
			return default(TableColumnModel);
		}

		/// <summary>
		/// Creates default columns for the table from
		/// the data model using the <code>getColumnCount</code> method
		/// defined in the <code>TableModel</code> interface.
		/// </summary>
		public void createDefaultColumnsFromModel()
		{
		}

		/// <summary>
		/// Returns the default table model object, which is
		/// a <code>DefaultTableModel</code>.
		/// </summary>
		protected TableModel createDefaultDataModel()
		{
			return default(TableModel);
		}

		/// <summary>
		/// Creates default cell editors for objects, numbers, and boolean values.
		/// </summary>
		protected void createDefaultEditors()
		{
		}

		/// <summary>
		/// Creates default cell renderers for objects, numbers, doubles, dates,
		/// booleans, and icons.
		/// </summary>
		protected void createDefaultRenderers()
		{
		}

		/// <summary>
		/// Returns the default selection model object, which is
		/// a <code>DefaultListSelectionModel</code>.
		/// </summary>
		protected ListSelectionModel createDefaultSelectionModel()
		{
			return default(ListSelectionModel);
		}

		/// <summary>
		/// Returns the default table header object, which is
		/// a <code>JTableHeader</code>.
		/// </summary>
		protected JTableHeader createDefaultTableHeader()
		{
			return default(JTableHeader);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.2,
		/// replaced by <code>new JScrollPane(aTable)</code>.</I>
		/// </summary>
		static public JScrollPane createScrollPaneForTable(JTable @aTable)
		{
			return default(JScrollPane);
		}

		/// <summary>
		/// Causes this table to lay out its rows and columns.
		/// </summary>
		public void doLayout()
		{
		}

		/// <summary>
		/// Programmatically starts editing the cell at <code>row</code> and
		/// <code>column</code>, if the cell is editable.
		/// </summary>
		public bool editCellAt(int @row, int @column)
		{
			return default(bool);
		}

		/// <summary>
		/// Programmatically starts editing the cell at <code>row</code> and
		/// <code>column</code>, if the cell is editable.
		/// </summary>
		public bool editCellAt(int @row, int @column, EventObject @e)
		{
			return default(bool);
		}

		/// <summary>
		/// Invoked when editing is canceled.
		/// </summary>
		public void editingCanceled(ChangeEvent @e)
		{
		}

		/// <summary>
		/// Invoked when editing is finished.
		/// </summary>
		public void editingStopped(ChangeEvent @e)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JTable.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Determines whether the table will create default columns from the model.
		/// </summary>
		public bool getAutoCreateColumnsFromModel()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the auto resize mode of the table.
		/// </summary>
		public int getAutoResizeMode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the cell editor.
		/// </summary>
		public TableCellEditor getCellEditor()
		{
			return default(TableCellEditor);
		}

		/// <summary>
		/// Returns an appropriate editor for the cell specified by
		/// <code>row</code> and <code>column</code>.
		/// </summary>
		public TableCellEditor getCellEditor(int @row, int @column)
		{
			return default(TableCellEditor);
		}

		/// <summary>
		/// Returns a rectangle for the cell that lies at the intersection of
		/// <code>row</code> and <code>column</code>.
		/// </summary>
		public Rectangle getCellRect(int @row, int @column, bool @includeSpacing)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns an appropriate renderer for the cell specified by this row and
		/// column.
		/// </summary>
		public TableCellRenderer getCellRenderer(int @row, int @column)
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Returns true if both row and column selection models are enabled.
		/// </summary>
		public bool getCellSelectionEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the <code>TableColumn</code> object for the column in the table
		/// whose identifier is equal to <code>identifier</code>, when compared using
		/// <code>equals</code>.
		/// </summary>
		public TableColumn getColumn(object @identifier)
		{
			return default(TableColumn);
		}

		/// <summary>
		/// Returns the type of the column appearing in the view at
		/// column position <code>column</code>.
		/// </summary>
		public Class getColumnClass(int @column)
		{
			return default(Class);
		}

		/// <summary>
		/// Returns the number of columns in the column model.
		/// </summary>
		public int getColumnCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>TableColumnModel</code> that contains all column information
		/// of this table.
		/// </summary>
		public TableColumnModel getColumnModel()
		{
			return default(TableColumnModel);
		}

		/// <summary>
		/// Returns the name of the column appearing in the view at
		/// column position <code>column</code>.
		/// </summary>
		public string getColumnName(int @column)
		{
			return default(string);
		}

		/// <summary>
		/// Returns true if columns can be selected.
		/// </summary>
		public bool getColumnSelectionAllowed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the editor to be used when no editor has been set in
		/// a <code>TableColumn</code>.
		/// </summary>
		public TableCellEditor getDefaultEditor(Class @columnClass)
		{
			return default(TableCellEditor);
		}

		/// <summary>
		/// Returns the cell renderer to be used when no renderer has been set in
		/// a <code>TableColumn</code>.
		/// </summary>
		public TableCellRenderer getDefaultRenderer(Class @columnClass)
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Gets the value of the <code>dragEnabled</code> property.
		/// </summary>
		public bool getDragEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the index of the column that contains the cell currently
		/// being edited.
		/// </summary>
		public int getEditingColumn()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index of the row that contains the cell currently
		/// being edited.
		/// </summary>
		public int getEditingRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the component that is handling the editing session.
		/// </summary>
		public Component getEditorComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the color used to draw grid lines.
		/// </summary>
		public Color getGridColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the horizontal and vertical space between cells.
		/// </summary>
		public Dimension getIntercellSpacing()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the <code>TableModel</code> that provides the data displayed by this
		/// <code>JTable</code>.
		/// </summary>
		public TableModel getModel()
		{
			return default(TableModel);
		}

		/// <summary>
		/// Returns the preferred size of the viewport for this table.
		/// </summary>
		public Dimension getPreferredScrollableViewportSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the number of rows in this table's model.
		/// </summary>
		public int getRowCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the height of a table row, in pixels.
		/// </summary>
		public int getRowHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the height, in pixels, of the cells in <code>row</code>.
		/// </summary>
		public int getRowHeight(int @row)
		{
			return default(int);
		}

		/// <summary>
		/// Gets the amount of empty space, in pixels, between cells.
		/// </summary>
		public int getRowMargin()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if rows can be selected.
		/// </summary>
		public bool getRowSelectionAllowed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>visibleRect.height</code> or
		/// <code>visibleRect.width</code>,
		/// depending on this table's orientation.
		/// </summary>
		public int getScrollableBlockIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns false to indicate that the height of the viewport does not
		/// determine the height of the table.
		/// </summary>
		public bool getScrollableTracksViewportHeight()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns false if <code>autoResizeMode</code> is set to
		/// <code>AUTO_RESIZE_OFF</code>, which indicates that the
		/// width of the viewport does not determine the width
		/// of the table.
		/// </summary>
		public bool getScrollableTracksViewportWidth()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the scroll increment (in pixels) that completely exposes one new
		/// row or column (depending on the orientation).
		/// </summary>
		public int getScrollableUnitIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index of the first selected column,
		/// -1 if no column is selected.
		/// </summary>
		public int getSelectedColumn()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of selected columns.
		/// </summary>
		public int getSelectedColumnCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the indices of all selected columns.
		/// </summary>
		public int[] getSelectedColumns()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the index of the first selected row, -1 if no row is selected.
		/// </summary>
		public int getSelectedRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of selected rows.
		/// </summary>
		public int getSelectedRowCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the indices of all selected rows.
		/// </summary>
		public int[] getSelectedRows()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the background color for selected cells.
		/// </summary>
		public Color getSelectionBackground()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the foreground color for selected cells.
		/// </summary>
		public Color getSelectionForeground()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the <code>ListSelectionModel</code> that is used to maintain row
		/// selection state.
		/// </summary>
		public ListSelectionModel getSelectionModel()
		{
			return default(ListSelectionModel);
		}

		/// <summary>
		/// Returns true if the table draws horizontal lines between cells, false if it
		/// doesn't.
		/// </summary>
		public bool getShowHorizontalLines()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the table draws vertical lines between cells, false if it
		/// doesn't.
		/// </summary>
		public bool getShowVerticalLines()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the editor should get the focus
		/// when keystrokes cause the editor to be activated
		/// </summary>
		public bool getSurrendersFocusOnKeystroke()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the <code>tableHeader</code> used by this <code>JTable</code>.
		/// </summary>
		public JTableHeader getTableHeader()
		{
			return default(JTableHeader);
		}

		/// <summary>
		/// Overrides <code>JComponent</code>'s <code>getToolTipText</code>
		/// method in order to allow the renderer's tips to be used
		/// if it has text set.
		/// </summary>
		public string getToolTipText(MouseEvent @event)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public TableUI getUI()
		{
			return default(TableUI);
		}

		/// <summary>
		/// Returns the suffix used to construct the name of the L&F class used to
		/// render this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the cell value at <code>row</code> and <code>column</code>.
		/// </summary>
		public object getValueAt(int @row, int @column)
		{
			return default(object);
		}

		/// <summary>
		/// Initializes table properties to their default values.
		/// </summary>
		protected void initializeLocalVars()
		{
		}

		/// <summary>
		/// Returns true if the cell at <code>row</code> and <code>column</code>
		/// is editable.
		/// </summary>
		public bool isCellEditable(int @row, int @column)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the cell at the specified position is selected.
		/// </summary>
		public bool isCellSelected(int @row, int @column)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the column at the specified index is selected.
		/// </summary>
		public bool isColumnSelected(int @column)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if a cell is being edited.
		/// </summary>
		public bool isEditing()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the row at the specified index is selected.
		/// </summary>
		public bool isRowSelected(int @row)
		{
			return default(bool);
		}

		/// <summary>
		/// Moves the column <code>column</code> to the position currently
		/// occupied by the column <code>targetColumn</code> in the view.
		/// </summary>
		public void moveColumn(int @column, int @targetColumn)
		{
		}

		/// <summary>
		/// Returns a string representation of this table.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Prepares the editor by querying the data model for the value and
		/// selection state of the cell at <code>row</code>, <code>column</code>.
		/// </summary>
		public Component prepareEditor(TableCellEditor @editor, int @row, int @column)
		{
			return default(Component);
		}

		/// <summary>
		/// Prepares the renderer by querying the data model for the
		/// value and selection state
		/// of the cell at <code>row</code>, <code>column</code>.
		/// </summary>
		public Component prepareRenderer(TableCellRenderer @renderer, int @row, int @column)
		{
			return default(Component);
		}

		/// <summary>
		/// Invoked to process the key bindings for <code>ks</code> as the result
		/// of the <code>KeyEvent</code> <code>e</code>.
		/// </summary>
		protected bool processKeyBinding(KeyStroke @ks, KeyEvent @e, int @condition, bool @pressed)
		{
			return default(bool);
		}

		/// <summary>
		/// Removes <code>aColumn</code> from this <code>JTable</code>'s
		/// array of columns.
		/// </summary>
		public void removeColumn(TableColumn @aColumn)
		{
		}

		/// <summary>
		/// Deselects the columns from <code>index0</code> to <code>index1</code>, inclusive.
		/// </summary>
		public void removeColumnSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Discards the editor object and frees the real estate it used for
		/// cell rendering.
		/// </summary>
		public void removeEditor()
		{
		}

		/// <summary>
		/// Calls the <code>unconfigureEnclosingScrollPane</code> method.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Deselects the rows from <code>index0</code> to <code>index1</code>, inclusive.
		/// </summary>
		public void removeRowSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Equivalent to <code>revalidate</code> followed by <code>repaint</code>.
		/// </summary>
		protected void resizeAndRepaint()
		{
		}

		/// <summary>
		/// Returns the index of the row that <code>point</code> lies in,
		/// or -1 if the result is not in the range
		/// [0, <code>getRowCount()</code>-1].
		/// </summary>
		public int rowAtPoint(Point @point)
		{
			return default(int);
		}

		/// <summary>
		/// Selects all rows, columns, and cells in the table.
		/// </summary>
		public void selectAll()
		{
		}

		/// <summary>
		/// Sets this table's <code>autoCreateColumnsFromModel</code> flag.
		/// </summary>
		public void setAutoCreateColumnsFromModel(bool @autoCreateColumnsFromModel)
		{
		}

		/// <summary>
		/// Sets the table's auto resize mode when the table is resized.
		/// </summary>
		public void setAutoResizeMode(int @mode)
		{
		}

		/// <summary>
		/// Sets the <code>cellEditor</code> variable.
		/// </summary>
		public void setCellEditor(TableCellEditor @anEditor)
		{
		}

		/// <summary>
		/// Sets whether this table allows both a column selection and a
		/// row selection to exist simultaneously.
		/// </summary>
		public void setCellSelectionEnabled(bool @cellSelectionEnabled)
		{
		}

		/// <summary>
		/// Sets the column model for this table to <code>newModel</code> and registers
		/// for listener notifications from the new column model.
		/// </summary>
		public void setColumnModel(TableColumnModel @columnModel)
		{
		}

		/// <summary>
		/// Sets whether the columns in this model can be selected.
		/// </summary>
		public void setColumnSelectionAllowed(bool @columnSelectionAllowed)
		{
		}

		/// <summary>
		/// Selects the columns from <code>index0</code> to <code>index1</code>,
		/// inclusive.
		/// </summary>
		public void setColumnSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Sets a default cell editor to be used if no editor has been set in
		/// a <code>TableColumn</code>.
		/// </summary>
		public void setDefaultEditor(Class @columnClass, TableCellEditor @editor)
		{
		}

		/// <summary>
		/// Sets a default cell renderer to be used if no renderer has been set in
		/// a <code>TableColumn</code>.
		/// </summary>
		public void setDefaultRenderer(Class @columnClass, TableCellRenderer @renderer)
		{
		}

		/// <summary>
		/// Sets the <code>dragEnabled</code> property,
		/// which must be <code>true</code> to enable
		/// automatic drag handling (the first part of drag and drop)
		/// on this component.
		/// </summary>
		public void setDragEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>editingColumn</code> variable.
		/// </summary>
		public void setEditingColumn(int @aColumn)
		{
		}

		/// <summary>
		/// Sets the <code>editingRow</code> variable.
		/// </summary>
		public void setEditingRow(int @aRow)
		{
		}

		/// <summary>
		/// Sets the color used to draw grid lines to <code>gridColor</code> and redisplays.
		/// </summary>
		public void setGridColor(Color @gridColor)
		{
		}

		/// <summary>
		/// Sets the <code>rowMargin</code> and the <code>columnMargin</code> --
		/// the height and width of the space between cells -- to
		/// <code>intercellSpacing</code>.
		/// </summary>
		public void setIntercellSpacing(Dimension @intercellSpacing)
		{
		}

		/// <summary>
		/// Sets the data model for this table to <code>newModel</code> and registers
		/// with it for listener notifications from the new data model.
		/// </summary>
		public void setModel(TableModel @dataModel)
		{
		}

		/// <summary>
		/// Sets the preferred size of the viewport for this table.
		/// </summary>
		public void setPreferredScrollableViewportSize(Dimension @size)
		{
		}

		/// <summary>
		/// Sets the height, in pixels, of all cells to <code>rowHeight</code>,
		/// revalidates, and repaints.
		/// </summary>
		public void setRowHeight(int @rowHeight)
		{
		}

		/// <summary>
		/// Sets the height for <code>row</code> to <code>rowHeight</code>,
		/// revalidates, and repaints.
		/// </summary>
		public void setRowHeight(int @row, int @rowHeight)
		{
		}

		/// <summary>
		/// Sets the amount of empty space between cells in adjacent rows.
		/// </summary>
		public void setRowMargin(int @rowMargin)
		{
		}

		/// <summary>
		/// Sets whether the rows in this model can be selected.
		/// </summary>
		public void setRowSelectionAllowed(bool @rowSelectionAllowed)
		{
		}

		/// <summary>
		/// Selects the rows from <code>index0</code> to <code>index1</code>,
		/// inclusive.
		/// </summary>
		public void setRowSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Sets the background color for selected cells.
		/// </summary>
		public void setSelectionBackground(Color @selectionBackground)
		{
		}

		/// <summary>
		/// Sets the foreground color for selected cells.
		/// </summary>
		public void setSelectionForeground(Color @selectionForeground)
		{
		}

		/// <summary>
		/// Sets the table's selection mode to allow only single selections, a single
		/// contiguous interval, or multiple intervals.
		/// </summary>
		public void setSelectionMode(int @selectionMode)
		{
		}

		/// <summary>
		/// Sets the row selection model for this table to <code>newModel</code>
		/// and registers for listener notifications from the new selection model.
		/// </summary>
		public void setSelectionModel(ListSelectionModel @newModel)
		{
		}

		/// <summary>
		/// Sets whether the table draws grid lines around cells.
		/// </summary>
		public void setShowGrid(bool @showGrid)
		{
		}

		/// <summary>
		/// Sets whether the table draws horizontal lines between cells.
		/// </summary>
		public void setShowHorizontalLines(bool @showHorizontalLines)
		{
		}

		/// <summary>
		/// Sets whether the table draws vertical lines between cells.
		/// </summary>
		public void setShowVerticalLines(bool @showVerticalLines)
		{
		}

		/// <summary>
		/// Sets whether editors in this JTable get the keyboard focus
		/// when an editor is activated as a result of the JTable
		/// forwarding keyboard events for a cell.
		/// </summary>
		public void setSurrendersFocusOnKeystroke(bool @surrendersFocusOnKeystroke)
		{
		}

		/// <summary>
		/// Sets the <code>tableHeader</code> working with this <code>JTable</code> to <code>newHeader</code>.
		/// </summary>
		public void setTableHeader(JTableHeader @tableHeader)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component and repaints.
		/// </summary>
		public void setUI(TableUI @ui)
		{
		}

		/// <summary>
		/// Sets the value for the cell in the table model at <code>row</code>
		/// and <code>column</code>.
		/// </summary>
		public void setValueAt(object @aValue, int @row, int @column)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Swing version 1.0.3,
		/// replaced by <code>doLayout()</code>.</I>
		/// </summary>
		public void sizeColumnsToFit(bool @lastColumnOnly)
		{
		}

		/// <summary>
		/// Obsolete as of Java 2 platform v1.4.
		/// </summary>
		public void sizeColumnsToFit(int @resizingColumn)
		{
		}

		/// <summary>
		/// Invoked when this table's <code>TableModel</code> generates
		/// a <code>TableModelEvent</code>.
		/// </summary>
		public void tableChanged(TableModelEvent @e)
		{
		}

		/// <summary>
		/// Reverses the effect of <code>configureEnclosingScrollPane</code>
		/// by replacing the <code>columnHeaderView</code> of the enclosing
		/// scroll pane with <code>null</code>.
		/// </summary>
		protected void unconfigureEnclosingScrollPane()
		{
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the L&F has changed.
		/// </summary>
		public void updateUI()
		{
		}

		/// <summary>
		/// Invoked when the row selection changes -- repaints to show the new
		/// selection.
		/// </summary>
		public void valueChanged(ListSelectionEvent @e)
		{
		}

	}
}
