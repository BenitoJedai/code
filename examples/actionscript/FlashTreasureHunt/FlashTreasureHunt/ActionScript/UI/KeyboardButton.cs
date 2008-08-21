using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript.UI
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


		bool CheckButton(uint button, uint location, bool ResetTickTimer)
		{

			if (Groups == null)
			{
				if (this.Buttons == null)
					return false;

				if (!this.Buttons.Contains(button))
					return false;

				if (ResetTickTimer)
					a = true;

				if (this.Filter != null && !this.Filter())
					return false;

				return true;
			}
			else
			{
				var g = Groups.Where(i => i.Group.Enabled && i.Buttons.Any(p => p.Button == button && p.FilterLocation(location))).FirstOrDefault();

				if (g != null)
				{
					if (this.Filter == null)
						return true;

					if (ResetTickTimer)
						a = true;

					//Console.WriteLine("keyCode " + button + " ok for " + g.Group.Name);
					return this.Filter();
				}


				return false;
			}
		}

		public Action Tick;

		public KeyboardButton(Stage s)
			: this(s, 1000 / 30)
		{
		}

		public readonly Action ForceKeyDown;
		public readonly Action ForceKeyUp;

		bool a = false;

		public KeyboardButton(Stage s, int fps)
		{
			var t = new Timer(fps);

			t.timer +=
				delegate
				{
					if (a)
					{
						a = false;
						t.stop();
						return;
					}

					if (this.Tick != null)
						this.Tick();
				};

			this.ForceKeyDown =
				delegate
				{
					a = false;

					if (this.Down != null)
						this.Down();

					if (!t.running)
					{
						if (this.Tick != null)
							this.Tick();

						t.start();
					}
				};

			s.keyDown +=
				e =>
				{

					if (CheckButton(e.keyCode, e.keyLocation, false))
					{
						this.ForceKeyDown();
					}
				};


			this.ForceKeyUp =
				delegate
				{
					if (this.Up != null)
						this.Up();

					a = true;
				};

			s.keyUp +=
				  e =>
				  {

					  if (CheckButton(e.keyCode, e.keyLocation, true))
					  {
						  this.ForceKeyUp();
					  }
				  };
		}
	}

}
