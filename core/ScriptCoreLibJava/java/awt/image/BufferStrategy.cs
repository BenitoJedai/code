// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/BufferStrategy.html
	[Script(IsNative = true)]
	public abstract class BufferStrategy
	{
		/// <summary>
		/// 
		/// </summary>
		public BufferStrategy()
		{
		}

		/// <summary>
		/// Returns whether the drawing buffer was lost since the last call to
		/// <code>getDrawGraphics</code>.
		/// </summary>
		abstract public bool contentsLost();

		/// <summary>
		/// Returns whether the drawing buffer was recently restored from a lost
		/// state and reinitialized to the default background color (white).
		/// </summary>
		abstract public bool contentsRestored();

		/// <summary>
		/// 
		/// </summary>
		public BufferCapabilities getCapabilities()
		{
			return default(BufferCapabilities);
		}

		/// <summary>
		/// 
		/// </summary>
		public Graphics getDrawGraphics()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Makes the next available buffer visible by either copying the memory
		/// (blitting) or changing the display pointer (flipping).
		/// </summary>
		abstract public void show();

	}
}

