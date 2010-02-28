using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib;
namespace jsc.meta.Library.Templates.JavaScript
{
	[Script(InternalConstructor = true)]
	public class NamedAudio : global::ScriptCoreLib.JavaScript.DOM.HTML.IHTMLAudio
	{
		internal const string IHTMLAudio_src = "IHTMLAudio_src";

		public NamedAudio()
		{

		}

		public static NamedAudio InternalConstructor()
		{
			var i = new IHTMLAudio { src = IHTMLAudio_src };


			return (NamedAudio)i;
		}


		public static int AudioFileSize
		{
			get
			{
				return NamedAudioInformation.GetAudioFileSize();
			}
		}
	}

	public static class NamedAudioInformation
	{
		// note: this type is shared between variations FromWeb, FromBase64, FromAssets

	

		public static int GetAudioFileSize()
		{
			throw new NotImplementedException();
		}


	}
}
