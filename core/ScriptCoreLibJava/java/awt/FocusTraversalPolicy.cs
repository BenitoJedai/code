// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/FocusTraversalPolicy.html
	[Script(IsNative = true)]
	public class FocusTraversalPolicy
	{
		/// <summary>
		/// 
		/// </summary>
		public FocusTraversalPolicy()
		{
		}

		/// <summary>
		/// Returns the Component that should receive the focus after aComponent.
		/// </summary>
		public Component getComponentAfter(Container @focusCycleRoot, Component @aComponent)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the Component that should receive the focus before aComponent.
		/// </summary>
		public Component getComponentBefore(Container @focusCycleRoot, Component @aComponent)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the default Component to focus.
		/// </summary>
		public Component getDefaultComponent(Container @focusCycleRoot)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the first Component in the traversal cycle.
		/// </summary>
		public Component getFirstComponent(Container @focusCycleRoot)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the Component that should receive the focus when a Window is
		/// made visible for the first time.
		/// </summary>
		public Component getInitialComponent(Window @window)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the last Component in the traversal cycle.
		/// </summary>
		public Component getLastComponent(Container @focusCycleRoot)
		{
			return default(Component);
		}

	}
}

