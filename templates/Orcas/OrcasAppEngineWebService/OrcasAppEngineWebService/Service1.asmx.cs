using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;

namespace OrcasAppEngineWebService
{
	/// <summary>
	/// Summary description for Service1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Service1 : System.Web.Services.WebService
	{
		// http://jsc-project.appspot.com/Service1.asmx/

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		[WebMethod]
		public string LoremIpsum(string word1)
		{
			var u = new Uri("http://example.com/");
			var c = new WebClient();

			var x = c.DownloadString(u);

			return "Lorem Ipsum: " + word1 + " : " + x;
		}
	}
}
