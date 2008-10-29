using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class Card : ISupportsContainer
	{
		public readonly CardInfo Info;

		public Canvas Container { get; set; }


	}
}
