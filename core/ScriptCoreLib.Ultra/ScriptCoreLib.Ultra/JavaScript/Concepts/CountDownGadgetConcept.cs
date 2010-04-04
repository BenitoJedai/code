using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Concepts
{
	public interface ICountDownGadgetConcept : IUltraComponent
	{
		IHTMLDiv GadgetContainer { get; set; }
		IHTMLSpan DayCount { get; set; }
		IHTMLSpan EventName { get; set; }
	}

	public class CountDownGadgetConcept
	{
		public ICountDownGadgetConcept Element { get; private set; }


		DateTime InternalEvent;
		public DateTime Event
		{
			get
			{
				return InternalEvent;
			}
			set
			{
				InternalEvent = value;
				Update();
			}
		}

		public CountDownGadgetConcept(Func<ICountDownGadgetConcept> Constructor)
		{
			this.Element = Constructor();
		}

		private void Update()
		{
			var diff = Event - DateTime.Now;


			if (this.ShowOnlyDays)
			{
				this.Element.DayCount.innerText = "" + diff.Days;
			}
			else
			{
				this.Element.DayCount.innerText = "" + diff;
			}

		}

		public static implicit operator IHTMLDiv(CountDownGadgetConcept e)
		{
			return e.Element.GadgetContainer;
		}

		public bool ShowOnlyDays { get; set; }

		public bool AutoUpdate
		{
			set
			{
				new Timer(
					delegate
					{
						Update();
					}
				).StartInterval(1000);
			}
		}
	}

	public static class CountDownGadgetConceptExtensions
	{


	}
}
