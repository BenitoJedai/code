// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.im;
using java.lang;
using java.util;

namespace java.awt.im
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/im/InputContext.html
	[Script(IsNative = true)]
	public class InputContext
	{
		/// <summary>
		/// Constructs an InputContext.
		/// </summary>
		public InputContext()
		{
		}

		/// <summary>
		/// Dispatches an event to the active input method.
		/// </summary>
		public void dispatchEvent(AWTEvent @event)
		{
		}

		/// <summary>
		/// Disposes of the input context and release the resources used by it.
		/// </summary>
		public void dispose()
		{
		}

		/// <summary>
		/// Ends any input composition that may currently be going on in this
		/// context.
		/// </summary>
		public void endComposition()
		{
		}

		/// <summary>
		/// Returns a control object from the current input method, or null.
		/// </summary>
		public object getInputMethodControlObject()
		{
			return default(object);
		}

		/// <summary>
		/// Returns a new InputContext instance.
		/// </summary>
		public InputContext getInstance()
		{
			return default(InputContext);
		}

		/// <summary>
		/// Returns the current locale of the current input method or keyboard
		/// layout.
		/// </summary>
		public Locale getLocale()
		{
			return default(Locale);
		}

		/// <summary>
		/// Determines whether the current input method is enabled for composition.
		/// </summary>
		public bool isCompositionEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Asks the current input method to reconvert text from the
		/// current client component.
		/// </summary>
		public void reconvert()
		{
		}

		/// <summary>
		/// Notifies the input context that a client component has been
		/// removed from its containment hierarchy, or that input method
		/// support has been disabled for the component.
		/// </summary>
		public void removeNotify(Component @client)
		{
		}

		/// <summary>
		/// Attempts to select an input method or keyboard layout that
		/// supports the given locale, and returns a value indicating whether such
		/// an input method or keyboard layout has been successfully selected.
		/// </summary>
		public bool selectInputMethod(Locale @locale)
		{
			return default(bool);
		}


		/// <summary>
		/// Enables or disables the current input method for composition,
		/// depending on the value of the parameter <code>enable</code>.
		/// </summary>
		public void setCompositionEnabled(bool @enable)
		{
		}

	}
}

