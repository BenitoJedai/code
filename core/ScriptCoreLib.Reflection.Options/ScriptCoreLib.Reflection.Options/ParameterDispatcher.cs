using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Reflection.Options
{
	internal class ParameterDispatcher : IEnumerable
	{

	

		public delegate void OptionAction(Option o);

		public class Option
		{
			public string Name;
			public OptionAction Handler;

			public Option Next;

			public ObjectFunc GetArguments;
		}

		Option Options;

		public void Add(Action Handler)
		{
			Add(
				new Option
				{
					Name = Handler.Target.GetType().Name,
					GetArguments = () => Handler.Target,
					Handler = delegate { Handler(); }
				}
			);
		}



		void Add(Option o)
		{
			o.Next = this.Options;
			this.Options = o;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			var p = this.Options;

			return new FuncEnumerator(
				delegate
				{
					var z = p;

					if (z != null)
						p = z.Next;

					return z;
				}
			);
		}

		#endregion



		public Option this[string Name]
		{
			get
			{
				var r = default(Option);

				foreach (Option k in this)
				{
					if (Name == null)
						if (k.Name == null)
						{
							r = k;
							break;
						}

					if (k.Name != null)
						if (k.Name == Name)
						{
							r = k;
							break;
						}
				}

				return r;

			}
		}
	}


}
