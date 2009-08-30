// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/AWTKeyStroke.html
	[Script(IsNative = true)]
	public class AWTKeyStroke
	{
		/// <summary>
		/// Constructs an <code>AWTKeyStroke</code> with default values.
		/// </summary>
		public AWTKeyStroke()
		{
		}

		/// <summary>
		/// Constructs an <code>AWTKeyStroke</code> with the specified
		/// values.
		/// </summary>
		public AWTKeyStroke(char @keyChar, int @keyCode, int @modifiers, bool @onKeyRelease)
		{
		}

		/// <summary>
		/// Returns true if this object is identical to the specified object.
		/// </summary>
		public override bool Equals(object @anObject)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a shared instance of an <code>AWTKeyStroke</code>
		/// that represents a <code>KEY_TYPED</code> event for the
		/// specified character.
		/// </summary>
		public AWTKeyStroke getAWTKeyStroke(char @keyChar)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Returns a shared instance of an <code>AWTKeyStroke</code>,
		/// given a Character object and a set of modifiers.
		/// </summary>
		public AWTKeyStroke getAWTKeyStroke(Character @keyChar, int @modifiers)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Returns a shared instance of an <code>AWTKeyStroke</code>,
		/// given a numeric key code and a set of modifiers.
		/// </summary>
		public AWTKeyStroke getAWTKeyStroke(int @keyCode, int @modifiers)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Returns a shared instance of an <code>AWTKeyStroke</code>,
		/// given a numeric key code and a set of modifiers, specifying
		/// whether the key is activated when it is pressed or released.
		/// </summary>
		public AWTKeyStroke getAWTKeyStroke(int @keyCode, int @modifiers, bool @onKeyRelease)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Parses a string and returns an <code>AWTKeyStroke</code>.
		/// </summary>
		public AWTKeyStroke getAWTKeyStroke(string @s)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Returns an <code>AWTKeyStroke</code> which represents the
		/// stroke which generated a given <code>KeyEvent</code>.
		/// </summary>
		public AWTKeyStroke getAWTKeyStrokeForEvent(KeyEvent @anEvent)
		{
			return default(AWTKeyStroke);
		}

		/// <summary>
		/// Returns the character for this <code>AWTKeyStroke</code>.
		/// </summary>
		public char getKeyChar()
		{
			return default(char);
		}

		/// <summary>
		/// Returns the numeric key code for this <code>AWTKeyStroke</code>.
		/// </summary>
		public int getKeyCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the type of <code>KeyEvent</code> which corresponds to
		/// this <code>AWTKeyStroke</code>.
		/// </summary>
		public int getKeyEventType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the modifier keys for this <code>AWTKeyStroke</code>.
		/// </summary>
		public int getModifiers()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a numeric value for this object that is likely to be unique,
		/// making it a good choice as the index value in a hash table.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns whether this <code>AWTKeyStroke</code> represents a key release.
		/// </summary>
		public bool isOnKeyRelease()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a cached instance of <code>AWTKeyStroke</code> (or a subclass of
		/// <code>AWTKeyStroke</code>) which is equal to this instance.
		/// </summary>
		public object readResolve()
		{
			return default(object);
		}

		/// <summary>
		/// Registers a new class which the factory methods in
		/// <code>AWTKeyStroke</code> will use when generating new
		/// instances of <code>AWTKeyStroke</code>s.
		/// </summary>
		static protected void registerSubclass(Class @subclass)
		{
		}

		/// <summary>
		/// Returns a string that displays and identifies this object's properties.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

