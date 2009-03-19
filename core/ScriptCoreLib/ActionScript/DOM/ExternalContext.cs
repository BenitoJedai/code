using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;

namespace ScriptCoreLib.ActionScript.DOM
{
	[Script]
	public partial class ExternalContext
	{
		// dom objects should be referenced by id once they are on the dom
		// if they are taken from the dom
		// they can be stored on the cache to be referenced later
		// when disposing a dom tree, the references shall be removed

		readonly string a;
		readonly string b;
		int c;

		string InternalId;
		IHTMLObject InternalElement;
		public event Action ElementChanged;

		public IHTMLObject Element
		{
			get
			{
				return InternalElement;
			}
		}

		public IHTMLDocument Document
		{
			get
			{
				return Element.ownerDocument;
			}
		}

		public event Action<string> Trace;

		public void RaiseTrace(string e)
		{
			if (Trace != null)
				Trace(e);
		}

		public ExternalContext()
		{
			var r = new Random();
			a = "_" + r.Next(0x7ffffff);
			b = "_" + r.Next(0x7ffffff);

			#region ExternalAuthentication
			var Handler = default(Converter<string, string>);

			Handler =
				value =>
				{
					if (value == a)
					{
						Handler =
							id =>
							{
								InternalId = id;
								InternalElement = new IHTMLObject { context = this, id = id };

								Handler = e => "";

								if (ElementChanged != null)
									ElementChanged();

								return "";
							};


						return b;
					}

					return value;
				};

			a.External(
				(string e) => Handler(e)
			);
			#endregion


			this.SetElementPropertyString = ToExternal<string, string, string>(
				"_id", "_key", "_value",
				"document.getElementById(_id)[_key] = _value;"
			);

			this.GetGlobalPropertyString = ToExternalConverter<string, string, string>(
				"_id", "_key",
				"return window[_id][_key];"
			);

			this.SetGlobalPropertyString = ToExternal<string, string, string>(
				"_id", "_key", "_value",
				"window[_id][_key] = _value;"
			);

			this.ExternalContext_IHTMLDocument_createElement = ToExternal<string, string>(
				"_tag", "_i",
				"window[_i] = document.createElement(_tag);"
			);

			this.ExternalContext_IHTMLDocument_get_body = ToExternal<string>(
				"_i",
				"window[_i] = document.body;"
			);

			this.ExternalContext_IHTMLElement_set_innerHTML = ToExternal<string, string>(
				"_value", "_i",
				"window[_i].innerHTML = _value;"
			);

			this.ExternalContext_IHTMLElement_appendChild = ToExternal<string, string>(
				"_parent", "_child",
				"window[_parent].appendChild(window[_child]);"
			);

			this.ExternalContext_getElementById_call_string = ToExternal<string, string, string>(
				"_i", "_f", "_a0",
				"document.getElementById(_i)[_f](_a0);"
			);

			this.ExternalContext_getElementById_call = ToExternal<string, string>(
				"_i", "_f", 
				"document.getElementById(_i)[_f]();"
			);


			// http://mihai.bazon.net/blog/flash-s-externalinterface-and-ie
			// http://swfupload.org/forum/generaldiscussion/985

			// ie is unable to load flash external interface if the object was created by innerHTML
			this.ExternalContext_getElementById_add_event = ToExternal<string, string, string, string>(
				"_i", "_f", "_j", "_h",
				// attachEvent = on
//                @"
//				var _element = document.getElementById(_i); 
//				if ('attachEvent' in _element) alert('ie event');
//				else if ('addEventListener' in _element) alert('ff event');
//				"
				@"
				var _element = document.getElementById(_i); 
				if ('addEventListener' in _element) 
				{
					_element.addEventListener(_f,
						function ()
						{
							document.getElementById(_j)[_h](); 
						}, false	
					);
				}
				"
			);
		}

		
		public readonly Action<string, string, string> SetElementPropertyString;
		public readonly Action<string, string, string> SetGlobalPropertyString;
		public readonly Converter<string, string, string> GetGlobalPropertyString;

		public readonly Action<string, string> ExternalContext_IHTMLDocument_createElement;
		public readonly Action<string> ExternalContext_IHTMLDocument_get_body;
		public readonly Action<string, string> ExternalContext_IHTMLElement_set_innerHTML;
		public readonly Action<string, string> ExternalContext_IHTMLElement_appendChild;


		public readonly Action<string, string> ExternalContext_getElementById_call;
		public readonly Action<string, string, string> ExternalContext_getElementById_call_string;

		public readonly Action<string, string, string, string> ExternalContext_getElementById_add_event;

		public string CreateToken()
		{
			c++;

			var f = a + "_" + c;

			return f;
		}


		public void ExternalAuthentication()
		{
			if (InternalId != null)
				return;

			var _SetPropertyString = a + "_SetPropertyString";


			1.ExternalAtDelay(@"


				
				var x = document.getElementsByTagName('object');

				for (var i = 0; i < x.length; i++)
				{
					try
					{
						var h = x[i]['" + a + @"']('" + a + @"');

						if (h == '" + b + @"')
						{
							x[i]['" + a + @"'](x[i].id);
							break;
						}
					}
					catch (ex)
					{
						

					}
				}
			");
		}


	}

}
