// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Document

using ScriptCoreLib;
using java.lang;
using javax.swing.@event;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Document.html
	[Script(IsNative = true)]
	public interface Document
	{
		/// <summary>
		/// Registers the given observer to begin receiving notifications
		/// when changes are made to the document.
		/// </summary>
		void addDocumentListener(DocumentListener @listener);

		/// <summary>
		/// Registers the given observer to begin receiving notifications
		/// when undoable edits are made to the document.
		/// </summary>
		void addUndoableEditListener(UndoableEditListener @listener);

		/// <summary>
		/// This method allows an application to mark a place in
		/// a sequence of character content.
		/// </summary>
		Position createPosition(int @offs);

		/// <summary>
		/// Returns the root element that views should be based upon,
		/// unless some other mechanism for assigning views to element
		/// structures is provided.
		/// </summary>
		Element getDefaultRootElement();

		/// <summary>
		/// Returns a position that represents the end of the document.
		/// </summary>
		Position getEndPosition();

		/// <summary>
		/// Returns number of characters of content currently
		/// in the document.
		/// </summary>
		int getLength();

		/// <summary>
		/// Gets the properties associated with the document.
		/// </summary>
		object getProperty(object @key);

		/// <summary>
		/// Returns all of the root elements that are defined.
		/// </summary>
		Element[] getRootElements();

		/// <summary>
		/// Returns a position that represents the start of the document.
		/// </summary>
		Position getStartPosition();

		/// <summary>
		/// Fetches the text contained within the given portion
		/// of the document.
		/// </summary>
		string getText(int @offset, int @length);

		/// <summary>
		/// Fetches the text contained within the given portion
		/// of the document.
		/// </summary>
		void getText(int @offset, int @length, Segment @txt);

		/// <summary>
		/// Inserts a string of content.
		/// </summary>
		void insertString(int @offset, string @str, AttributeSet @a);

		/// <summary>
		/// Associates a property with the document.
		/// </summary>
		void putProperty(object @key, object @value);

		/// <summary>
		/// Removes a portion of the content of the document.
		/// </summary>
		void remove(int @offs, int @len);

		/// <summary>
		/// Unregisters the given observer from the notification list
		/// so it will no longer receive change updates.
		/// </summary>
		void removeDocumentListener(DocumentListener @listener);

		/// <summary>
		/// Unregisters the given observer from the notification list
		/// so it will no longer receive updates.
		/// </summary>
		void removeUndoableEditListener(UndoableEditListener @listener);

		/// <summary>
		/// This allows the model to be safely rendered in the presence
		/// of currency, if the model supports being updated asynchronously.
		/// </summary>
		void render(Runnable @r);

	}
}
