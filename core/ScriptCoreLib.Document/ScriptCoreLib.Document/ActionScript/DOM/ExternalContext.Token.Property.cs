using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;
using ScriptCoreLib.Shared;
using System.Diagnostics;

namespace ScriptCoreLib.ActionScript.DOM
{
	partial class ExternalContext
	{
		partial class Token
		{

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

		}

	}

	internal static class CodeGenerator
	{
		public static void CreateProperties(string names)
		{
			var properties = names.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()).ToArray();

			var w = new StringBuilder();

			w.AppendLine("#region Properties");

			w.AppendLine("partial void InitializeProperties()");
			w.AppendLine("{");
			foreach (var p in properties)
			{
				w.AppendLine("	this.__" + p + " = new ExternalContext.Token.Property(this.Token, \"" + p + "\");");
			}
			w.AppendLine("}");
			w.AppendLine();

			foreach (var p in properties)
			{

			w.AppendLine(@"
		public string " + p + @"
		{
			set
			{
				this.__" + p + @".PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __" + p + @";
		");
			}

			w.AppendLine("#endregion");

			var text = w.ToString();

			// break here to copy the multiline text to clipboard


			Debugger.Break();
		}
	}

}
