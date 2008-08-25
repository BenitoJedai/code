using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript
{

	[Script]
	public class JoinAction
	{
		int _counter = 0;
		Action _done;

		public JoinAction(Action done, int counter)
		{
			_done = done;
			_counter = counter;
		}

		public void Signal()
		{
			_counter--;

			if (_counter == 0)
			{
				_done();
				_done = null;
			}
		}
	}
}
