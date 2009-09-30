// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.ListSelectionModel

using ScriptCoreLib;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/ListSelectionModel.html
	[Script(IsNative = true)]
	public interface ListSelectionModel
	{
		/// <summary>
		/// Add a listener to the list that's notified each time a change
		/// to the selection occurs.
		/// </summary>
		void addListSelectionListener(ListSelectionListener @x);

		/// <summary>
		/// Change the selection to be the set union of the current selection
		/// and the indices between index0 and index1 inclusive.
		/// </summary>
		void addSelectionInterval(int @index0, int @index1);

		/// <summary>
		/// Change the selection to the empty set.
		/// </summary>
		void clearSelection();

		/// <summary>
		/// Return the first index argument from the most recent call to
		/// setSelectionInterval(), addSelectionInterval() or removeSelectionInterval().
		/// </summary>
		int getAnchorSelectionIndex();

		/// <summary>
		/// Return the second index argument from the most recent call to
		/// setSelectionInterval(), addSelectionInterval() or removeSelectionInterval().
		/// </summary>
		int getLeadSelectionIndex();

		/// <summary>
		/// Returns the last selected index or -1 if the selection is empty.
		/// </summary>
		int getMaxSelectionIndex();

		/// <summary>
		/// Returns the first selected index or -1 if the selection is empty.
		/// </summary>
		int getMinSelectionIndex();

		/// <summary>
		/// Returns the current selection mode.
		/// </summary>
		int getSelectionMode();

		/// <summary>
		/// Returns true if the value is undergoing a series of changes.
		/// </summary>
		bool getValueIsAdjusting();

		/// <summary>
		/// Insert length indices beginning before/after index.
		/// </summary>
		void insertIndexInterval(int @index, int @length, bool @before);

		/// <summary>
		/// Returns true if the specified index is selected.
		/// </summary>
		bool isSelectedIndex(int @index);

		/// <summary>
		/// Returns true if no indices are selected.
		/// </summary>
		bool isSelectionEmpty();

		/// <summary>
		/// Remove the indices in the interval index0,index1 (inclusive) from
		/// the selection model.
		/// </summary>
		void removeIndexInterval(int @index0, int @index1);

		/// <summary>
		/// Remove a listener from the list that's notified each time a
		/// change to the selection occurs.
		/// </summary>
		void removeListSelectionListener(ListSelectionListener @x);

		/// <summary>
		/// Change the selection to be the set difference of the current selection
		/// and the indices between index0 and index1 inclusive.
		/// </summary>
		void removeSelectionInterval(int @index0, int @index1);

		/// <summary>
		/// Set the anchor selection index.
		/// </summary>
		void setAnchorSelectionIndex(int @index);

		/// <summary>
		/// Set the lead selection index.
		/// </summary>
		void setLeadSelectionIndex(int @index);

		/// <summary>
		/// Change the selection to be between index0 and index1 inclusive.
		/// </summary>
		void setSelectionInterval(int @index0, int @index1);

		/// <summary>
		/// Set the selection mode.
		/// </summary>
		void setSelectionMode(int @selectionMode);

		/// <summary>
		/// This property is true if upcoming changes to the value
		/// of the model should be considered a single event.
		/// </summary>
		void setValueIsAdjusting(bool @valueIsAdjusting);

	}
}
