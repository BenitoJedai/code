using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.Shared;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLInputElement.idl

	/// <summary>
	/// http://www.w3.org/TR/REC-html40/interact/forms.html#adef-type-INPUT
	/// </summary>
	[Script(InternalConstructor = true)]
	public class IHTMLInput : IHTMLElement
	{
		// http://true-coder.ru/html/trackbar-html5-i-ego-ispolzovanie.html
		[System.Obsolete("available for [type=range], change type if used?")]
		public int min;
		public int max;





		public string alt;
		public string src;
		public string autocomplete;

		public int maxLength;
		public int size;

		public HTMLInputTypeEnum type;
		public string value;

		// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms\Application.cs
		public string list;



		[System.Obsolete("Need to test Browsers")]
		public long valueAsNumber;
		public IDate valueAsDate;

		[Script(DefineAsStatic = true)]
		public System.DateTime GetDateTime()
		{
			//Tested by chrome android
			//Tested by: E:\jsc.svn\examples\javascript\Test\TestInputDateOperator\TestInputDateOperator\Application.cs
			if (valueAsDate == null)
				return System.DateTime.Now;
			return new __DateTime { InternalValue = valueAsDate, Kind = System.DateTimeKind.Local };
		}


		public bool disabled;

		// https://developer.mozilla.org/en-US/docs/Web/CSS/:indeterminate
		public bool @checked;
		// X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\CheckBox.cs
		public bool indeterminate;



		public bool @readOnly;

		public FileList files;

		[Script(DefineAsStatic = true)]
		public int GetInteger()
		{
			return int.Parse(value);
		}

		[Script(DefineAsStatic = true)]
		public double GetDouble()
		{
			return double.Parse(value);
		}

		public bool IsInteger
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return IRegExp.Integer.exec(value) != null;
			}
		}

		public bool IsCurrency
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return IRegExp.Currency.exec(value) != null;
			}
		}

		#region constructors
		public IHTMLInput() { }
		public IHTMLInput(HTMLInputTypeEnum type) { }
		public IHTMLInput(HTMLInputTypeEnum type, string value) { }
		public IHTMLInput(HTMLInputTypeEnum type, string name, string value) { }

		internal static IHTMLInput InternalConstructor()
		{
			return (IHTMLInput)(object)new IHTMLElement(HTMLElementEnum.input);
		}


		internal static IHTMLInput InternalConstructor(HTMLInputTypeEnum type)
		{
			IHTMLInput n = null;

			var _radio = HTMLInputTypeEnum.radio;

			if (type == _radio)
			{
				n = (IHTMLInput)new IFunction("e", "/*@cc_on return this.createElement(e); @*/ return null;").apply(Native.Document, "<input type='radio' name='' value='' />");
			}

			if (n == null)
			{
				n = new IHTMLInput();
				n.type = type;
			}

			return n;
		}

		internal static IHTMLInput InternalConstructor(HTMLInputTypeEnum type, string value)
		{
			IHTMLInput n = new IHTMLInput(type);

			n.value = value;

			return n;
		}

		internal static IHTMLInput InternalConstructor(HTMLInputTypeEnum type, string name, string value)
		{
			IHTMLInput n = null;

			var _radio = HTMLInputTypeEnum.radio;

			if (type == _radio)
			{
				// TODO: escape name and value

				n = (IHTMLInput)new IFunction("e", "/*@cc_on return this.createElement(e); @*/ return null;").apply(Native.Document,
					"<input type='radio' name='" + name + "' value='" + value + "' />"
					);
			}

			if (n == null)
			{
				n = new IHTMLInput();
				n.type = type;
				n.name = name;
				n.value = value;
			}

			return n;
		}

		#endregion


		public static IHTMLInput CreateRadio(string name, string value, bool @checked)
		{
			IHTMLInput n = null;

			string c = "";

			if (@checked)
				c = " checked='checked'";

			// packer shall not remove the cc statement

			n = (IHTMLInput)new IFunction("e", "/*@cc_on return this.createElement(e); @*/ return null;").apply(Native.Document,
				"<input type='radio' name='" + name + "' value='" + value + "'" + c + " />"
				);

			if (n == null)
			{
				n = new IHTMLInput(HTMLInputTypeEnum.radio, name, value);
				n.@checked = @checked;
			}

			return n;
		}



		public static IHTMLInput CreateCheckbox(string title)
		{
			IHTMLInput i = new IHTMLInput(HTMLInputTypeEnum.checkbox);

			i.title = title;

			return i;
		}

		// http://www.w3.org/TR/html-markup/spec.html
		public int selectionStart;
		public int selectionEnd;


		public static implicit operator bool (IHTMLInput i)
		{
			if (i.type == HTMLInputTypeEnum.checkbox)
			{
				// X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs

				return i.@checked;
			}

			return false;
		}

		public static implicit operator IHTMLInput(bool i)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Application.cs
			return new IHTMLInput { @checked = i };
		}


		public static implicit operator System.DateTime(IHTMLInput i)
		{
			if (i.type == HTMLInputTypeEnum.date)
			{
				return i.GetDateTime();
			}

			return System.DateTime.Now;
		}

		public static implicit operator string (IHTMLInput i)
		{
			// X:\jsc.svn\examples\javascript\css\Test\CSSSearchUserFeedback\CSSSearchUserFeedback\Application.cs

			//if (i.type == HTMLInputTypeEnum.text)
			{
				// X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs

				return i.value;
			}

			//return "";
		}

		//validity: ValidityState
		//badInput: false
		//customError: false
		//patternMismatch: false
		//rangeOverflow: false
		//rangeUnderflow: false
		//stepMismatch: false
		//tooLong: false
		//typeMismatch: false
		//valid: true
		//valueMissing: false


		public static implicit operator IHTMLInput(System.Type x)
		{
			// tested by?
			return new IHTMLInput { className = x.Name };
		}





		#region async
		[Script]
		public new class Tasks : IHTMLElement.Tasks<IHTMLInput>
		{
			[Script]
			public class TasksCheckedEvent
			{
				public static implicit operator bool (TasksCheckedEvent e)
				{
					// future C# may allow if (obj)
					// but for now booleans are needed

					// enable 
					// while (await Native.window.async.onresize);
					return ((object)e != null);
				}
			}

			[System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
			public Task<TasksCheckedEvent> @checked
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Application.cs

					var i = that;
					var y = new TaskCompletionSource<TasksCheckedEvent>();

					if (i.@checked)
					{
						y.SetResult(new TasksCheckedEvent());
					}
					else
					{
						i.onchange +=
							e =>
							{
								if (y == null)
									return;

								if (i.@checked)
								{
									y.SetResult(new TasksCheckedEvent());

									y = null;
								}
							};
					}

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
