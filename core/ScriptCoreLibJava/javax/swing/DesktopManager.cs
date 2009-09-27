// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.DesktopManager

using ScriptCoreLib;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/DesktopManager.html
	[Script(IsNative = true)]
	public interface DesktopManager
	{
		/// <summary>
		/// Generally, indicate that this frame has focus.
		/// </summary>
		void activateFrame(JInternalFrame @f);

		/// <summary>
		/// This method is normally called when the user has indicated that
		/// they will begin dragging a component around.
		/// </summary>
		void beginDraggingFrame(JComponent @f);

		/// <summary>
		/// This methods is normally called when the user has indicated that
		/// they will begin resizing the frame.
		/// </summary>
		void beginResizingFrame(JComponent @f, int @direction);

		/// <summary>
		/// Generally, this call should remove the frame from it's parent.
		/// </summary>
		void closeFrame(JInternalFrame @f);

		/// <summary>
		/// Generally, indicate that this frame has lost focus.
		/// </summary>
		void deactivateFrame(JInternalFrame @f);

		/// <summary>
		/// Generally, remove any iconic representation that is present and restore the
		/// frame to it's original size and location.
		/// </summary>
		void deiconifyFrame(JInternalFrame @f);

		/// <summary>
		/// The user has moved the frame.
		/// </summary>
		void dragFrame(JComponent @f, int @newX, int @newY);

		/// <summary>
		/// This method signals the end of the dragging session.
		/// </summary>
		void endDraggingFrame(JComponent @f);

		/// <summary>
		/// This method signals the end of the resize session.
		/// </summary>
		void endResizingFrame(JComponent @f);

		/// <summary>
		/// Generally, remove this frame from it's parent and add an iconic representation.
		/// </summary>
		void iconifyFrame(JInternalFrame @f);

		/// <summary>
		/// Generally, the frame should be resized to match it's parents bounds.
		/// </summary>
		void maximizeFrame(JInternalFrame @f);

		/// <summary>
		/// Generally, this indicates that the frame should be restored to it's
		/// size and position prior to a maximizeFrame() call.
		/// </summary>
		void minimizeFrame(JInternalFrame @f);

		/// <summary>
		/// If possible, display this frame in an appropriate location.
		/// </summary>
		void openFrame(JInternalFrame @f);

		/// <summary>
		/// The user has resized the component.
		/// </summary>
		void resizeFrame(JComponent @f, int @newX, int @newY, int @newWidth, int @newHeight);

		/// <summary>
		/// This is a primitive reshape method.
		/// </summary>
		void setBoundsForFrame(JComponent @f, int @newX, int @newY, int @newWidth, int @newHeight);

	}
}
