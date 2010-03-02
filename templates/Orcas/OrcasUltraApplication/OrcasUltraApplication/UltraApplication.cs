using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace OrcasUltraApplication
{

	//[Description("OrcasUltraApplication. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class UltraApplication
	{

		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world!" }.AttachToDocument();

			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachToDocument();

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachToDocument();

							}
						);

					};
			}

			// do we have javac configured? uncomment next line to use applets
			//this.CreateApplet();

			// do we jave mxmlc configured? uncomment next line to use sprites
			//this.CreateSprite();
		}


	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}
}
