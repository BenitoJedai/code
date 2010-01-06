using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Net;
using System.Web.Script.Services;

namespace OrcasMetaWebService1
{
	/// <summary>
	/// Summary description for Service1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	//[System.Web.Script.Services.ScriptService]
	public class Service1 : System.Web.Services.WebService
	{
		// http://stackoverflow.com/questions/2005752/how-to-force-asp-net-web-service-expose-only-http-post-interface

		[WebMethod(Description = "OrcasMetaWebService1. Crosscompiled from C# to Java for Google App Engine and PHP.")]
		public string HelloWorld()
		{



			var n = new ExampleCom();


			return "Hi there! :)" + n.Text.Length;
		}

		[WebMethod(Description = "OrcasMetaWebService1. Crosscompiled from C# to Java for Google App Engine and PHP.")]
		public string Howdy(string key, string value)
		{
			return key + ": " + value;
		}
	}
}
