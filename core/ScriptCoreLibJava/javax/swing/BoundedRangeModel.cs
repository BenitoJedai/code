// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.BoundedRangeModel

using ScriptCoreLib;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/BoundedRangeModel.html
	[Script(IsNative = true)]
	public interface BoundedRangeModel
	{
		/// <summary>
		/// Adds a ChangeListener to the model's listener list.
		/// </summary>
		void addChangeListener(ChangeListener @x);

		/// <summary>
		/// Returns the model's extent, the length of the inner range that
		/// begins at the model's value.
		/// </summary>
		int getExtent();

		/// <summary>
		/// Returns the model's maximum.
		/// </summary>
		int getMaximum();

		/// <summary>
		/// Returns the minimum acceptable value.
		/// </summary>
		int getMinimum();

		/// <summary>
		/// Returns the model's current value.
		/// </summary>
		int getValue();

		/// <summary>
		/// Returns true if the current changes to the value property are part
		/// of a series of changes.
		/// </summary>
		bool getValueIsAdjusting();

		/// <summary>
		/// Removes a ChangeListener from the model's listener list.
		/// </summary>
		void removeChangeListener(ChangeListener @x);

		/// <summary>
		/// Sets the model's extent.
		/// </summary>
		void setExtent(int @newExtent);

		/// <summary>
		/// Sets the model's maximum to <I>newMaximum</I>.
		/// </summary>
		void setMaximum(int @newMaximum);

		/// <summary>
		/// Sets the model's minimum to <I>newMinimum</I>.
		/// </summary>
		void setMinimum(int @newMinimum);

		/// <summary>
		/// This method sets all of the model's data with a single method call.
		/// </summary>
		void setRangeProperties(int @value, int @extent, int @min, int @max, bool @adjusting);

		/// <summary>
		/// Sets the model's current value to <code>newValue</code> if <code>newValue</code>
		/// satisfies the model's constraints.
		/// </summary>
		void setValue(int @newValue);

		/// <summary>
		/// This attribute indicates that any upcoming changes to the value
		/// of the model should be considered a single event.
		/// </summary>
		void setValueIsAdjusting(bool @b);

	}
}
