// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JTextArea

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing.text;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JTextArea.html
	[Script(IsNative = true)]
	public class JTextArea : JTextComponent
	{
		/// <summary>
		/// Constructs a new TextArea.
		/// </summary>
		public JTextArea()
		{
		}

		/// <summary>
		/// Constructs a new JTextArea with the given document model, and defaults
		/// for all of the other arguments (null, 0, 0).
		/// </summary>
		public JTextArea(Document @doc)
		{
		}

		/// <summary>
		/// Constructs a new JTextArea with the specified number of rows
		/// and columns, and the given model.
		/// </summary>
		public JTextArea(Document @doc, string @text, int @rows, int @columns)
		{
		}

		/// <summary>
		/// Constructs a new empty TextArea with the specified number of
		/// rows and columns.
		/// </summary>
		public JTextArea(int @rows, int @columns)
		{
		}

		/// <summary>
		/// Constructs a new TextArea with the specified text displayed.
		/// </summary>
		public JTextArea(string @text)
		{
		}

		/// <summary>
		/// Constructs a new TextArea with the specified text and number
		/// of rows and columns.
		/// </summary>
		public JTextArea(string @text, int @rows, int @columns)
		{
		}

		/// <summary>
		/// Appends the given text to the end of the document.
		/// </summary>
		public void append(string @str)
		{
		}

		/// <summary>
		/// Creates the default implementation of the model
		/// to be used at construction if one isn't explicitly
		/// given.
		/// </summary>
		protected Document createDefaultModel()
		{
			return default(Document);
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JTextArea.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the number of columns in the TextArea.
		/// </summary>
		public int getColumns()
		{
			return default(int);
		}

		/// <summary>
		/// Gets column width.
		/// </summary>
		protected int getColumnWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the number of lines contained in the area.
		/// </summary>
		public int getLineCount()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the offset of the end of the given line.
		/// </summary>
		public int getLineEndOffset(int @line)
		{
			return default(int);
		}

		/// <summary>
		/// Translates an offset into the components text to a
		/// line number.
		/// </summary>
		public int getLineOfOffset(int @offset)
		{
			return default(int);
		}

		/// <summary>
		/// Determines the offset of the start of the given line.
		/// </summary>
		public int getLineStartOffset(int @line)
		{
			return default(int);
		}

		/// <summary>
		/// Gets the line-wrapping policy of the text area.
		/// </summary>
		public bool getLineWrap()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the preferred size of the viewport if this component
		/// is embedded in a JScrollPane.
		/// </summary>
		public Dimension getPreferredScrollableViewportSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the preferred size of the TextArea.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Defines the meaning of the height of a row.
		/// </summary>
		protected int getRowHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of rows in the TextArea.
		/// </summary>
		public int getRows()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if a viewport should always force the width of this
		/// Scrollable to match the width of the viewport.
		/// </summary>
		public bool getScrollableTracksViewportWidth()
		{
			return default(bool);
		}

		/// <summary>
		/// Components that display logical rows or columns should compute
		/// the scroll increment that will completely expose one new row
		/// or column, depending on the value of orientation.
		/// </summary>
		public int getScrollableUnitIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Gets the number of characters used to expand tabs.
		/// </summary>
		public int getTabSize()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the class ID for the UI.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the style of wrapping used if the text area is wrapping
		/// lines.
		/// </summary>
		public bool getWrapStyleWord()
		{
			return default(bool);
		}

		/// <summary>
		/// Inserts the specified text at the specified position.
		/// </summary>
		public void insert(string @str, int @pos)
		{
		}

		/// <summary>
		/// Returns a string representation of this JTextArea.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Replaces text from the indicated start to end position with the
		/// new text specified.
		/// </summary>
		public void replaceRange(string @str, int @start, int @end)
		{
		}

		/// <summary>
		/// Sets the number of columns for this TextArea.
		/// </summary>
		public void setColumns(int @columns)
		{
		}

		/// <summary>
		/// Sets the current font.
		/// </summary>
		public void setFont(Font @f)
		{
		}

		/// <summary>
		/// Sets the line-wrapping policy of the text area.
		/// </summary>
		public void setLineWrap(bool @wrap)
		{
		}

		/// <summary>
		/// Sets the number of rows for this TextArea.
		/// </summary>
		public void setRows(int @rows)
		{
		}

		/// <summary>
		/// Sets the number of characters to expand tabs to.
		/// </summary>
		public void setTabSize(int @size)
		{
		}

		/// <summary>
		/// Sets the style of wrapping used if the text area is wrapping
		/// lines.
		/// </summary>
		public void setWrapStyleWord(bool @word)
		{
		}

	}
}
