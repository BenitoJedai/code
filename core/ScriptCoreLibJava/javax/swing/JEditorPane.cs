// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JEditorPane

using ScriptCoreLib;
using java.awt;
using java.io;
using java.lang;
using java.net;
using javax.accessibility;
using javax.swing.@event;
using javax.swing.text;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JEditorPane.html
	[Script(IsNative = true)]
	public class JEditorPane : JTextComponent
	{
		/// <summary>
		/// Creates a new <code>JEditorPane</code>.
		/// </summary>
		public JEditorPane()
		{
		}

		/// <summary>
		/// Creates a <code>JEditorPane</code> based on a string containing
		/// a URL specification.
		/// </summary>
		public JEditorPane(string @url)
		{
		}

		/// <summary>
		/// Creates a <code>JEditorPane</code> that has been initialized
		/// to the given text.
		/// </summary>
		public JEditorPane(string @type, string @text)
		{
		}

		/// <summary>
		/// Creates a <code>JEditorPane</code> based on a specified URL for input.
		/// </summary>
		public JEditorPane(URL @initialPage)
		{
		}

		/// <summary>
		/// Adds a hyperlink listener for notification of any changes, for example
		/// when a link is selected and entered.
		/// </summary>
		public void addHyperlinkListener(HyperlinkListener @listener)
		{
		}

		/// <summary>
		/// Creates the default editor kit (<code>PlainEditorKit</code>) for when
		/// the component is first created.
		/// </summary>
		protected EditorKit createDefaultEditorKit()
		{
			return default(EditorKit);
		}

		/// <summary>
		/// Creates a handler for the given type from the default registry
		/// of editor kits.
		/// </summary>
		static public EditorKit createEditorKitForContentType(string @type)
		{
			return default(EditorKit);
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		public void fireHyperlinkUpdate(HyperlinkEvent @e)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JEditorPane.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Gets the type of content that this editor
		/// is currently set to deal with.
		/// </summary>
		public string getContentType()
		{
			return default(string);
		}

		/// <summary>
		/// Fetches the currently installed kit for handling content.
		/// </summary>
		public EditorKit getEditorKit()
		{
			return default(EditorKit);
		}

		/// <summary>
		/// Returns the currently registered <code>EditorKit</code>
		/// class name for the type <code>type</code>.
		/// </summary>
		static public string getEditorKitClassNameForContentType(string @type)
		{
			return default(string);
		}

		/// <summary>
		/// Fetches the editor kit to use for the given type
		/// of content.
		/// </summary>
		public EditorKit getEditorKitForContentType(string @type)
		{
			return default(EditorKit);
		}

		/// <summary>
		/// Returns an array of all the <code>HyperLinkListener</code>s added
		/// to this JEditorPane with addHyperlinkListener().
		/// </summary>
		public HyperlinkListener[] getHyperlinkListeners()
		{
			return default(HyperlinkListener[]);
		}

		/// <summary>
		/// Gets the current URL being displayed.
		/// </summary>
		public URL getPage()
		{
			return default(URL);
		}

		/// <summary>
		/// Returns the preferred size for the <code>JEditorPane</code>.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
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
		/// Fetches a stream for the given URL, which is about to
		/// be loaded by the <code>setPage</code> method.
		/// </summary>
		protected InputStream getStream(URL @page)
		{
			return default(InputStream);
		}

		/// <summary>
		/// Returns the text contained in this <code>TextComponent</code>
		/// in terms of the
		/// content type of this editor.
		/// </summary>
		public string getText()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the class ID for the UI.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns a string representation of this <code>JEditorPane</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// This method initializes from a stream.
		/// </summary>
		public void read(InputStream @in, object @desc)
		{
		}

		/// <summary>
		/// Establishes the default bindings of <code>type</code> to
		/// <code>classname</code>.
		/// </summary>
		static public void registerEditorKitForContentType(string @type, string @classname)
		{
		}

		/// <summary>
		/// Establishes the default bindings of <code>type</code> to
		/// <code>classname</code>.
		/// </summary>
		static public void registerEditorKitForContentType(string @type, string @classname, ClassLoader @loader)
		{
		}

		/// <summary>
		/// Removes a hyperlink listener.
		/// </summary>
		public void removeHyperlinkListener(HyperlinkListener @listener)
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
		/// Scrolls the view to the given reference location
		/// (that is, the value returned by the <code>UL.getRef</code>
		/// method for the URL being displayed).
		/// </summary>
		public void scrollToReference(string @reference)
		{
		}

		/// <summary>
		/// Sets the type of content that this editor
		/// handles.
		/// </summary>
		public void setContentType(string @type)
		{
		}

		/// <summary>
		/// Sets the currently installed kit for handling
		/// content.
		/// </summary>
		public void setEditorKit(EditorKit @kit)
		{
		}

		/// <summary>
		/// Directly sets the editor kit to use for the given type.
		/// </summary>
		public void setEditorKitForContentType(string @type, EditorKit @k)
		{
		}

		/// <summary>
		/// Sets the current URL being displayed.
		/// </summary>
		public void setPage(string @url)
		{
		}

		/// <summary>
		/// Sets the current URL being displayed.
		/// </summary>
		public void setPage(URL @page)
		{
		}

		/// <summary>
		/// Sets the text of this <code>TextComponent</code> to the specified
		/// content,
		/// which is expected to be in the format of the content type of
		/// this editor.
		/// </summary>
		public void setText(string @t)
		{
		}

	}
}
