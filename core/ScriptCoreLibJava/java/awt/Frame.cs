// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Frame.html
	[Script(IsNative = true)]
	public class Frame : Window
	{
		/// <summary>
		/// Constructs a new instance of <code>Frame</code> that is
		/// initially invisible.
		/// </summary>
		public Frame() : base((Frame)(null))
		{
		}

		/// <summary>
		/// Create a <code>Frame</code> with the specified
		/// <code>GraphicsConfiguration</code> of
		/// a screen device.
		/// </summary>
		public Frame(GraphicsConfiguration @gc)
			: base((Frame)(null))
		{
		}

		/// <summary>
		/// Constructs a new, initially invisible <code>Frame</code> object
		/// with the specified title.
		/// </summary>
		public Frame(string @title)
			: base((Frame)(null))
		{
		}

		/// <summary>
		/// Constructs a new, initially invisible <code>Frame</code> object
		/// with the specified title and a
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public Frame(string @title, GraphicsConfiguration @gc)
			: base((Frame)(null))
		{
		}

		/// <summary>
		/// Makes this Frame displayable by connecting it to
		/// a native screen resource.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// We have to remove the (hard) reference to weakThis in the
		/// Vector, otherwise the WeakReference instance will never get
		/// garbage collected.
		/// </summary>
		protected void finalize()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this Frame.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>Component.getCursor()</code>.</I>
		/// </summary>
		public int getCursorType()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the state of this frame.
		/// </summary>
		public int getExtendedState()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array containing all Frames created by the application.
		/// </summary>
		public Frame[] getFrames()
		{
			return default(Frame[]);
		}

		/// <summary>
		/// Gets the image to be displayed in the minimized icon
		/// for this frame.
		/// </summary>
		public Image getIconImage()
		{
			return default(Image);
		}

		/// <summary>
		/// Gets maximized bounds for this frame.
		/// </summary>
		public Rectangle getMaximizedBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Gets the menu bar for this frame.
		/// </summary>
		public MenuBar getMenuBar()
		{
			return default(MenuBar);
		}

		/// <summary>
		/// Gets the state of this frame (obsolete).
		/// </summary>
		public int getState()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the title of the frame.
		/// </summary>
		public string getTitle()
		{
			return default(string);
		}

		/// <summary>
		/// Indicates whether this frame is resizable by the user.
		/// </summary>
		public bool isResizable()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether this frame is undecorated.
		/// </summary>
		public bool isUndecorated()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representing the state of this <code>Frame</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes the specified menu bar from this frame.
		/// </summary>
		public void remove(MenuComponent @m)
		{
		}

		/// <summary>
		/// Makes this Frame undisplayable by removing its connection
		/// to its native screen resource.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>Component.setCursor(Cursor)</code>.</I>
		/// </summary>
		public void setCursor(int @cursorType)
		{
		}

		/// <summary>
		/// Sets the state of this frame.
		/// </summary>
		public void setExtendedState(int @state)
		{
		}

		/// <summary>
		/// Sets the image to be displayed in the minimized icon for this frame.
		/// </summary>
		public void setIconImage(Image @image)
		{
		}

		/// <summary>
		/// Sets the maximized bounds for this frame.
		/// </summary>
		public void setMaximizedBounds(Rectangle @bounds)
		{
		}

		/// <summary>
		/// Sets the menu bar for this frame to the specified menu bar.
		/// </summary>
		public void setMenuBar(MenuBar @mb)
		{
		}

		/// <summary>
		/// Sets whether this frame is resizable by the user.
		/// </summary>
		public void setResizable(bool @resizable)
		{
		}

		/// <summary>
		/// Sets the state of this frame (obsolete).
		/// </summary>
		public void setState(int @state)
		{
		}

		/// <summary>
		/// Sets the title for this frame to the specified string.
		/// </summary>
		public void setTitle(string @title)
		{
		}

		/// <summary>
		/// Disables or enables decorations for this frame.
		/// </summary>
		public void setUndecorated(bool @undecorated)
		{
		}

	}
}

