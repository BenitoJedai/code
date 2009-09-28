// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.LookAndFeel

using ScriptCoreLib;
using java.awt;
using java.lang;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/LookAndFeel.html
	[Script(IsNative = true)]
	public abstract class LookAndFeel
	{
		/// <summary>
		/// 
		/// </summary>
		public LookAndFeel()
		{
		}

		/// <summary>
		/// This method is called once by UIManager.setLookAndFeel to create
		/// the look and feel specific defaults table.
		/// </summary>
		public UIDefaults getDefaults()
		{
			return default(UIDefaults);
		}

		/// <summary>
		/// Return a one line description of this look and feel implementation,
		/// e.g.
		/// </summary>
		abstract public string getDescription();

		/// <summary>
		/// Returns the value of the specified system desktop property by
		/// invoking <code>Toolkit.getDefaultToolkit().getDesktopProperty()</code>.
		/// </summary>
		static public object getDesktopPropertyValue(string @systemPropertyName, object @fallbackValue)
		{
			return default(object);
		}

		/// <summary>
		/// Return a string that identifies this look and feel.
		/// </summary>
		abstract public string getID();

		/// <summary>
		/// Return a short string that identifies this look and feel, e.g.
		/// </summary>
		abstract public string getName();

		/// <summary>
		/// Returns true if the <code>LookAndFeel</code> returned
		/// <code>RootPaneUI</code> instances support providing Window decorations
		/// in a <code>JRootPane</code>.
		/// </summary>
		public bool getSupportsWindowDecorations()
		{
			return default(bool);
		}

		/// <summary>
		/// UIManager.setLookAndFeel calls this method before the first
		/// call (and typically the only call) to getDefaults().
		/// </summary>
		public void initialize()
		{
		}

		/// <summary>
		/// Convenience method for installing a component's default Border
		/// object on the specified component if either the border is
		/// currently null or already an instance of UIResource.
		/// </summary>
		static public void installBorder(JComponent @c, string @defaultBorderName)
		{
		}

		/// <summary>
		/// Convenience method for initializing a component's foreground
		/// and background color properties with values from the current
		/// defaults table.
		/// </summary>
		static public void installColors(JComponent @c, string @defaultBgName, string @defaultFgName)
		{
		}

		/// <summary>
		/// Convenience method for initializing a components foreground
		/// background and font properties with values from the current
		/// defaults table.
		/// </summary>
		static public void installColorsAndFont(JComponent @c, string @defaultBgName, string @defaultFgName, string @defaultFontName)
		{
		}

		/// <summary>
		/// If the underlying platform has a "native" look and feel, and this
		/// is an implementation of it, return true.
		/// </summary>
		abstract public bool isNativeLookAndFeel();

		/// <summary>
		/// Return true if the underlying platform supports and or permits
		/// this look and feel.
		/// </summary>
		abstract public bool isSupportedLookAndFeel();

		/// <summary>
		/// Loads the bindings in <code>keys</code> into <code>retMap</code>.
		/// </summary>
		static public void loadKeyBindings(InputMap @retMap, object[] @keys)
		{
		}

		/// <summary>
		/// Creates a ComponentInputMap from <code>keys</code>.
		/// </summary>
		static public ComponentInputMap makeComponentInputMap(JComponent @c, object[] @keys)
		{
			return default(ComponentInputMap);
		}

		/// <summary>
		/// Utility method that creates a UIDefaults.LazyValue that creates
		/// an ImageIcon UIResource for the specified <code>gifFile</code>
		/// filename.
		/// </summary>
		static public object makeIcon(Class @baseClass, string @gifFile)
		{
			return default(object);
		}

		/// <summary>
		/// Creates a InputMap from <code>keys</code>.
		/// </summary>
		static public InputMap makeInputMap(object[] @keys)
		{
			return default(InputMap);
		}

	

		/// <summary>
		/// Invoked when the user attempts an invalid operation,
		/// such as pasting into an uneditable <code>JTextField</code>
		/// that has focus.
		/// </summary>
		public void provideErrorFeedback(Component @component)
		{
		}

		/// <summary>
		/// Returns a string that displays and identifies this
		/// object's properties.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// UIManager.setLookAndFeel calls this method just before we're
		/// replaced by a new default look and feel.
		/// </summary>
		public void uninitialize()
		{
		}

		/// <summary>
		/// Convenience method for un-installing a component's default
		/// border on the specified component if the border is
		/// currently an instance of UIResource.
		/// </summary>
		static public void uninstallBorder(JComponent @c)
		{
		}

	}
}
