using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.text;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class DebugDumpTextField
	{
		public readonly TextField Field;

		public readonly BooleanProperty Visible = false;

		public DebugDumpTextField()
		{


			var TextInfo = new TextField
			{



				textColor = Colors.White,
				embedFonts = true,

				mouseEnabled = false,

				defaultTextFormat = new TextFormat
				{
					font = Fonts.FontFixedSys,
					size = 12,
				},
				//selectable = false,
				condenseWhite = false,

				background = true,
				backgroundColor = 0x101010,

				multiline = true,
				text = "",
			};



			this.Field = TextInfo;

			#region info


			var DebugDumpQueue = new Queue<string>();
			this.DebugDumpUpdate =
				delegate
				{
					if (TextInfo.parent == null)
						return;

					var w = new StringBuilder();

					foreach (var v in DebugDumpQueue)
					{
						w.AppendLine(v);
					}

					TextInfo.text = w.ToString();
				};
			this.Visible.ValueChangedToTrue +=
							delegate
							{
								TextInfo.alpha = 1;

								DebugDumpUpdate();

							};

			this.Visible.ValueChangedToFalse +=
				delegate
				{
					TextInfo.FadeOutAndOrphanize();

				};
			Write =
				o =>
				{
					if (DebugDumpQueue.Count > 16)
						DebugDumpQueue.Dequeue();

					DebugDumpQueue.Enqueue(o.ToString());

					DebugDumpUpdate();
				};


			#endregion


		}

		public readonly Action DebugDumpUpdate;

		public readonly Action<object> Write;

	}
}
