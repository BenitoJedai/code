using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OrcasAppEngieWebService
{
	/// <summary>
	/// Summary description for WebService1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class WebService1 : System.Web.Services.WebService
	{
		// step 1. expose wsdl
		// step 2. consume wsdl
		// step 3. ??
		// step 4. profit :)

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		[WebMethod]
		public string HelloWorld2(string a, string b)
		{
			//POST /WebService1.asmx/HelloWorld2 HTTP/1.1
			//Host: localhost
			//Content-Type: application/x-www-form-urlencoded
			//Content-Length: length

			//a=string&b=string

			//HTTP/1.1 200 OK
			//Content-Type: text/xml; charset=utf-8
			//Content-Length: length

			//<?xml version="1.0" encoding="utf-8"?>
			//<string xmlns="http://tempuri.org/">string</string>

			return "Hello World : " + a + " " + b;
		}
	}
}
