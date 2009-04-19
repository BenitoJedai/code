﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.AppJet
{
	// http://appjet.com/docs/librefbrowser?page=request
	[Script(HasNoPrototype = true)]
	public class Request
	{
		/// <summary>
		/// Whether the curent HTTP request is a GET request.
		/// </summary>
		public bool isGet;

		/// <summary>
		/// The request path following the hostname. For example, if the user is visiting yourapp.appjet.net/foo, then this will be set to "/foo". This does not include CGI parameters or the domain name, and always begins with a "/".
		/// </summary>
		public string path;

		/// <summary>
		/// The value request query string. For example, if the user visits "yourapp.appjet.net/foo?id=20", then request.query will be "id=20".
		/// </summary>
		public string query;

		/// <summary>
		/// Either "GET" or "POST" (uppercase).
		/// </summary>
		public string method;
	}
}
