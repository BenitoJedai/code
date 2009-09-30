// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.awt.Adjustable

using ScriptCoreLib;
using java.awt.@event;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Adjustable.html
	[Script(IsNative = true)]
	public interface Adjustable
	{
		/// <summary>
		/// Adds a listener to receive adjustment events when the value of
		/// the adjustable object changes.
		/// </summary>
		void addAdjustmentListener(AdjustmentListener @l);

		/// <summary>
		/// Gets the block value increment for the adjustable object.
		/// </summary>
		int getBlockIncrement();

		/// <summary>
		/// Gets the maximum value of the adjustable object.
		/// </summary>
		int getMaximum();

		/// <summary>
		/// Gets the minimum value of the adjustable object.
		/// </summary>
		int getMinimum();

		/// <summary>
		/// Gets the orientation of the adjustable object.
		/// </summary>
		int getOrientation();

		/// <summary>
		/// Gets the unit value increment for the adjustable object.
		/// </summary>
		int getUnitIncrement();

		/// <summary>
		/// Gets the current value of the adjustable object.
		/// </summary>
		int getValue();

		/// <summary>
		/// Gets the length of the proportional indicator.
		/// </summary>
		int getVisibleAmount();

		/// <summary>
		/// Removes an adjustment listener.
		/// </summary>
		void removeAdjustmentListener(AdjustmentListener @l);

		/// <summary>
		/// Sets the block value increment for the adjustable object.
		/// </summary>
		void setBlockIncrement(int @b);

		/// <summary>
		/// Sets the maximum value of the adjustable object.
		/// </summary>
		void setMaximum(int @max);

		/// <summary>
		/// Sets the minimum value of the adjustable object.
		/// </summary>
		void setMinimum(int @min);

		/// <summary>
		/// Sets the unit value increment for the adjustable object.
		/// </summary>
		void setUnitIncrement(int @u);

		/// <summary>
		/// Sets the current value of the adjustable object.
		/// </summary>
		void setValue(int @v);

		/// <summary>
		/// Sets the length of the proportional indicator of the
		/// adjustable object.
		/// </summary>
		void setVisibleAmount(int @v);

	}
}
