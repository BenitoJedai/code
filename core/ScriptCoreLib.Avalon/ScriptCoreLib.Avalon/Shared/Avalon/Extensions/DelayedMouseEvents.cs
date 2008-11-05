using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public class DelayedMouseEvents
	{
		public Func<bool> ValidateMouseEnter;
		public Func<bool> ValidateMouseLeave;

		public event Action MouseEnter;
		public event Action MouseLeave;

		public int MouseEnterDelay = 200;
		public int MouseLeaveDelay = 200;

		public readonly UIElement Target;

		public DelayedMouseEvents(UIElement Target)
		{
			this.Target = Target;

			var MouseEnterEnabled = false;
			var MouseLeaveEnabled = false;

			Action MouseEnterAction =
				delegate
				{
					if (this.MouseEnter != null)
						this.MouseEnter();
				};

			Action MouseLeaveAction =
				delegate
				{
					if (this.MouseLeave != null)
						this.MouseLeave();
				};

			var MouseEnterDelayed = MouseEnterAction.ToFiltered(() => MouseEnterEnabled).ToDelayed(() => MouseEnterDelay);
			var MouseLeaveDelayed = MouseLeaveAction.ToFiltered(() => MouseLeaveEnabled).ToDelayed(() => MouseLeaveDelay);

			Target.MouseEnter +=
				delegate
				{
					MouseLeaveEnabled = false;

					if (ValidateMouseEnter != null)
						if (!ValidateMouseEnter())
							return;


					MouseEnterEnabled = true;
					MouseEnterDelayed();
				};

			Target.MouseLeave +=
				delegate
				{
					MouseEnterEnabled = false;

					if (ValidateMouseLeave != null)
						if (!ValidateMouseLeave())
							return;


					MouseLeaveEnabled = true;
					MouseLeaveDelayed();
				};
		}
	}

	[Script]
	public static class DelayedMouseEventsExtensions
	{
		public static DelayedMouseEvents ToDelayedMouseEvents(this UIElement Target)
		{
			return new DelayedMouseEvents(Target);
		}
	}
}
