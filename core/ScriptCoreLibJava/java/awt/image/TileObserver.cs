// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.image;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/TileObserver.html
	[Script(IsNative = true)]
	public interface TileObserver
	{
		/// <summary>
		/// A tile is about to be updated (it is either about to be grabbed
		/// for writing, or it is being released from writing).
		/// </summary>
		void tileUpdate(WritableRenderedImage @source, int @tileX, int @tileY, bool @willBeWritable);

	}
}

