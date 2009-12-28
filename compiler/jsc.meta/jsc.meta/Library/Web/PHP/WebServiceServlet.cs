using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace jsc.meta.Library.Web.PHP
{
	public class WebServiceServlet
	{
		public const string WebServiceExtension = ".asmx";

		public string Operation
		{
			get
			{
				var op = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Get["op"];

				return op;
			}
		}

		public string ServiceName
		{
			get
			{
				var c = CommandLine;

				{
					var i = c.IndexOf("?");
					if (i > -1)
						c = c.Substring(0, i);
				}

				{
					var i = c.IndexOf("/");
					if (i > -1)
						c = c.Substring(0, i);
				}

				{
					var i = c.IndexOf(WebServiceExtension);
					if (i > -1)
						return c.Substring(0, i);
				}

				return null;
			}
		}

		public string WebMethod
		{
			get
			{
				var c = CommandLine;
				var j = -1;

				{
					var i = c.IndexOf("/");
					if (i > -1)
						c = c.Substring(0, i);

					j = i;
				}

				{
					var i = c.IndexOf(WebServiceExtension);
					if (i > -1)
						if (j > -1)
							return CommandLine.Substring(j + 1);
				}

				return null;
			}
		}

		// /Service1.asmx/HelloWorld
		public string CommandLine
		{
			get
			{
				// /WebService1/Service1.asmx
				var REQUEST_URI = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.REQUEST_URI];
				// /WebService1/index.php
				var SCRIPT_NAME = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.SCRIPT_NAME];

				var i = SCRIPT_NAME.LastIndexOf("/");
				if (i < 0)
					i = 0;

				var p = REQUEST_URI.Substring(i);

				p = p.Substring(1);

				return p;
			}
		}

		public string GetString(string name)
		{
			var value = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Post[name];

			return value;
		}

		public void SetReturnParameterString(string value)
		{
			// we should be using XElement here
			// we really should escape value for xml...

			// http://xmmssc-www.star.le.ac.uk/SAS/xmmsas_20070308_1802/doc/param/node24.html
			// http://www.hdfgroup.org/HDF5/XML/xml_escape_chars.htm
			var content = value
				.Replace("\"", "&quot;")
				.Replace("'", "&apos;")
				.Replace("<", "&lt;")
				.Replace(">", "&gt;")
				.Replace("&", "&amp;");




			var Content = "<?xml version='1.0' encoding='utf-8'?><string xmlns='http://tempuri.org/'>" + content + "</string>";

			Console.Write(Content);
		}


		public void Invoke()
		{
			// no dice?

			Console.WriteLine("hello world :) one step closer to having ASP.NET web services in php!");
			Console.WriteLine(@"<hr \>");


			// http://localhost:58527/Service1.asmx
			// http://localhost:58527/Service1.asmx?op=HelloWorld
			// http://localhost:58527/Service1.asmx/HelloWorld

			Console.WriteLine("CommandLine: " + CommandLine + "<br />");
			Console.WriteLine("Operation: " + Operation + "<br />");
			Console.WriteLine("ServiceName: " + ServiceName + "<br />");
			Console.WriteLine("WebMethod: " + WebMethod + "<br />");

		}

		public string DocumentContent;

		public void RenderMethodsToDocumentContent(string[] Methods)
		{
			// do we actually need this separate type anymore?

			this.InternalRenderMethodsToDocumentContent(Methods);

			Console.Write(DocumentContent);
		}

		public void InternalRenderMethodsToDocumentContent(string[] Methods)
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


		public void RenderOperationToDocumentContent(SimpleParameterInfo[] Parameters)
		{
			this.InternalRenderOperationToDocumentContent(Parameters);

			Console.Write(DocumentContent);
		}

		public void InternalRenderOperationToDocumentContent(SimpleParameterInfo[] Parameters)
		{

			var w = new StringBuilder();

			w.AppendLine(@"


<html>

    <head><link rel='alternate' type='text/xml' href='/WebService1.asmx?disco' />

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
	" + this.ServiceName + @" Web Service
</title></head>

  <body>

    <div id='content'>

      <p class='heading1'>" + this.ServiceName + @"</p><br>

      

      

      <span>
          <p class='intro'>Click <a href='" + this.ServiceName + @".asmx'>here</a> for a complete list of operations.</p>
          <h2>HelloWorld2</h2>

          <p class='intro'></p>

          <h3>Test</h3>
          
          To test the operation using the HTTP POST protocol, click the 'Invoke' button.



                      <form target='_blank' action='/" + this.ServiceName + @".asmx/" + Operation + @"' method='POST'>                      
                        
                          <table cellspacing='0' cellpadding='4' frame='box' bordercolor='#dcdcdc' rules='none' style='border-collapse: collapse;'>
                          <tr>
	<td class='frmHeader' background='#dcdcdc' style='border-right: 2px solid white;'>Parameter</td>
	<td class='frmHeader' background='#dcdcdc'>Value</td>

</tr>

                        ");


			foreach (var item in Parameters)
			{
				w.AppendLine(@"

                          <tr>
                            <td class='frmText' style='color: #000000; font-weight: normal;'>" + item.Name + @":</td>
                            <td><input class='frmInput' type='text' size='50' name='" + item.Name + @"'></td>
                          </tr>
                        
				");
			}



			w.AppendLine(@"
                        <tr>
                          <td></td>
                          <td align='right'> <input type='submit' value='Invoke' class='button'></td>
                        </tr>
                        </table>
                      

                    </form>
                  <span>
<!--
              <h3>SOAP 1.1</h3>
              <p>The following is a sample SOAP 1.1 request and response.  The <font class=value>placeholders</font> shown need to be replaced with actual values.</p>

              <pre>POST /" + this.ServiceName + @".asmx HTTP/1.1
Host: localhost
Content-Type: text/xml; charset=utf-8
Content-Length: <font class=value>length</font>
SOAPAction: 'http://tempuri.org/HelloWorld2'

&lt;?xml version='1.0' encoding='utf-8'?&gt;

&lt;soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'&gt;
  &lt;soap:Body&gt;
    &lt;HelloWorld2 xmlns='http://tempuri.org/'&gt;
      &lt;a&gt;<font class=value>string</font>&lt;/a&gt;
      &lt;b&gt;<font class=value>string</font>&lt;/b&gt;

    &lt;/HelloWorld2&gt;
  &lt;/soap:Body&gt;
&lt;/soap:Envelope&gt;</pre>

              <pre>HTTP/1.1 200 OK
Content-Type: text/xml; charset=utf-8
Content-Length: <font class=value>length</font>

&lt;?xml version='1.0' encoding='utf-8'?&gt;
&lt;soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'&gt;

  &lt;soap:Body&gt;
    &lt;HelloWorld2Response xmlns='http://tempuri.org/'&gt;
      &lt;HelloWorld2Result&gt;<font class=value>string</font>&lt;/HelloWorld2Result&gt;
    &lt;/HelloWorld2Response&gt;
  &lt;/soap:Body&gt;

&lt;/soap:Envelope&gt;</pre>
          </span>

          <span>
              <h3>SOAP 1.2</h3>
              <p>The following is a sample SOAP 1.2 request and response.  The <font class=value>placeholders</font> shown need to be replaced with actual values.</p>

              <pre>POST /WebService1.asmx HTTP/1.1
Host: localhost
Content-Type: application/soap+xml; charset=utf-8
Content-Length: <font class=value>length</font>

&lt;?xml version='1.0' encoding='utf-8'?&gt;
&lt;soap12:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap12='http://www.w3.org/2003/05/soap-envelope'&gt;
  &lt;soap12:Body&gt;
    &lt;HelloWorld2 xmlns='http://tempuri.org/'&gt;
      &lt;a&gt;<font class=value>string</font>&lt;/a&gt;

      &lt;b&gt;<font class=value>string</font>&lt;/b&gt;
    &lt;/HelloWorld2&gt;
  &lt;/soap12:Body&gt;
&lt;/soap12:Envelope&gt;</pre>

              <pre>HTTP/1.1 200 OK
Content-Type: application/soap+xml; charset=utf-8
Content-Length: <font class=value>length</font>

&lt;?xml version='1.0' encoding='utf-8'?&gt;
&lt;soap12:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap12='http://www.w3.org/2003/05/soap-envelope'&gt;
  &lt;soap12:Body&gt;
    &lt;HelloWorld2Response xmlns='http://tempuri.org/'&gt;
      &lt;HelloWorld2Result&gt;<font class=value>string</font>&lt;/HelloWorld2Result&gt;
    &lt;/HelloWorld2Response&gt;

  &lt;/soap12:Body&gt;
&lt;/soap12:Envelope&gt;</pre>
          </span>
-->
          

          <span>
              <h3>HTTP POST</h3>
              <p>The following is a sample HTTP POST request and response.  The <font class=value>placeholders</font> shown need to be replaced with actual values.</p>

              <pre>POST /" + this.ServiceName + @".asmx/HelloWorld2 HTTP/1.1
Host: localhost
Content-Type: application/x-www-form-urlencoded
Content-Length: <font class=value>length</font>

<font class=key>a</font>=<font class=value>string</font>&amp;<font class=key>b</font>=<font class=value>string</font></pre>

              <pre>HTTP/1.1 200 OK
Content-Type: text/xml; charset=utf-8
Content-Length: <font class=value>length</font>

&lt;?xml version='1.0' encoding='utf-8'?&gt;
&lt;string xmlns='http://tempuri.org/'&gt;<font class=value>string</font>&lt;/string&gt;</pre>
          </span>

      </span>
      

    
    
      

      

    
  </body>
</html>

");

			this.DocumentContent = w.ToString();
		}

	}
}
