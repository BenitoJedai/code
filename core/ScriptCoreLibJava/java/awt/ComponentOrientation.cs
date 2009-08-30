// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using java.util;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/ComponentOrientation.html
	[Script(IsNative = true)]
	public class ComponentOrientation : Object
	{
		/// <summary>
		/// Returns the orientation that is appropriate for the given locale.
		/// </summary>
		public ComponentOrientation getOrientation(Locale @locale)
		{
			return default(ComponentOrientation);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of J2SE 1.4, use <A HREF="../../java/awt/ComponentOrientation.html#getOrientation(java.util.Locale)"><CODE>getOrientation(java.util.Locale)</CODE></A>.</I>
		/// </summary>
		public ComponentOrientation getOrientation(ResourceBundle @bdl)
		{
			return default(ComponentOrientation);
		}

		/// <summary>
		/// Are lines horizontal?
		/// This will return true for horizontal, left-to-right writing
		/// systems such as Roman.
		/// </summary>
		public bool isHorizontal()
		{
			return default(bool);
		}

		/// <summary>
		/// HorizontalLines: Do items run left-to-right?<br>
		/// Vertical Lines:  Do lines run left-to-right?<br>
		/// This will return true for horizontal, left-to-right writing
		/// systems such as Roman.
		/// </summary>
		public bool isLeftToRight()
		{
			return default(bool);
		}

	}
}

