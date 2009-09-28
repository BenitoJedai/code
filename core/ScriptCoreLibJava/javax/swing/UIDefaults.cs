// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.UIDefaults

using ScriptCoreLib;
using java.awt;
using java.beans;
using java.lang;
using java.util;
using javax.swing.border;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/UIDefaults.html
	[Script(IsNative = true)]
	public class UIDefaults : Hashtable
	{
		/// <summary>
		/// Create an empty defaults table.
		/// </summary>
		public UIDefaults()
		{
		}

		/// <summary>
		/// Create a defaults table initialized with the specified
		/// key/value pairs.
		/// </summary>
		public UIDefaults(object[] @keyValueList)
		{
		}

		/// <summary>
		/// Adds a <code>PropertyChangeListener</code> to the listener list.
		/// </summary>
		public void addPropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Adds a resource bundle to the list of resource bundles that are
		/// searched for localized values.
		/// </summary>
		public void addResourceBundle(string @bundleName)
		{
		}

		/// <summary>
		/// Support for reporting bound property changes.
		/// </summary>
		protected void firePropertyChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Returns the value for key.
		/// </summary>
		public object get(object @key)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the value for key associated with the given locale.
		/// </summary>
		public object get(object @key, Locale @l)
		{
			return default(object);
		}

		/// <summary>
		/// If the value of <code>key</code> is boolean, return the
		/// boolean value, otherwise return false.
		/// </summary>
		public bool getBoolean(object @key)
		{
			return default(bool);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is boolean, return the boolean value, otherwise return false.
		/// </summary>
		public bool getBoolean(object @key, Locale @l)
		{
			return default(bool);
		}

		/// <summary>
		/// If the value of <code>key</code> is a <code>Border</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Border getBorder(object @key)
		{
			return default(Border);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is a <code>Border</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Border getBorder(object @key, Locale @l)
		{
			return default(Border);
		}

		/// <summary>
		/// If the value of <code>key</code> is a <code>Color</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Color getColor(object @key)
		{
			return default(Color);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is a <code>Color</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Color getColor(object @key, Locale @l)
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the default locale.
		/// </summary>
		public Locale getDefaultLocale()
		{
			return default(Locale);
		}

		/// <summary>
		/// If the value of <code>key</code> is a <code>Dimension</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Dimension getDimension(object @key)
		{
			return default(Dimension);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is a <code>Dimension</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Dimension getDimension(object @key, Locale @l)
		{
			return default(Dimension);
		}

		/// <summary>
		/// If the value of <code>key</code> is a <code>Font</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Font getFont(object @key)
		{
			return default(Font);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is a <code>Font</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Font getFont(object @key, Locale @l)
		{
			return default(Font);
		}

		/// <summary>
		/// If the value of <code>key</code> is an <code>Icon</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Icon getIcon(object @key)
		{
			return default(Icon);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is an <code>Icon</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Icon getIcon(object @key, Locale @l)
		{
			return default(Icon);
		}

		/// <summary>
		/// If the value of <code>key</code> is an <code>Insets</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public Insets getInsets(object @key)
		{
			return default(Insets);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is an <code>Insets</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public Insets getInsets(object @key, Locale @l)
		{
			return default(Insets);
		}

		/// <summary>
		/// If the value of <code>key</code> is an <code>Integer</code> return its
		/// integer value, otherwise return 0.
		/// </summary>
		public int getInt(object @key)
		{
			return default(int);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is an <code>Integer</code> return its integer value, otherwise return 0.
		/// </summary>
		public int getInt(object @key, Locale @l)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the <code>PropertyChangeListener</code>s added
		/// to this UIDefaults with addPropertyChangeListener().
		/// </summary>
		public PropertyChangeListener[] getPropertyChangeListeners()
		{
			return default(PropertyChangeListener[]);
		}

		/// <summary>
		/// If the value of <code>key</code> is a <code>String</code> return it,
		/// otherwise return <code>null</code>.
		/// </summary>
		public string getString(object @key)
		{
			return default(string);
		}

		/// <summary>
		/// If the value of <code>key</code> for the given <code>Locale</code>
		/// is a <code>String</code> return it, otherwise return <code>null</code>.
		/// </summary>
		public string getString(object @key, Locale @l)
		{
			return default(string);
		}

		/// <summary>
		/// Creates an <code>ComponentUI</code> implementation for the
		/// specified component.
		/// </summary>
		public ComponentUI getUI(JComponent @target)
		{
			return default(ComponentUI);
		}

		/// <summary>
		/// Returns the L&F class that renders this component.
		/// </summary>
		public Class getUIClass(string @uiClassID)
		{
			return default(Class);
		}

		/// <summary>
		/// The value of <code>get(uidClassID)</code> must be the
		/// <code>String</code> name of a
		/// class that implements the corresponding <code>ComponentUI</code>
		/// class.
		/// </summary>
		public Class getUIClass(string @uiClassID, ClassLoader @uiClassLoader)
		{
			return default(Class);
		}

		/// <summary>
		/// If <code>getUI()</code> fails for any reason,
		/// it calls this method before returning <code>null</code>.
		/// </summary>
		protected void getUIError(string @msg)
		{
		}

		/// <summary>
		/// Sets the value of <code>key</code> to <code>value</code> for all locales.
		/// </summary>
		public object put(object @key, object @value)
		{
			return default(object);
		}

		/// <summary>
		/// Puts all of the key/value pairs in the database and
		/// unconditionally generates one <code>PropertyChangeEvent</code>.
		/// </summary>
		public void putDefaults(object[] @keyValueList)
		{
		}

		/// <summary>
		/// Removes a <code>PropertyChangeListener</code> from the listener list.
		/// </summary>
		public void removePropertyChangeListener(PropertyChangeListener @listener)
		{
		}

		/// <summary>
		/// Removes a resource bundle from the list of resource bundles that are
		/// searched for localized defaults.
		/// </summary>
		public void removeResourceBundle(string @bundleName)
		{
		}

		/// <summary>
		/// Sets the default locale.
		/// </summary>
		public void setDefaultLocale(Locale @l)
		{
		}

	}
}
