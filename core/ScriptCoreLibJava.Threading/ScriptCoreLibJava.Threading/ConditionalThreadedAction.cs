using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.Threading
{


	[Script]
	public abstract class ConditionalThreadedAction : ThreadedAction
	{
		[Script]
		public interface ICondition
		{
			bool Value { get; }
		}

		public ICondition Condition;

		[Script]
		public class While : ICondition, IDisposable
		{
			public While()
			{
				this.Value = true;
			}

			#region IDisposable Members

			public void Dispose()
			{
				this.Value = false;
			}

			#endregion

			#region ICondition Members

			public bool Value
			{
				get;
				set;
			}

			#endregion
		}
		public sealed override void Invoke()
		{
			if (Condition.Value)
				ConditionalInvoke();

		}

		public abstract void ConditionalInvoke();
	}

}
