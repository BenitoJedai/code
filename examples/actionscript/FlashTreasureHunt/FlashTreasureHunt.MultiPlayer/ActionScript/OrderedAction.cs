using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Sub actions cannot occur before the primary action has signalled
	/// </summary>
	[Script]
	public class OrderedAction
	{
		readonly Queue<Action> Queue = new Queue<Action>();

		public bool Ready
		{
			get
			{
				return _Signal;
			}
		}

		public void ContinueWhenDone(Action e)
		{
			if (_Signal)
			{
				e();
				return;
			}


			this.Queue.Enqueue(e);
		}

		bool _Signal;

		public void Signal()
		{
			_Signal = true;

			while (Queue.Count > 0)
				this.Queue.Dequeue()();
		}
	}
}
