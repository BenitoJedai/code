// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/SingleSelectionModel.html
	[Script(IsNative = true)]
	public interface SingleSelectionModel
	{
		/// <summary>
		/// Adds <I>listener</I> as a listener to changes in the model.
		/// </summary>
		void addChangeListener(ChangeListener @listener);

		/// <summary>
		/// Clears the selection (to -1).
		/// </summary>
		void clearSelection();

		/// <summary>
		/// Returns the model's selection.
		/// </summary>
		int getSelectedIndex();

		/// <summary>
		/// Returns true if the selection model currently has a selected value.
		/// </summary>
		bool isSelected();

		/// <summary>
		/// Removes <I>listener</I> as a listener to changes in the model.
		/// </summary>
		void removeChangeListener(ChangeListener @listener);

		/// <summary>
		/// Sets the model's selected index to <I>index</I>.
		/// </summary>
		void setSelectedIndex(int @index);

	}
}

