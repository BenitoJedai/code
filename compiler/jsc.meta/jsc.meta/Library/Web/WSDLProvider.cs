using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.Web
{
	public class WSDLProvider
	{
		public string ServiceName;

		public string Location;

		public SimpleMethodInfo[] Methods;

		public string ContentType = "application/xml; charset=utf-8";

		public override string ToString()
		{
			var w = new StringBuilder();

			// know what would be really awesome?
			// if we had XElement available in php and java already!

			w.Append(@"
<wsdl:definitions xmlns:soap=""http://schemas.xmlsoap.org/wsdl/soap/"" xmlns:tm=""http://microsoft.com/wsdl/mime/textMatching/"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:mime=""http://schemas.xmlsoap.org/wsdl/mime/"" xmlns:tns=""http://tempuri.org/"" xmlns:s=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://schemas.xmlsoap.org/wsdl/soap12/"" xmlns:http=""http://schemas.xmlsoap.org/wsdl/http/"" targetNamespace=""http://tempuri.org/"" xmlns:wsdl=""http://schemas.xmlsoap.org/wsdl/""> 
  <wsdl:types> 
    <s:schema elementFormDefault=""qualified"" targetNamespace=""http://tempuri.org/""> 
      <s:element name=""string"" nillable=""true"" type=""s:string"" /> 
    </s:schema> 
  </wsdl:types>
");
			#region wsdl:message
			foreach (var Method in this.Methods)
			{

				w.Append(@"
  <wsdl:message name=""" + Method.Name + @"HttpPostIn""> 
");

				foreach (var Parameter in Method.Parameters)
				{
					w.Append(@"
    <wsdl:part name=""" + Parameter.Name + @""" type=""s:string"" /> 
");
				}

				w.Append(@"
  </wsdl:message> 
  <wsdl:message name=""" + Method.Name + @"HttpPostOut""> 
    <wsdl:part name=""Body"" element=""tns:string"" /> 
  </wsdl:message> 
");

			}
			#endregion


			#region wsdl:portType
			w.Append(@"
 <wsdl:portType name=""" + this.ServiceName + @"HttpPost""> 
");
			foreach (var Method in this.Methods)
			{
				#region wsdl:operation
				
				w.Append(@"
    <wsdl:operation name=""" + Method.Name + @"""> 
      <wsdl:documentation xmlns:wsdl=""http://schemas.xmlsoap.org/wsdl/"">Documentation placeholder</wsdl:documentation> 
      <wsdl:input message=""tns:" + Method.Name + @"HttpPostIn"" /> 
      <wsdl:output message=""tns:" + Method.Name + @"HttpPostOut"" /> 
    </wsdl:operation> 
");
				#endregion

			}

			w.Append(@"
  </wsdl:portType> 
");
			#endregion

			// <http:operation location= may need a slash
			// <http:address location= may need full url

			#region wsdl:binding
			w.Append(@"
  <wsdl:binding name=""" + this.ServiceName + @"HttpPost"" type=""tns:" + this.ServiceName + @"HttpPost""> 
    <http:binding verb=""POST"" /> 
");
			foreach (var Method in this.Methods)
			{
				#region wsdl:operation

				w.Append(@"
    <wsdl:operation name=""" + Method.Name + @"""> 
      <http:operation location=""/OrcasMetaWebService1/" + Method.Name + @""" /> 
      <wsdl:input> 
        <mime:content type=""application/x-www-form-urlencoded"" /> 
      </wsdl:input> 
      <wsdl:output> 
        <mime:mimeXml part=""Body"" /> 
      </wsdl:output> 
    </wsdl:operation> 
");
				#endregion

			}

			w.Append(@"
  </wsdl:binding> 
");
			#endregion

			w.Append(@"
  <wsdl:service name=""" + this.ServiceName + @"""> 
    <wsdl:port name=""" + this.ServiceName + @"HttpPost"" binding=""tns:" + this.ServiceName + @"HttpPost""> 
      <http:address location=""http://localhost/OrcasMetaWebService1/" + this.ServiceName + @".asmx"" /> 
    </wsdl:port> 
  </wsdl:service> 	 
");


			w.Append(@"
</wsdl:definitions>
");

			return w.ToString();
		}
	}
}
