using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OrcasAppEngieWebService
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

		[WebMethod]
		public string HelloWorld()
		{
//POST /Service1.asmx/HelloWorld HTTP/1.1
//Host: localhost
//Content-Type: application/x-www-form-urlencoded
//Content-Length: length

//HTTP/1.1 200 OK
//Content-Type: text/xml; charset=utf-8
//Content-Length: length

//<?xml version="1.0" encoding="utf-8"?>
//<string xmlns="http://tempuri.org/">string</string>

			return "Hello World";
		}
	}
}
