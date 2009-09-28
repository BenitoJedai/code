// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.UIManager

using ScriptCoreLib;
using java.awt;
using java.beans;
using java.lang;
using java.util;
using javax.swing.border;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/UIManager.html
	[Script(IsNative = true)]
	public class UIManager
	{
		/// <summary>
		/// 
		/// </summary>
		public UIManager()
		{
		}

		/// <summary>
		/// Adds a <code>LookAndFeel</code> to the list of auxiliary look and feels.
		/// </summary>
		static public void addAuxiliaryLookAndFeel(LookAndFeel @laf)
		{
		}

		/// <summary>
		/// Adds a <code>PropertyChangeListener</code> to the listener list.
		/// </summary>
		static public void addPropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Returns an object from the defaults table.
		/// </summary>
		static public object get(object @key)
		{
			return default(object);
		}

		/// <summary>
		/// Returns an object from the defaults table that is appropriate for
		/// the given locale.
		/// </summary>
		static public object get(object @key, Locale @l)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the list of auxiliary look and feels (can be <code>null</code>).
		/// </summary>
		static public LookAndFeel[] getAuxiliaryLookAndFeels()
		{
			return default(LookAndFeel[]);
		}

		/// <summary>
		/// Returns a boolean from the defaults table which is associated with
		/// the key value.
		/// </summary>
		static public bool getBoolean(object @key)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a boolean from the defaults table which is associated with
		/// the key value and the given <code>Locale</code>.
		/// </summary>
		static public bool getBoolean(object @key, Locale @l)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a border from the defaults table.
		/// </summary>
		static public Border getBorder(object @key)
		{
			return default(Border);
		}

		/// <summary>
		/// Returns a border from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public Border getBorder(object @key, Locale @l)
		{
			return default(Border);
		}

		/// <summary>
		/// Returns a drawing color from the defaults table.
		/// </summary>
		static public Color getColor(object @key)
		{
			return default(Color);
		}

		/// <summary>
		/// Returns a drawing color from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public Color getColor(object @key, Locale @l)
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the name of the <code>LookAndFeel</code> class that implements
		/// the default cross platform look and feel -- the Java
		/// Look and Feel (JLF).
		/// </summary>
		static public string getCrossPlatformLookAndFeelClassName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the default values for this look and feel.
		/// </summary>
		static public UIDefaults getDefaults()
		{
			return default(UIDefaults);
		}

		/// <summary>
		/// Returns a dimension from the defaults table.
		/// </summary>
		static public Dimension getDimension(object @key)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns a dimension from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public Dimension getDimension(object @key, Locale @l)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns a drawing font from the defaults table.
		/// </summary>
		static public Font getFont(object @key)
		{
			return default(Font);
		}

		/// <summary>
		/// Returns a drawing font from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public Font getFont(object @key, Locale @l)
		{
			return default(Font);
		}

		/// <summary>
		/// Returns an <code>Icon</code> from the defaults table.
		/// </summary>
		static public Icon getIcon(object @key)
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns an <code>Icon</code> from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public Icon getIcon(object @key, Locale @l)
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns an <code>Insets</code> object from the defaults table.
		/// </summary>
		static public Insets getInsets(object @key)
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns an <code>Insets</code> object from the defaults table that is
		/// appropriate for the given locale.
		/// </summary>
		static public Insets getInsets(object @key, Locale @l)
		{
			return default(Insets);
		}

	

		/// <summary>
		/// Returns an integer from the defaults table.
		/// </summary>
		static public int getInt(object @key)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an integer from the defaults table that is appropriate
		/// for the given locale.
		/// </summary>
		static public int getInt(object @key, Locale @l)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current default look and feel or <code>null</code>.
		/// </summary>
		static public LookAndFeel getLookAndFeel()
		{
			return default(LookAndFeel);
		}

		/// <summary>
		/// Returns the default values for this look and feel.
		/// </summary>
		static public UIDefaults getLookAndFeelDefaults()
		{
			return default(UIDefaults);
		}

		/// <summary>
		/// Returns an array of all the <code>PropertyChangeListener</code>s added
		/// to this UIManager with addPropertyChangeListener().
		/// </summary>
		static public PropertyChangeListener[] getPropertyChangeListeners()
		{
			return default(PropertyChangeListener[]);
		}

		/// <summary>
		/// Returns a string from the defaults table.
		/// </summary>
		static public string getString(object @key)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a string from the defaults table that is appropriate for the
		/// given locale.
		/// </summary>
		static public string getString(object @key, Locale @l)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the name of the <code>LookAndFeel</code> class that implements
		/// the native systems look and feel if there is one, otherwise
		/// the name of the default cross platform <code>LookAndFeel</code>
		/// class.
		/// </summary>
		static public string getSystemLookAndFeelClassName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the L&F object that renders the target component.
		/// </summary>
		static public ComponentUI getUI(JComponent @target)
		{
			return default(ComponentUI);
		}

		/// <summary>
		/// Creates a new look and feel and adds it to the current array.
		/// </summary>
		static public void installLookAndFeel(string @name, string @className)
		{
		}

		/// <summary>
		/// Stores an object in the defaults table.
		/// </summary>
		static public object put(object @key, object @value)
		{
			return default(object);
		}

		/// <summary>
		/// Removes a <code>LookAndFeel</code> from the list of auxiliary look and feels.
		/// </summary>
		static public bool removeAuxiliaryLookAndFeel(LookAndFeel @laf)
		{
			return default(bool);
		}

		/// <summary>
		/// Removes a <code>PropertyChangeListener</code> from the listener list.
		/// </summary>
		static public void removePropertyChangeListener(PropertyChangeListener @listener)
		{
		}

	

		/// <summary>
		/// Sets the current default look and feel using a
		/// <code>LookAndFeel</code> object.
		/// </summary>
		static public void setLookAndFeel(LookAndFeel @newLookAndFeel)
		{
		}

		/// <summary>
		/// Sets the current default look and feel using a class name.
		/// </summary>
		static public void setLookAndFeel(string @className)
		{
		}

	}
}
