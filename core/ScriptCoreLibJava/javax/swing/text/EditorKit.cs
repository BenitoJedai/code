// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.EditorKit

using ScriptCoreLib;
using java.io;
using java.lang;
using javax.swing;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/EditorKit.html
	[Script(IsNative = true)]
	public abstract class EditorKit
	{
		/// <summary>
		/// Construct an EditorKit.
		/// </summary>
		public EditorKit()
		{
		}

		/// <summary>
		/// Creates a copy of the editor kit.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Fetches a caret that can navigate through views
		/// produced by the associated ViewFactory.
		/// </summary>
		abstract public Caret createCaret();

		/// <summary>
		/// Creates an uninitialized text storage model
		/// that is appropriate for this type of editor.
		/// </summary>
		abstract public Document createDefaultDocument();

		/// <summary>
		/// Called when the kit is being removed from the
		/// JEditorPane.
		/// </summary>
		public void deinstall(JEditorPane @c)
		{
		}

		/// <summary>
		/// Fetches the set of commands that can be used
		/// on a text component that is using a model and
		/// view produced by this kit.
		/// </summary>
		abstract public Action[] getActions();

		/// <summary>
		/// Gets the MIME type of the data that this
		/// kit represents support for.
		/// </summary>
		abstract public string getContentType();

		/// <summary>
		/// Fetches a factory that is suitable for producing
		/// views of any models that are produced by this
		/// kit.
		/// </summary>
		abstract public ViewFactory getViewFactory();

		/// <summary>
		/// Called when the kit is being installed into the
		/// a JEditorPane.
		/// </summary>
		public void install(JEditorPane @c)
		{
		}

		/// <summary>
		/// Inserts content from the given stream which is expected
		/// to be in a format appropriate for this kind of content
		/// handler.
		/// </summary>
		abstract public void read(InputStream @in, Document @doc, int @pos);

		/// <summary>
		/// Inserts content from the given stream which is expected
		/// to be in a format appropriate for this kind of content
		/// handler.
		/// </summary>
		abstract public void read(Reader @in, Document @doc, int @pos);

		/// <summary>
		/// Writes content from a document to the given stream
		/// in a format appropriate for this kind of content handler.
		/// </summary>
		abstract public void write(OutputStream @out, Document @doc, int @pos, int @len);

		/// <summary>
		/// Writes content from a document to the given stream
		/// in a format appropriate for this kind of content handler.
		/// </summary>
		abstract public void write(Writer @out, Document @doc, int @pos, int @len);

	}
}
