using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.ActionScript.DOM
{
	partial class ExternalContext
	{
		[Script]
		public partial class Token
		{
			public Action RequestToken;

			public void RaiseRequestToken()
			{
				if (this.TokenValue == null)
				{
					if (this.RequestToken != null)
						this.RequestToken();
				}
			}

	
			public ExternalContext Context;

			string _TokenValue;
			public string TokenValue
			{
				get
				{
					return _TokenValue;
				}
				set
				{
					_TokenValue = value;
					if (Changed != null)
						Changed();
				}
			}

			public event Action Changed;
		}

	}

}
