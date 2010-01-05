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

</wsdl:definitions>
");

			return w.ToString();
		}
	}
}
