// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JScrollBar

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JScrollBar.html
	[Script(IsNative = true)]
	public class JScrollBar : JComponent
	{
		/// <summary>
		/// Creates a vertical scrollbar with the following initial values:
		/// </summary>
		public JScrollBar()
		{
		}

		/// <summary>
		/// Creates a scrollbar with the specified orientation
		/// and the following initial values:
		/// </summary>
		public JScrollBar(int @orientation)
		{
		}

		/// <summary>
		/// Creates a scrollbar with the specified orientation,
		/// value, extent, minimum, and maximum.
		/// </summary>
		public JScrollBar(int @orientation, int @value, int @extent, int @min, int @max)
		{
		}

		/// <summary>
		/// Adds an AdjustmentListener.
		/// </summary>
		public void addAdjustmentListener(AdjustmentListener @l)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected void fireAdjustmentValueChanged(int @id, int @type, int @value)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JScrollBar.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns an array of all the <code>AdjustmentListener</code>s added
		/// to this JScrollBar with addAdjustmentListener().
		/// </summary>
		public AdjustmentListener[] getAdjustmentListeners()
		{
			return default(AdjustmentListener[]);
		}

		/// <summary>
		/// For backwards compatibility with java.awt.Scrollbar.
		/// </summary>
		public int getBlockIncrement()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the amount to change the scrollbar's value by,
		/// given a block (usually "page") up/down request.
		/// </summary>
		public int getBlockIncrement(int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// The maximum value of the scrollbar is maximum - extent.
		/// </summary>
		public int getMaximum()
		{
			return default(int);
		}

		/// <summary>
		/// The scrollbar is flexible along it's scrolling axis and
		/// rigid along the other axis.
		/// </summary>
		public Dimension getMaximumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the minimum value supported by the scrollbar
		/// (usually zero).
		/// </summary>
		public int getMinimum()
		{
			return default(int);
		}

		/// <summary>
		/// The scrollbar is flexible along it's scrolling axis and
		/// rigid along the other axis.
		/// </summary>
		public Dimension getMinimumSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns data model that handles the scrollbar's four
		/// fundamental properties: minimum, maximum, value, extent.
		/// </summary>
		public BoundedRangeModel getModel()
		{
			return default(BoundedRangeModel);
		}

		/// <summary>
		/// Returns the component's orientation (horizontal or vertical).
		/// </summary>
		public int getOrientation()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the delegate that implements the look and feel for
		/// this component.
		/// </summary>
		public ScrollBarUI getUI()
		{
			return default(ScrollBarUI);
		}

		/// <summary>
		/// Returns the name of the LookAndFeel class for this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// For backwards compatibility with java.awt.Scrollbar.
		/// </summary>
		public int getUnitIncrement()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the amount to change the scrollbar's value by,
		/// given a unit up/down request.
		/// </summary>
		public int getUnitIncrement(int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the scrollbar's value.
		/// </summary>
		public int getValue()
		{
			return default(int);
		}

		/// <summary>
		/// True if the scrollbar knob is being dragged.
		/// </summary>
		public bool getValueIsAdjusting()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the scrollbar's extent, aka its "visibleAmount".
		/// </summary>
		public int getVisibleAmount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a string representation of this JScrollBar.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes an AdjustmentEvent listener.
		/// </summary>
		public void removeAdjustmentListener(AdjustmentListener @l)
		{
		}

		/// <summary>
		/// Sets the blockIncrement property.
		/// </summary>
		public void setBlockIncrement(int @blockIncrement)
		{
		}

		/// <summary>
		/// Enables the component so that the knob position can be changed.
		/// </summary>
		public void setEnabled(bool @x)
		{
		}

		/// <summary>
		/// Sets the model's maximum property.
		/// </summary>
		public void setMaximum(int @maximum)
		{
		}

		/// <summary>
		/// Sets the model's minimum property.
		/// </summary>
		public void setMinimum(int @minimum)
		{
		}

		/// <summary>
		/// Sets the model that handles the scrollbar's four
		/// fundamental properties: minimum, maximum, value, extent.
		/// </summary>
		public void setModel(BoundedRangeModel @newModel)
		{
		}

		/// <summary>
		/// Set the scrollbar's orientation to either VERTICAL or
		/// HORIZONTAL.
		/// </summary>
		public void setOrientation(int @orientation)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(ScrollBarUI @ui)
		{
		}

		/// <summary>
		/// Sets the unitIncrement property.
		/// </summary>
		public void setUnitIncrement(int @unitIncrement)
		{
		}

		/// <summary>
		/// Sets the scrollbar's value.
		/// </summary>
		public void setValue(int @value)
		{
		}

		/// <summary>
		/// Sets the model's valueIsAdjusting property.
		/// </summary>
		public void setValueIsAdjusting(bool @b)
		{
		}

		/// <summary>
		/// Sets the four BoundedRangeModel properties after forcing
		/// the arguments to obey the usual constraints:
		/// </summary>
		public void setValues(int @newValue, int @newExtent, int @newMin, int @newMax)
		{
		}

		/// <summary>
		/// Set the model's extent property.
		/// </summary>
		public void setVisibleAmount(int @extent)
		{
		}

		/// <summary>
		/// Overrides <code>JComponent.updateUI</code>.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
