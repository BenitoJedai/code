using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra;
using ScriptCoreLib.Delegates;

namespace jsc.meta.Library.Mashups
{
	class UltraApplicationWithTier
	{
		public UltraApplicationWithTier()
		{
			Async<string, string> Function1 =
				(Data, Yield) =>
				{
					Tier.Flash();

					// running in a plugin

					Yield("from flash");
				};

			Action<string> YieldToClosure =
				Data =>
				{
					// javascript got something

					new CodeAtServerOrBrowser().Invoke();

					Function1(Data,
						DataFromPlugin =>
						{
							// back from flash
						}
					);
				};

			Action<string, Action<string>> StringConvert =
				(Data, Yield) =>
				{
					Tier.Server();

					new CodeAtServerOrBrowser().Invoke();

					// we cannot reference browser API at this point.
					// we can reference server API at this point

					Yield("Server Yield");
					YieldToClosure("Server YieldToClosure");
				};
		}
	}

	class CodeAtServerOrBrowser
	{
		public void Invoke()
		{

		}
	}
}
