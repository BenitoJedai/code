using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Commands.Reference
{
	public class ReferenceWaveComponent
	{
		// each wav file added to the project will be created as
		// a SoundPlayer
		// a folder MySounds.WaveComponent
		// each wav file will created to a SoundPlayer instance
		// we should support wav file embedding
		// and prodceural generation like sfx
		// first target is C

		const string TextComponent = "WaveComponent";

		// this is an example of how jsc.meta
		// could be used as a code generator
		// before any jsc kicks in

		/// <summary>
		/// This is this csproj file. We should also support VB project file
		/// as they should really not be that different.
		/// </summary>
		public FileInfo ProjectFileName;


		public void Invoke()
		{
			
		}
	}
}
