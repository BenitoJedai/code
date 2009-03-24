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
		public class Token
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

			[Script]
			public class Property
			{

				public readonly Token PropertyToken;
				public readonly string PropertyName;
				public Property(Token Token, string PropertyName)
				{
					this.PropertyToken = Token;
					this.PropertyName = PropertyName;

				}

				bool _PropertyValueDirty;
				object _PropertyValue;
				public object PropertyValue
				{
					set
					{
						if (this.PropertyToken == null)
						{
							throw new ArgumentNullException("PropertyToken");
						}

						if (this.PropertyToken.Context == null)
						{
							_PropertyValue = value;

							if (!_PropertyValueDirty)
							{
								_PropertyValueDirty = true;
								this.PropertyToken.Changed +=
									delegate
									{
										this.PropertyValue = _PropertyValue;
										this._PropertyValue = null;
									};
							}
							return;
						}

						this.PropertyToken.RaiseRequestToken();

						if (this.PropertyToken.TokenValue == null)
							throw new ArgumentNullException("PropertyToken.TokenValue");

						this.PropertyToken.Context.ExternalContext_token_set_property(
							this.PropertyToken.TokenValue, this.PropertyName, value
						);

					}
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
