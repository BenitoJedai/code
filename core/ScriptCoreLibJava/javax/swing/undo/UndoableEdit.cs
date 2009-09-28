// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.undo.UndoableEdit

using ScriptCoreLib;
using java.lang;

namespace javax.swing.undo
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/undo/UndoableEdit.html
	[Script(IsNative = true)]
	public interface UndoableEdit
	{
		/// <summary>
		/// This <code>UndoableEdit</code> should absorb <code>anEdit</code>
		/// if it can.
		/// </summary>
		bool addEdit(UndoableEdit @anEdit);

		/// <summary>
		/// True if it is still possible to redo this operation.
		/// </summary>
		bool canRedo();

		/// <summary>
		/// True if it is still possible to undo this operation.
		/// </summary>
		bool canUndo();

		/// <summary>
		/// May be sent to inform an edit that it should no longer be
		/// used.
		/// </summary>
		void die();

		/// <summary>
		/// Provides a localized, human readable description of this edit
		/// suitable for use in, say, a change log.
		/// </summary>
		string getPresentationName();

		/// <summary>
		/// Provides a localized, human readable description of the redoable
		/// form of this edit, e.g.
		/// </summary>
		string getRedoPresentationName();

		/// <summary>
		/// Provides a localized, human readable description of the undoable
		/// form of this edit, e.g.
		/// </summary>
		string getUndoPresentationName();

		/// <summary>
		/// Returns false if this edit is insignificant--for example one
		/// that maintains the user's selection, but does not change any
		/// model state.
		/// </summary>
		bool isSignificant();

		/// <summary>
		/// Re-apply the edit, assuming that it has been undone.
		/// </summary>
		void redo();

		/// <summary>
		/// Returns true if this <code>UndoableEdit</code> should replace
		/// <code>anEdit</code>.
		/// </summary>
		bool replaceEdit(UndoableEdit @anEdit);

		/// <summary>
		/// Undo the edit that was made.
		/// </summary>
		void undo();

	}
}
