using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;

namespace MP3PitchExample.ActionScript
{
	[Script]
	public class MP3Pitch
	{
		private const int BLOCK_SIZE = 3072;

		public MP3Pitch(string url)
		{
			var _target = new ByteArray();

			var _mp3 = new Sound();

			_mp3.load(new URLRequest(url));

			var _position = 0.0;
			var _rate = 1.0;

			var _sound = new Sound();

			_sound.sampleData +=
				e =>
				{
					//-- REUSE INSTEAD OF RECREATION
					_target.position = 0;

					//-- SHORTCUT
					var data = e.data;

					var scaledBlockSize = BLOCK_SIZE * _rate;
					var positionInt = Convert.ToInt32(_position);
					var alpha = _position - positionInt;

					var positionTargetNum = alpha;
					var positionTargetInt = -1;

					//-- COMPUTE NUMBER OF SAMPLES NEED TO PROCESS BLOCK (+2 FOR INTERPOLATION)
					var need = Convert.ToInt32(Math.Ceiling(scaledBlockSize) + 2);

					//-- EXTRACT SAMPLES
					var read = (int)_mp3.extract(_target, need, positionInt);

					var n = BLOCK_SIZE;
					if (read != need)
						n = Convert.ToInt32(read / _rate);

					var l0 = .0;
					var r0 = .0;
					var l1 = .0;
					var r1 = .0;

					var i = 0;
					for (; i < n; i++)
					{
						//-- AVOID READING EQUAL SAMPLES, IF RATE < 1.0
						if (Convert.ToInt32(positionTargetNum) != positionTargetInt)
						{
							positionTargetInt = Convert.ToInt32(positionTargetNum);

							//-- SET TARGET READ POSITION
							_target.position = (uint)(positionTargetInt << 3);

							//-- READ TWO STEREO SAMPLES FOR LINEAR INTERPOLATION
							l0 = _target.readFloat();
							r0 = _target.readFloat();

							l1 = _target.readFloat();
							r1 = _target.readFloat();
						}

						//-- WRITE INTERPOLATED AMPLITUDES INTO STREAM
						data.writeFloat(l0 + alpha * (l1 - l0));
						data.writeFloat(r0 + alpha * (r1 - r0));

						//-- INCREASE TARGET POSITION
						positionTargetNum += _rate;

						//-- INCREASE FRACTION AND CLAMP BETWEEN 0 AND 1
						alpha += _rate;
						while (alpha >= 1.0) --alpha;
					}

					//-- FILL REST OF STREAM WITH ZEROs
					if (i < BLOCK_SIZE)
					{
						while (i < BLOCK_SIZE)
						{
							data.writeFloat(0.0);
							data.writeFloat(0.0);

							++i;
						}
					}

					//-- INCREASE SOUND POSITION
					_position += scaledBlockSize;
				};

			_mp3.complete +=
				e =>
				{
					_sound.play();
				};

		}
	}
}
