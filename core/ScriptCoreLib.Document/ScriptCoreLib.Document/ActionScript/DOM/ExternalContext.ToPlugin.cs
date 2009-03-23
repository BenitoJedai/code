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


		public void ToPlugin(string Function, string ActiveX, string ContentType, Action<string> Handler)
		{
			var InstanceToken = this.CreateToken();
			var HandlerToken = this.CreateToken();

			HandlerToken.External(
				(bool status) =>
				{
					if (status)
					{
						Handler(InstanceToken);
					}
					else
					{
						// todo: delete instance token
						Handler(null);
					}

					return false;
				}
			);

			1.ExternalAtDelay(@"
				
				var _f = null;				

				if (typeof " + Function + @" != 'undefined') 
				{
//					alert('google.gears ff');
					_f = new " + Function + @"();
				} else {
					try
					{
						_f = new ActiveXObject('" + ActiveX + @"');
//						alert('google.gears ie');
					}
					catch (ie)
					{
					  // Safari
					  if ((typeof navigator.mimeTypes != 'undefined')
						   && navigator.mimeTypes['" + ContentType + @"'])
					  {
						
						_f = document.createElement('object');
						_f.style.display = 'none';
						_f.width = 0;
						_f.height = 0;
						_f.type = '" + ContentType + @"';



						document.body.appendChild(_f);
// alert('google.gears chrome');

					  }
					else {}
//						alert('google.gears missing');
					}
				}


				window['" + InstanceToken + @"'] = _f;

				setTimeout(
					function ()
					{
						document.getElementById('" + this.Element.id + "')['" + HandlerToken + @"'](_f != null);
					}, 1);
			
				
			");

		}
	}

}
