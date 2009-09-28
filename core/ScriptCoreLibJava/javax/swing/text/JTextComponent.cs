// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.JTextComponent

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.awt.im;
using java.io;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/JTextComponent.html
	[Script(IsNative = true)]
	public abstract class JTextComponent : JComponent
	{
		/// <summary>
		/// Creates a new <code>JTextComponent</code>.
		/// </summary>
		public JTextComponent()
		{
		}

		/// <summary>
		/// Adds a caret listener for notification of any changes
		/// to the caret.
		/// </summary>
		public void addCaretListener(CaretListener @listener)
		{
		}

		/// <summary>
		/// Adds the specified input method listener to receive
		/// input method events from this component.
		/// </summary>
		public void addInputMethodListener(InputMethodListener @l)
		{
		}

		/// <summary>
		/// Adds a new keymap into the keymap hierarchy.
		/// </summary>
		static public Keymap addKeymap(string @nm, Keymap @parent)
		{
			return default(Keymap);
		}

		/// <summary>
		/// Transfers the currently selected range in the associated
		/// text model to the system clipboard, leaving the contents
		/// in the text model.
		/// </summary>
		public void copy()
		{
		}

		/// <summary>
		/// Transfers the currently selected range in the associated
		/// text model to the system clipboard, removing the contents
		/// from the model.
		/// </summary>
		public void cut()
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireCaretUpdate(CaretEvent @e)
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JTextComponent</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Fetches the command list for the editor.
		/// </summary>
		public Action[] getActions()
		{
			return default(Action[]);
		}

		/// <summary>
		/// Fetches the caret that allows text-oriented navigation over
		/// the view.
		/// </summary>
		public Caret getCaret()
		{
			return default(Caret);
		}

		/// <summary>
		/// Fetches the current color used to render the
		/// caret.
		/// </summary>
		public Color getCaretColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns an array of all the caret listeners
		/// registered on this text component.
		/// </summary>
		public CaretListener[] getCaretListeners()
		{
			return default(CaretListener[]);
		}

		/// <summary>
		/// Returns the position of the text insertion caret for the
		/// text component.
		/// </summary>
		public int getCaretPosition()
		{
			return default(int);
		}

		/// <summary>
		/// Fetches the current color used to render the
		/// selected text.
		/// </summary>
		public Color getDisabledTextColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Fetches the model associated with the editor.
		/// </summary>
		public Document getDocument()
		{
			return default(Document);
		}

		/// <summary>
		/// Gets the <code>dragEnabled</code> property.
		/// </summary>
		public bool getDragEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the key accelerator that will cause the receiving
		/// text component to get the focus.
		/// </summary>
		public char getFocusAccelerator()
		{
			return default(char);
		}

		/// <summary>
		/// Fetches the object responsible for making highlights.
		/// </summary>
		public Highlighter getHighlighter()
		{
			return default(Highlighter);
		}

		/// <summary>
		/// Gets the input method request handler which supports
		/// requests from input methods for this component.
		/// </summary>
		public InputMethodRequests getInputMethodRequests()
		{
			return default(InputMethodRequests);
		}

		/// <summary>
		/// Fetches the keymap currently active in this text
		/// component.
		/// </summary>
		public Keymap getKeymap()
		{
			return default(Keymap);
		}

		/// <summary>
		/// Fetches a named keymap previously added to the document.
		/// </summary>
		static public Keymap getKeymap(string @nm)
		{
			return default(Keymap);
		}

		/// <summary>
		/// Returns the margin between the text component's border and
		/// its text.
		/// </summary>
		public Insets getMargin()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns the <code>NavigationFilter</code>.
		/// </summary>
		public NavigationFilter getNavigationFilter()
		{
			return default(NavigationFilter);
		}

		/// <summary>
		/// Returns the preferred size of the viewport for a view component.
		/// </summary>
		public Dimension getPreferredScrollableViewportSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Components that display logical rows or columns should compute
		/// the scroll increment that will completely expose one block
		/// of rows or columns, depending on the value of orientation.
		/// </summary>
		public int getScrollableBlockIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if a viewport should always force the height of this
		/// <code>Scrollable</code> to match the height of the viewport.
		/// </summary>
		public bool getScrollableTracksViewportHeight()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if a viewport should always force the width of this
		/// <code>Scrollable</code> to match the width of the viewport.
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
		/// Returns the selected text contained in this
		/// <code>TextComponent</code>.
		/// </summary>
		public string getSelectedText()
		{
			return default(string);
		}

		/// <summary>
		/// Fetches the current color used to render the
		/// selected text.
		/// </summary>
		public Color getSelectedTextColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Fetches the current color used to render the
		/// selection.
		/// </summary>
		public Color getSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the selected text's end position.
		/// </summary>
		public int getSelectionEnd()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the selected text's start position.
		/// </summary>
		public int getSelectionStart()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the text contained in this <code>TextComponent</code>.
		/// </summary>
		public string getText()
		{
			return default(string);
		}

		/// <summary>
		/// Fetches a portion of the text represented by the
		/// component.
		/// </summary>
		public string getText(int @offs, int @len)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string to be used as the tooltip for <code>event</code>.
		/// </summary>
		public string getToolTipText(MouseEvent @event)
		{
			return default(string);
		}

		/// <summary>
		/// Fetches the user-interface factory for this text-oriented editor.
		/// </summary>
		public TextUI getUI()
		{
			return default(TextUI);
		}

		/// <summary>
		/// Returns the boolean indicating whether this
		/// <code>TextComponent</code> is editable or not.
		/// </summary>
		public bool isEditable()
		{
			return default(bool);
		}



		/// <summary>
		/// Converts the given location in the model to a place in
		/// the view coordinate system.
		/// </summary>
		public Rectangle modelToView(int @pos)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Moves the caret to a new position, leaving behind a mark
		/// defined by the last time <code>setCaretPosition</code> was
		/// called.
		/// </summary>
		public void moveCaretPosition(int @pos)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JTextComponent</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Transfers the contents of the system clipboard into the
		/// associated text model.
		/// </summary>
		public void paste()
		{
		}

		/// <summary>
		/// Processes input method events occurring on this component by
		/// dispatching them to any registered
		/// <code>InputMethodListener</code> objects.
		/// </summary>
		protected void processInputMethodEvent(InputMethodEvent @e)
		{
		}

		/// <summary>
		/// Initializes from a stream.
		/// </summary>
		public void read(Reader @in, object @desc)
		{
		}

		/// <summary>
		/// Removes a caret listener.
		/// </summary>
		public void removeCaretListener(CaretListener @listener)
		{
		}

		/// <summary>
		/// Removes a named keymap previously added to the document.
		/// </summary>
		static public Keymap removeKeymap(string @nm)
		{
			return default(Keymap);
		}

		/// <summary>
		/// Notifies this component that it no longer has a parent component.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Replaces the currently selected content with new content
		/// represented by the given string.
		/// </summary>
		public void replaceSelection(string @content)
		{
		}

		/// <summary>
		/// Selects the text between the specified start and end positions.
		/// </summary>
		public void select(int @selectionStart, int @selectionEnd)
		{
		}

		/// <summary>
		/// Selects all the text in the <code>TextComponent</code>.
		/// </summary>
		public void selectAll()
		{
		}

		/// <summary>
		/// Sets the caret to be used.
		/// </summary>
		public void setCaret(Caret @c)
		{
		}

		/// <summary>
		/// Sets the current color used to render the caret.
		/// </summary>
		public void setCaretColor(Color @c)
		{
		}

		/// <summary>
		/// Sets the position of the text insertion caret for the
		/// <code>TextComponent</code>.
		/// </summary>
		public void setCaretPosition(int @position)
		{
		}

		/// <summary>
		/// Sets the language-sensitive orientation that is to be used to order
		/// the elements or text within this component.
		/// </summary>
		public void setComponentOrientation(ComponentOrientation @o)
		{
		}

		/// <summary>
		/// Sets the current color used to render the
		/// disabled text.
		/// </summary>
		public void setDisabledTextColor(Color @c)
		{
		}

		/// <summary>
		/// Associates the editor with a text document.
		/// </summary>
		public void setDocument(Document @doc)
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
		/// Sets the specified boolean to indicate whether or not this
		/// <code>TextComponent</code> should be editable.
		/// </summary>
		public void setEditable(bool @b)
		{
		}

		/// <summary>
		/// Sets the key accelerator that will cause the receiving text
		/// component to get the focus.
		/// </summary>
		public void setFocusAccelerator(char @aKey)
		{
		}

		/// <summary>
		/// Sets the highlighter to be used.
		/// </summary>
		public void setHighlighter(Highlighter @h)
		{
		}

		/// <summary>
		/// Sets the keymap to use for binding events to
		/// actions.
		/// </summary>
		public void setKeymap(Keymap @map)
		{
		}

		/// <summary>
		/// Sets margin space between the text component's border
		/// and its text.
		/// </summary>
		public void setMargin(Insets @m)
		{
		}

		/// <summary>
		/// Sets the <code>NavigationFilter</code>.
		/// </summary>
		public void setNavigationFilter(NavigationFilter @filter)
		{
		}

		/// <summary>
		/// Sets the current color used to render the selected text.
		/// </summary>
		public void setSelectedTextColor(Color @c)
		{
		}

		/// <summary>
		/// Sets the current color used to render the selection.
		/// </summary>
		public void setSelectionColor(Color @c)
		{
		}

		/// <summary>
		/// Sets the selection end to the specified position.
		/// </summary>
		public void setSelectionEnd(int @selectionEnd)
		{
		}

		/// <summary>
		/// Sets the selection start to the specified position.
		/// </summary>
		public void setSelectionStart(int @selectionStart)
		{
		}

		/// <summary>
		/// Sets the text of this <code>TextComponent</code>
		/// to the specified text.
		/// </summary>
		public void setText(string @t)
		{
		}

		/// <summary>
		/// Sets the user-interface factory for this text-oriented editor.
		/// </summary>
		public void setUI(TextUI @ui)
		{
		}

		/// <summary>
		/// Reloads the pluggable UI.
		/// </summary>
		public void updateUI()
		{
		}

		/// <summary>
		/// Converts the given place in the view coordinate system
		/// to the nearest representative location in the model.
		/// </summary>
		public int viewToModel(Point @pt)
		{
			return default(int);
		}

		/// <summary>
		/// Stores the contents of the model into the given
		/// stream.
		/// </summary>
		public void write(Writer @out)
		{
		}

	}
}
