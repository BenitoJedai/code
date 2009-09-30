// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JScrollPane

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing.border;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JScrollPane.html
	[Script(IsNative = true)]
	public class JScrollPane : JComponent
	{
		/// <summary>
		/// Creates an empty (no viewport view) <code>JScrollPane</code>
		/// where both horizontal and vertical scrollbars appear when needed.
		/// </summary>
		public JScrollPane()
		{
		}

		/// <summary>
		/// Creates a <code>JScrollPane</code> that displays the
		/// contents of the specified
		/// component, where both horizontal and vertical scrollbars appear
		/// whenever the component's contents are larger than the view.
		/// </summary>
		public JScrollPane(Component @view)
		{
		}

		/// <summary>
		/// Creates a <code>JScrollPane</code> that displays the view
		/// component in a viewport
		/// whose view position can be controlled with a pair of scrollbars.
		/// </summary>
		public JScrollPane(Component @view, int @vsbPolicy, int @hsbPolicy)
		{
		}

		/// <summary>
		/// Creates an empty (no viewport view) <code>JScrollPane</code>
		/// with specified
		/// scrollbar policies.
		/// </summary>
		public JScrollPane(int @vsbPolicy, int @hsbPolicy)
		{
		}

		/// <summary>
		/// Returns a <code>JScrollPane.ScrollBar</code> by default.
		/// </summary>
		public JScrollBar createHorizontalScrollBar()
		{
			return default(JScrollBar);
		}

		/// <summary>
		/// Returns a <code>JScrollPane.ScrollBar</code> by default.
		/// </summary>
		public JScrollBar createVerticalScrollBar()
		{
			return default(JScrollBar);
		}

		/// <summary>
		/// Returns a new <code>JViewport</code> by default.
		/// </summary>
		protected JViewport createViewport()
		{
			return default(JViewport);
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JScrollPane.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the column header.
		/// </summary>
		public JViewport getColumnHeader()
		{
			return default(JViewport);
		}

		/// <summary>
		/// Returns the component at the specified corner.
		/// </summary>
		public Component getCorner(string @key)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the horizontal scroll bar that controls the viewport's
		/// horizontal view position.
		/// </summary>
		public JScrollBar getHorizontalScrollBar()
		{
			return default(JScrollBar);
		}

		/// <summary>
		/// Returns the horizontal scroll bar policy value.
		/// </summary>
		public int getHorizontalScrollBarPolicy()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the row header.
		/// </summary>
		public JViewport getRowHeader()
		{
			return default(JViewport);
		}

		/// <summary>
		/// Returns the look and feel (L&F) object that renders this component.
		/// </summary>
		public ScrollPaneUI getUI()
		{
			return default(ScrollPaneUI);
		}

		/// <summary>
		/// Returns the suffix used to construct the name of the L&F class used to
		/// render this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the vertical scroll bar that controls the viewports
		/// vertical view position.
		/// </summary>
		public JScrollBar getVerticalScrollBar()
		{
			return default(JScrollBar);
		}

		/// <summary>
		/// Returns the vertical scroll bar policy value.
		/// </summary>
		public int getVerticalScrollBarPolicy()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current <code>JViewport</code>.
		/// </summary>
		public JViewport getViewport()
		{
			return default(JViewport);
		}

		/// <summary>
		/// Returns the <code>Border</code> object that surrounds the viewport.
		/// </summary>
		public Border getViewportBorder()
		{
			return default(Border);
		}

		/// <summary>
		/// Returns the bounds of the viewport's border.
		/// </summary>
		public Rectangle getViewportBorderBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Calls <code>revalidate</code> on any descendant of this
		/// <code>JScrollPane</code>.
		/// </summary>
		public bool isValidateRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not scrolling will take place in response to the
		/// mouse wheel.
		/// </summary>
		public bool isWheelScrollingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JScrollPane</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes the old columnHeader, if it exists.
		/// </summary>
		public void setColumnHeader(JViewport @columnHeader)
		{
		}

		/// <summary>
		/// Creates a column-header viewport if necessary, sets
		/// its view, and then adds the column-header viewport
		/// to the scrollpane.
		/// </summary>
		public void setColumnHeaderView(Component @view)
		{
		}

		/// <summary>
		/// Sets the orientation for the vertical and horizontal
		/// scrollbars as determined by the
		/// <code>ComponentOrientation</code> argument.
		/// </summary>
		public void setComponentOrientation(ComponentOrientation @co)
		{
		}

		/// <summary>
		/// Adds a child that will appear in one of the scroll panes
		/// corners, if there's room.
		/// </summary>
		public void setCorner(string @key, Component @corner)
		{
		}

		/// <summary>
		/// Adds the scrollbar that controls the viewport's horizontal view
		/// position to the scrollpane.
		/// </summary>
		public void setHorizontalScrollBar(JScrollBar @horizontalScrollBar)
		{
		}

		/// <summary>
		/// Determines when the horizontal scrollbar appears in the scrollpane.
		/// </summary>
		public void setHorizontalScrollBarPolicy(int @policy)
		{
		}

		/// <summary>
		/// Sets the layout manager for this <code>JScrollPane</code>.
		/// </summary>
		public void setLayout(LayoutManager @layout)
		{
		}

		/// <summary>
		/// Removes the old rowHeader, if it exists.
		/// </summary>
		public void setRowHeader(JViewport @rowHeader)
		{
		}

		/// <summary>
		/// Creates a row-header viewport if necessary, sets
		/// its view and then adds the row-header viewport
		/// to the scrollpane.
		/// </summary>
		public void setRowHeaderView(Component @view)
		{
		}

		/// <summary>
		/// Sets the <code>ScrollPaneUI</code> object that provides the
		/// look and feel (L&F) for this component.
		/// </summary>
		public void setUI(ScrollPaneUI @ui)
		{
		}

		/// <summary>
		/// Adds the scrollbar that controls the viewports vertical view position
		/// to the scrollpane.
		/// </summary>
		public void setVerticalScrollBar(JScrollBar @verticalScrollBar)
		{
		}

		/// <summary>
		/// Determines when the vertical scrollbar appears in the scrollpane.
		/// </summary>
		public void setVerticalScrollBarPolicy(int @policy)
		{
		}

		/// <summary>
		/// Removes the old viewport (if there is one); forces the
		/// viewPosition of the new viewport to be in the +x,+y quadrant;
		/// syncs up the row and column headers (if there are any) with the
		/// new viewport; and finally syncs the scrollbars and
		/// headers with the new viewport.
		/// </summary>
		public void setViewport(JViewport @viewport)
		{
		}

		/// <summary>
		/// Adds a border around the viewport.
		/// </summary>
		public void setViewportBorder(Border @viewportBorder)
		{
		}

		/// <summary>
		/// Creates a viewport if necessary and then sets its view.
		/// </summary>
		public void setViewportView(Component @view)
		{
		}

		/// <summary>
		/// Enables/disables scrolling in response to movement of the mouse wheel.
		/// </summary>
		public void setWheelScrollingEnabled(bool @handleWheel)
		{
		}

		/// <summary>
		/// Replaces the current <code>ScrollPaneUI</code> object with a version
		/// from the current default look and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
