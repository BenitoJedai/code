using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon
{
	[Script]
	public sealed class AvalonSoundChannel
	{
		// this class is a wrapper for multiple platforms to provide
		// unified sound experience
		// currently only flash implements it tho...

		// do you know how to play mp3 from an embedded resource in WPF?

		public Action Start;
		public Action Stop;


		public Action<double> SetVolume;

		public AvalonSoundChannel()
		{
			this.Start = delegate { };
			this.Stop = delegate { };
			this.SetVolume = delegate { };
		}
				
		public event Action PlaybackComplete;

		public void RaisePlaybackComplete()
		{
			if (PlaybackComplete != null)
				PlaybackComplete();
		}

	
	}
}
