// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.plaf.TextUI

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing.text;

namespace javax.swing.plaf
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/plaf/TextUI.html
	[Script(IsNative = true)]
	public abstract class TextUI : ComponentUI
	{
		/// <summary>
		/// 
		/// </summary>
		public TextUI()
		{
		}

		/// <summary>
		/// Causes the portion of the view responsible for the
		/// given part of the model to be repainted.
		/// </summary>
		abstract public void damageRange(JTextComponent @t, int @p0, int @p1);

		
		/// <summary>
		/// Fetches the binding of services that set a policy
		/// for the type of document being edited.
		/// </summary>
		abstract public EditorKit getEditorKit(JTextComponent @t);

		/// <summary>
		/// Fetches a View with the allocation of the associated
		/// text component (i.e.
		/// </summary>
		abstract public View getRootView(JTextComponent @t);

		/// <summary>
		/// Returns the string to be used as the tooltip at the passed in location.
		/// </summary>
		public string getToolTipText(JTextComponent @t, Point @pt)
		{
			return default(string);
		}

		/// <summary>
		/// Converts the given location in the model to a place in
		/// the view coordinate system.
		/// </summary>
		abstract public Rectangle modelToView(JTextComponent @t, int @pos);

	
		/// <summary>
		/// Converts the given place in the view coordinate system
		/// to the nearest representative location in the model.
		/// </summary>
		abstract public int viewToModel(JTextComponent @t, Point @pt);

	}
}
