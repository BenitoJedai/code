// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/WindowEvent.html
	[Script(IsNative = true)]
	public class WindowEvent : ComponentEvent
	{
		/// <summary>
		/// Constructs a <code>WindowEvent</code> object.
		/// </summary>
		public WindowEvent(Window @source, int @id) : base(source, id)
		{
		}

		/// <summary>
		/// Constructs a <code>WindowEvent</code> object with the specified
		/// previous and new window states.
		/// </summary>
		public WindowEvent(Window @source, int @id, int @oldState, int @newState)
			: base(source, id)
		{
		}

		/// <summary>
		/// Constructs a <code>WindowEvent</code> object with the
		/// specified opposite <code>Window</code>.
		/// </summary>
		public WindowEvent(Window @source, int @id, Window @opposite)
			: base(source, id)
		{
		}

		/// <summary>
		/// Constructs a <code>WindowEvent</code> object.
		/// </summary>
		public WindowEvent(Window @source, int @id, Window @opposite, int @oldState, int @newState)
			: base(source, id)
		{
		}

		/// <summary>
		/// For <code>WINDOW_STATE_CHANGED</code> events returns the
		/// new state of the window.
		/// </summary>
		public int getNewState()
		{
			return default(int);
		}

		/// <summary>
		/// For <code>WINDOW_STATE_CHANGED</code> events returns the
		/// previous state of the window.
		/// </summary>
		public int getOldState()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the other Window involved in this focus or activation change.
		/// </summary>
		public Window getOppositeWindow()
		{
			return default(Window);
		}

		/// <summary>
		/// Returns the originator of the event.
		/// </summary>
		public Window getWindow()
		{
			return default(Window);
		}

		/// <summary>
		/// Returns a parameter string identifying this event.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

	}
}

