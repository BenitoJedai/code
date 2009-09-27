// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JDesktopPane

using ScriptCoreLib;
using java.lang;
using javax.accessibility;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JDesktopPane.html
	[Script(IsNative = true)]
	public class JDesktopPane : JLayeredPane
	{
		/// <summary>
		/// Creates a new <code>JDesktopPane</code>.
		/// </summary>
		public JDesktopPane()
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JDesktopPane</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns all <code>JInternalFrames</code> currently displayed in the
		/// desktop.
		/// </summary>
		public JInternalFrame[] getAllFrames()
		{
			return default(JInternalFrame[]);
		}

		/// <summary>
		/// Returns all <code>JInternalFrames</code> currently displayed in the
		/// specified layer of the desktop.
		/// </summary>
		public JInternalFrame[] getAllFramesInLayer(int @layer)
		{
			return default(JInternalFrame[]);
		}

		/// <summary>
		/// Returns the <code>DesktopManger</code> that handles
		/// desktop-specific UI actions.
		/// </summary>
		public DesktopManager getDesktopManager()
		{
			return default(DesktopManager);
		}

		/// <summary>
		/// Gets the current "dragging style" used by the desktop pane.
		/// </summary>
		public int getDragMode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the currently active <code>JInternalFrame</code>
		/// in this <code>JDesktopPane</code>, or <code>null</code>
		/// if no <code>JInternalFrame</code> is currently active.
		/// </summary>
		public JInternalFrame getSelectedFrame()
		{
			return default(JInternalFrame);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public DesktopPaneUI getUI()
		{
			return default(DesktopPaneUI);
		}

		/// <summary>
		/// Returns the name of the L&F class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns true to indicate that this component paints every pixel
		/// in its range.
		/// </summary>
		public bool isOpaque()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JDesktopPane</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Sets the <code>DesktopManger</code> that will handle
		/// desktop-specific UI actions.
		/// </summary>
		public void setDesktopManager(DesktopManager @d)
		{
		}

		/// <summary>
		/// Sets the "dragging style" used by the desktop pane.
		/// </summary>
		public void setDragMode(int @dragMode)
		{
		}

		/// <summary>
		/// Sets the currently active <code>JInternalFrame</code>
		/// in this <code>JDesktopPane</code>.
		/// </summary>
		public void setSelectedFrame(JInternalFrame @f)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(DesktopPaneUI @ui)
		{
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the L&F has changed.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
