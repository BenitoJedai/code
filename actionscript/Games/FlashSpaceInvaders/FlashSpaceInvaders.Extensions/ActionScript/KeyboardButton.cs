using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class KeyboardButton
	{
		public KeyboardButtonGroupInfo[] Groups { get; set; }

		public Action Up;
		public Action Down;

		/// <summary>
		/// When the filter is set it must return true in order to fire keyboard events
		/// </summary>
		public Func<bool> Filter;

		public uint[] Buttons;


		bool CheckButton(uint button, uint location)
		{

			if (Groups == null)
			{
				if (this.Buttons == null)
					return false;

				if (!this.Buttons.Contains(button))
					return false;

				if (this.Filter != null && !this.Filter())
					return false;

				return true;
			}
			else
			{
				var g = Groups.Where(i => i.Group.Enabled && i.Buttons.Any(p => p.Button == button && p.FilterLocation(location))).FirstOrDefault();

				if (g != null)
				{
					if (this.Filter != null && !this.Filter())
						return false;

					//Console.WriteLine("keyCode " + button + " ok for " + g.Group.Name);
					return true;
				}


				return false;
			}
		}

		public Action Tick;

		public KeyboardButton(Stage s)
		{
			var t = new Timer(1000 / 30);

			t.timer +=
				delegate
				{
					if (this.Tick != null)
						this.Tick();
				};

			s.keyDown +=
				e =>
				{

					if (CheckButton(e.keyCode, e.keyLocation))
					{
						if (this.Down != null)
							this.Down();

						t.start();
					}
				};

			s.keyUp +=
				  e =>
				  {

					  if (CheckButton(e.keyCode, e.keyLocation))
					  {
						  if (this.Up != null)
							  this.Up();

						  t.stop();
					  }
				  };
		}
	}

}
