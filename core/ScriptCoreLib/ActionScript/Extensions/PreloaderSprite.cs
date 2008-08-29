using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions
{
	/// <summary>
	/// To be used with FrameAttribute and TypeOfByNameOverrideAttribute
	/// </summary>
	[Script]
	public abstract class PreloaderSprite : Sprite
	{
		public abstract DisplayObject CreateInstance();

		public virtual bool AutoCreateInstance()
		{
			return true;
		}

		public PreloaderSprite()
		{
			this.LoadingComplete +=
				delegate
				{
					if (AutoCreateInstance())
						CreateInstance().AttachTo(this);
				};
		}

		public event Action LoadingComplete
		{
			add
			{
				var e = default(Action<Event>);

				e = delegate
				{
					if (root.loaderInfo.bytesLoaded == root.loaderInfo.bytesTotal)
					{
						this.enterFrame -= e;
						value();
					}


				};

				this.enterFrame += e;

			}
			remove
			{
			}
		}

		public event Action LoadingInProgress
		{
			add
			{
				var e = default(Action<Event>);

				e = delegate
				{
					if (root.loaderInfo.bytesLoaded == root.loaderInfo.bytesTotal)
					{
						this.enterFrame -= e;
						return;
					}

					value();
				};

				this.enterFrame += e;

			}
			remove
			{

			}
		}
	}

}
