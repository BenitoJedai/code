// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/Condition.html
	[Script(IsNative = true)]
	public abstract class Condition
	{
		/// <summary>
		/// Creates a new, unnamed Condition with the default priority, which is 80.
		/// </summary>
		public Condition()
		{
		}

		/// <summary>
		/// Creates a new Condition with the specified name, and default priority,
		/// which is 80.
		/// </summary>
		public Condition(string @name)
		{
		}

		/// <summary>
		/// Creates a new Condition with the specified name and priority.
		/// </summary>
		public Condition(string @name, int @priority)
		{
		}

		/// <summary>
		/// Called by the system in order to clean up references to internal objects.
		/// </summary>
		public void cleanup()
		{
		}

		/// <summary>
		/// Returns the name of this condition.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the priority of this condition.
		/// </summary>
		public int getPriority()
		{
			return default(int);
		}

		/// <summary>
		/// Sets the name of this condition.
		/// </summary>
		public void setName(string @newName)
		{
		}

		/// <summary>
		/// Sets the priority of this condition.
		/// </summary>
		public void setPriority(int @newPriority)
		{
		}

		/// <summary>
		/// Overriding the test() method is the point of a Condition.
		/// </summary>
		public abstract bool test();

	}
}
