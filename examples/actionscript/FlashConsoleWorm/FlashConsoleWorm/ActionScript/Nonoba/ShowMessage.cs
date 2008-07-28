using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	partial class Client
	{
		public Action<string> ShowMessage;

		void InitializeShowMessage()
		{

			#region Messages
			var ActiveMessages = new List<TextField>();
			var ShowMessageNow = default(Action<string, Action>);

			ShowMessageNow =
				(MessageText, Done) =>
				{

					var p = new TextField
					{
						textColor = 0x0,
						background = true,
						backgroundColor = 0xffffff,
						filters = new[] { new GlowFilter(0xffffff) },
						autoSize = TextFieldAutoSize.LEFT,
						text = MessageText,
						mouseEnabled = false
					};

					var y = DefaultControlHeight - p.height - 32;

					p.AddTo(ActiveMessages).AttachTo(this).MoveTo((DefaultControlWidth - p.width) / 2, DefaultControlHeight);

					//Sounds.snd_message.ToSoundAsset().play();

					var MessagesToBeMoved = (from TheMessage in ActiveMessages select new { TheMessage, y = TheMessage.y - TheMessage.height }).ToArray();



					(1000 / 24).AtInterval(
						t =>
						{
							foreach (var i in MessagesToBeMoved)
							{
								if (i.TheMessage.y > i.y)
									i.TheMessage.y -= 4;

							}

							p.y -= 4;

							if (p.y < y)
							{
								t.stop();

								if (Done != null)
									Done();

								500.AtDelayDo(
									delegate
									{
										p.alpha = 0.5;


									}
								);
								9000.AtDelayDo(
									() => p.RemoveFrom(ActiveMessages).FadeOutAndOrphanize(1000 / 24, 0.21)
								);
							}
						}
					);
				};


			var QueuedMessages = new Queue<string>();

			this.ShowMessage =
				Text =>
				{
					if (QueuedMessages.Count > 0)
					{
						QueuedMessages.Enqueue(Text);
						return;
					}

					// not busy
					QueuedMessages.Enqueue(Text);

					var NextQueuedMessages = default(Action);

					NextQueuedMessages =
						() => ShowMessageNow(QueuedMessages.Peek(),
							delegate
							{
								QueuedMessages.Dequeue();

								if (QueuedMessages.Count > 0)
									NextQueuedMessages();
							}
						);

					NextQueuedMessages();
				};
			#endregion

		}
	}
}
