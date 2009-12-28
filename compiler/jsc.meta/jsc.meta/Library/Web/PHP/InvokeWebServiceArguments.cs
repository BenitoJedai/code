﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.Web.PHP
{
	public class InvokeWebServiceArguments
	{
		// this class is not directly aware of native API

		public string Content;

		public string DocumentContent;

		internal void RenderMethodsToDocumentContent()
		{
			var a = new[] {
					"foo", 
					"bar"
				};

			RenderMethodsToDocumentContent(a);
		}

		public string ServiceName;

		public void RenderMethodsToDocumentContent(string[] Methods)
		{
			// we are server side
			// we are rendering a html document
			// we could use ScriptCoreLib.Document if it were ready for serverside DOM

			var w = new StringBuilder();

			w.AppendLine(@"

<html>

    <head><link rel='alternate' type='text/xml' href='?disco' />

    <style type='text/css'>
    
		BODY { color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }
		#content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }
		A:link { color: #336699; font-weight: bold; text-decoration: underline; }
		A:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }
		A:active { color: #336699; font-weight: bold; text-decoration: underline; }
		A:hover { color: cc3300; font-weight: bold; text-decoration: underline; }
		P { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana; }
		pre { background-color: #e5e5cc; padding: 5px; font-family: Courier New; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid; }
		td { color: #000000; font-family: Verdana; font-size: .7em; }
		h2 { font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #003366; margin-left: -15px; color: #003366; }
		h3 { font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px; }
		ul { margin-top: 10px; margin-left: 20px; }
		ol { margin-top: 10px; margin-left: 20px; }
		li { margin-top: 10px; color: #000000; }
		font.value { color: darkblue; font: bold; }
		font.key { color: darkgreen; font: bold; }
		font.error { color: darkred; font: bold; }
		.heading1 { color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 105%; }
		.button { background-color: #dcdcdc; font-family: Verdana; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid; }
		.frmheader { color: #000000; background: #dcdcdc; font-family: Verdana; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px; }
		.frmtext { font-family: Verdana; font-size: .7em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px; }
		.frmInput { font-family: Verdana; font-size: 1em; }
		.intro { margin-left: -15px; }
           
    </style>

    <title>
	WebService1 Web Service
</title></head>

  <body>

    <div id='content'>

      <p class='heading1'>WebService1</p><br>

      

      <span>

          <p class='intro'>The following operations are supported.  For a formal definition, please review the <a href='?WSDL'>Service Description</a>. </p>

");
			w.AppendLine("<ul>");

			foreach (var item in Methods)
			{
				w.AppendLine("<li><a href='?op=" + item + "'>" + item + "</a></li>");

			}

			w.AppendLine("</ul>");


			w.AppendLine(@"

   </span>

      
      

    <span>
        
    </span>
    
      <span>
          <hr>
          <h3>This web service is using http://tempuri.org/ as its default namespace.</h3>
          <h3>Recommendation: Change the default namespace before the XML Web service is made public.</h3>

          <p class='intro'>Each XML Web service needs a unique namespace in order for client applications to distinguish it from other services on the Web. http://tempuri.org/ is available for XML Web services that are under development, but published XML Web services should use a more permanent namespace.</p>
          <p class='intro'>Your XML Web service should be identified by a namespace that you control. For example, you can use your company's Internet domain name as part of the namespace. Although many XML Web service namespaces look like URLs, they need not point to actual resources on the Web. (XML Web service namespaces are URIs.)</p>
          <p class='intro'>For XML Web services creating using ASP.NET, the default namespace can be changed using the WebService attribute's Namespace property. The WebService attribute is an attribute applied to the class that contains the XML Web service methods. Below is a code example that sets the namespace to 'http://microsoft.com/webservices/':</p>
          <p class='intro'>C#</p>
          <pre>[WebService(Namespace='http://microsoft.com/webservices/')]
public class MyWebService {
    // implementation
}</pre>
          <p class='intro'>Visual Basic</p>

          <pre>&lt;WebService(Namespace:='http://microsoft.com/webservices/')&gt; Public Class MyWebService
    ' implementation
End Class</pre>

          <p class='intro'>C++</p>
          <pre>[WebService(Namespace='http://microsoft.com/webservices/')]
public ref class MyWebService {
    // implementation
};</pre>
          <p class='intro'>For more details on XML namespaces, see the W3C recommendation on <a href='http://www.w3.org/TR/REC-xml-names/'>Namespaces in XML</A>.</p>

          <p class='intro'>For more details on WSDL, see the <a href='http://www.w3.org/TR/wsdl'>WSDL Specification</a>.</p>
          <p class='intro'>For more details on URIs, see <a href='http://www.ietf.org/rfc/rfc2396.txt'>RFC 2396</a>.</p>
      </span>

      

    
  </body>
</html>

");

			this.DocumentContent = w.ToString();
		}

	
	}
}
