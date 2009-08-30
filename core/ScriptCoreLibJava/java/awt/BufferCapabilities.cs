// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/BufferCapabilities.html
	[Script(IsNative = true)]
	public class BufferCapabilities
	{
		[Script(IsNative = true)]
		public class FlipContents
		{
			
		}

		/// <summary>
		/// Creates a new object for specifying buffering capabilities
		/// </summary>
		public BufferCapabilities(ImageCapabilities @frontCaps, ImageCapabilities @backCaps, BufferCapabilities.FlipContents @flipContents)
		{
		}

		/// <summary>
		/// Creates and returns a copy of this object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// 
		/// </summary>
		public ImageCapabilities getBackBufferCapabilities()
		{
			return default(ImageCapabilities);
		}

		/// <summary>
		/// 
		/// </summary>
		public BufferCapabilities.FlipContents getFlipContents()
		{
			return default(BufferCapabilities.FlipContents);
		}

		/// <summary>
		/// 
		/// </summary>
		public ImageCapabilities getFrontBufferCapabilities()
		{
			return default(ImageCapabilities);
		}

		/// <summary>
		/// 
		/// </summary>
		public bool isFullScreenRequired()
		{
			return default(bool);
		}

		/// <summary>
		/// 
		/// </summary>
		public bool isMultiBufferAvailable()
		{
			return default(bool);
		}

		/// <summary>
		/// 
		/// </summary>
		public bool isPageFlipping()
		{
			return default(bool);
		}

	}
}

