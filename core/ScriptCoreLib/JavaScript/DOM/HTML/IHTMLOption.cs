using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLOptionElement.idl


	[Script(InternalConstructor = true)]
	public class IHTMLOption : IHTMLElement<IHTMLOption>
	{
		public string value;
		public bool selected;


		#region Constructor

		public IHTMLOption()
		{
			// InternalConstructor
		}

		static IHTMLOption InternalConstructor()
		{
			return (IHTMLOption)((object)new IHTMLElement(HTMLElementEnum.option));
		}

		#endregion


		// http://www.tasharen.com/forum/index.php?topic=9418.0

		#region async
		[Script]
		public new class Tasks : IHTMLElement.Tasks<IHTMLOption>
		{
			[Script]
			public class TasksSelectedEvent
			{
				public static implicit operator bool (TasksSelectedEvent e)
				{
					// future C# may allow if (obj)
					// but for now booleans are needed

					// enable 
					// while (await Native.window.async.onresize);
					return ((object)e != null);
				}
			}

			public Task<TasksSelectedEvent> selected
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms\Application.cs
					var y = new TaskCompletionSource<TasksSelectedEvent>();

					// are we in a select?

					var select = ((IHTMLSelect)that.parentNode);

					var o = select[select.selectedIndex];
					if (o == that)
					{
						y.SetResult(new TasksSelectedEvent());
					}
					else
					{
						this.onselect.ContinueWith(
							delegate
							{
								y.SetResult(new TasksSelectedEvent());
							}
						);
					}

					return y.Task;
				}
			}

			[System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
			public Task<IEvent<IHTMLSelect>> onselect
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms\Application.cs
					var y = new TaskCompletionSource<IEvent<IHTMLSelect>>();

					// are we in a select?

					var select = ((IHTMLSelect)that.parentNode);

					select.onchange +=
						e =>
						{
							if (y == null)
								return;

							var o = select[select.selectedIndex];

							if (o == that)
							{
								y.SetResult(e);
								y = null;
							}
						};

					return y.Task;
				}
			}

			[System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
			public Task<IEvent<IHTMLSelect>> ondeselect
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms\Application.cs
					var y = new TaskCompletionSource<IEvent<IHTMLSelect>>();

					// are we in a select?

					var select = ((IHTMLSelect)that.parentNode);

					select.onchange +=
						e =>
						{
							if (y == null)
								return;

							var o = select[select.selectedIndex];

							if (o != that)
							{
								y.SetResult(e);
								y = null;
							}
						};

					return y.Task;
				}
			}
		}

		[System.Obsolete("is this the best way to expose events as async?")]
		public new Tasks async
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return new Tasks { that = this };
			}
		}
		#endregion
	}
}
