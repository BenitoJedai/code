// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/GraphicsDevice.html
	[Script(IsNative = true)]
	public abstract class GraphicsDevice
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public GraphicsDevice()
		{
		}

		/// <summary>
		/// This method returns the number of bytes available in
		/// accelerated memory on this device.
		/// </summary>
		public int getAvailableAcceleratedMemory()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the "best" configuration possible that passes the
		/// criteria defined in the <A HREF="../../java/awt/GraphicsConfigTemplate.html" title="class in java.awt"><CODE>GraphicsConfigTemplate</CODE></A>.
		/// </summary>
		public GraphicsConfiguration getBestConfiguration(GraphicsConfigTemplate @gct)
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Returns all of the <code>GraphicsConfiguration</code>
		/// objects associated with this <code>GraphicsDevice</code>.
		/// </summary>
		public GraphicsConfiguration[] getConfigurations()
		{
			return default(GraphicsConfiguration[]);
		}

		/// <summary>
		/// Returns the default <code>GraphicsConfiguration</code>
		/// associated with this <code>GraphicsDevice</code>.
		/// </summary>
		public GraphicsConfiguration getDefaultConfiguration()
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Returns the current display mode of this
		/// <code>GraphicsDevice</code>.
		/// </summary>
		public DisplayMode getDisplayMode()
		{
			return default(DisplayMode);
		}

		/// <summary>
		/// Returns all display modes available for this
		/// <code>GraphicsDevice</code>.
		/// </summary>
		public DisplayMode[] getDisplayModes()
		{
			return default(DisplayMode[]);
		}

		/// <summary>
		/// Returns the <code>Window</code> object representing the
		/// full-screen window if the device is in full-screen mode.
		/// </summary>
		public Window getFullScreenWindow()
		{
			return default(Window);
		}

		/// <summary>
		/// Returns the identification string associated with this
		/// <code>GraphicsDevice</code>.
		/// </summary>
		public string getIDstring()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the type of this <code>GraphicsDevice</code>.
		/// </summary>
		abstract public int getType();

		/// <summary>
		/// Returns <code>true</code> if this <code>GraphicsDevice</code>
		/// supports low-level display changes.
		/// </summary>
		public bool isDisplayChangeSupported()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>GraphicsDevice</code>
		/// supports full-screen exclusive mode.
		/// </summary>
		public bool isFullScreenSupported()
		{
			return default(bool);
		}

		/// <summary>
		/// Sets the display mode of this graphics device.
		/// </summary>
		public void setDisplayMode(DisplayMode @dm)
		{
		}

		/// <summary>
		/// Enter full-screen mode, or return to windowed mode.
		/// </summary>
		public void setFullScreenWindow(Window @w)
		{
		}

	}
}

