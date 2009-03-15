using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;

namespace ScriptCoreLib.ActionScript.DOM
{
	[Script]
	public class ExternalContext
	{
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


			this.SetElementPropertyString = ToExternal(
				"_id", "_key", "_value",
				"document.getElementById(_id)[_key] = _value;"
			);

			this.SetGlobalPropertyString = ToExternal(
				"_id", "_key", "_value",
				"window[_id][_key] = _value;"
			);



		}

		public string CreateToken()
		{
			c++;

			var f = a + "_" + c;

			return f;
		}

		public Converter<string, string> ToExternalConverter(string a0, string code)
		{
			var f = CreateToken();

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + @") { " + code + @" };"
			);

			return (x0) => (string)f.External(x0);
		}




		public Action<string> ToExternal(string a0, string code)
		{
			var f = CreateToken();

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + @") { " + code + @" };"
			);

			return (x0) => f.External(x0);
		}

		public Action<string, string> ToExternal(string a0, string a1, string code)
		{
			c++;

			var f = a + "_" + c;

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + ", " + a1 + @") { " + code + @" };"
			);

			return (x0, x1) => f.External(x0, x1);
		}

		public Action<string, string, string> ToExternal(string a0, string a1, string a2, string code)
		{
			var f = CreateToken();

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + @") { " + code + @" };"
			);

			return (x0, x1, x2) => f.External(x0, x1, x2);
		}

		public Action<A0, A1, A2, A3> ToExternal<A0, A1, A2, A3>(string a0, string a1, string a2, string a3, string code)
		{
			var f = CreateToken();

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + ", " + a3 + @") { " + code + @" };"
			);

			return (x0, x1, x2, x3) => f.External(x0, x1, x2, x3);
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


		public readonly Action<string, string, string> SetElementPropertyString;
		public readonly Action<string, string, string> SetGlobalPropertyString;

	}

}
