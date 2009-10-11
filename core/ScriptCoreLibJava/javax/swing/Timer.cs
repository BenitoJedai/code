// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.Timer

using ScriptCoreLib;
using java.awt.@event;
using java.lang;
using java.util;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/Timer.html
	[Script(IsNative = true)]
	public class Timer
	{
		/// <summary>
		/// Creates a <code>Timer</code> that will notify its listeners every
		/// <code>delay</code> milliseconds.
		/// </summary>
		public Timer(int @delay, ActionListener @listener)
		{
		}

		/// <summary>
		/// Adds an action listener to the <code>Timer</code>.
		/// </summary>
		public void addActionListener(ActionListener @listener)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireActionPerformed(ActionEvent @e)
		{
		}

		/// <summary>
		/// Returns an array of all the action listeners registered
		/// on this timer.
		/// </summary>
		public ActionListener[] getActionListeners()
		{
			return default(ActionListener[]);
		}

		/// <summary>
		/// Returns the delay, in milliseconds,
		/// between firings of action events.
		/// </summary>
		public int getDelay()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>Timer</code>'s initial delay.
		/// </summary>
		public int getInitialDelay()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the objects currently registered as
		/// <code><em>Foo</em>Listener</code>s
		/// upon this <code>Timer</code>.
		/// </summary>
		public EventListener[] getListeners(Class @listenerType)
		{
			return default(EventListener[]);
		}

		/// <summary>
		/// Returns <code>true</code> if logging is enabled.
		/// </summary>
		static public bool getLogTimers()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if the <code>Timer</code> coalesces
		/// multiple pending action events.
		/// </summary>
		public bool isCoalesce()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> (the default)
		/// if the <code>Timer</code> will send
		/// an action event
		/// to its listeners multiple times.
		/// </summary>
		public bool isRepeats()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if the <code>Timer</code> is running.
		/// </summary>
		public bool isRunning()
		{
			return default(bool);
		}

		/// <summary>
		/// Removes the specified action listener from the <code>Timer</code>.
		/// </summary>
		public void removeActionListener(ActionListener @listener)
		{
		}

		/// <summary>
		/// Restarts the <code>Timer</code>,
		/// canceling any pending firings and causing
		/// it to fire with its initial delay.
		/// </summary>
		public void restart()
		{
		}

		/// <summary>
		/// Sets whether the <code>Timer</code> coalesces multiple pending
		/// <code>ActionEvent</code> firings.
		/// </summary>
		public void setCoalesce(bool @flag)
		{
		}

		/// <summary>
		/// Sets the <code>Timer</code>'s delay, the number of milliseconds
		/// between successive action events.
		/// </summary>
		public void setDelay(int @delay)
		{
		}

		/// <summary>
		/// Sets the <code>Timer</code>'s initial delay,
		/// which by default is the same as the between-event delay.
		/// </summary>
		public void setInitialDelay(int @initialDelay)
		{
		}

		/// <summary>
		/// Enables or disables the timer log.
		/// </summary>
		static public void setLogTimers(bool @flag)
		{
		}

		/// <summary>
		/// If <code>flag</code> is <code>false</code>,
		/// instructs the <code>Timer</code> to send only one
		/// action event to its listeners.
		/// </summary>
		public void setRepeats(bool @flag)
		{
		}

		/// <summary>
		/// Starts the <code>Timer</code>,
		/// causing it to start sending action events
		/// to its listeners.
		/// </summary>
		public void start()
		{
		}

		/// <summary>
		/// Stops the <code>Timer</code>,
		/// causing it to stop sending action events
		/// to its listeners.
		/// </summary>
		public void stop()
		{
		}

	}
}
