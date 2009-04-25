using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/MemoryImageSource.html
	[Script(IsNative = true)]
	public class MemoryImageSource : ImageProducer
	{
		/// <summary>
		/// Constructs an ImageProducer object which uses an array of integers in the default RGB ColorModel to produce data for an Image object.
		/// </summary>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="pix"></param>
		/// <param name="off"></param>
		/// <param name="scan"></param>
		public MemoryImageSource(int w, int h, int[] pix, int off, int scan) 
		{

		}

		public MemoryImageSource(int w, int h, uint[] pix, int off, int scan)
		{

		}

		/// <summary>
		/// Sends a whole new buffer of pixels to any ImageConsumers that are currently interested in the data for this image and notify them that an animation frame is complete.
		/// </summary>
		public void newPixels()
		{
		}
          
		/// <summary>
		/// Changes this memory image into a multi-frame animation or a single-frame static image depending on the animated parameter.
		/// </summary>
		/// <param name="animated"></param>
		public void setAnimated(bool animated)
		{
		}

		/// <summary>
		/// Specifies whether this animated memory image should always be updated by sending the complete buffer of pixels whenever there is a change.
		/// </summary>
		/// <param name="fullbuffers"></param>
		public void setFullBufferUpdates(bool fullbuffers)
		{
		}
          

	}
}
