using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TesctCurrencyWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public Task<Dictionary<string, double>> GetConversionRate()
        {

            //<endpoint address="http://www.webservicex.net/CurrencyConvertor.asmx"
            //    binding="basicHttpBinding" bindingConfiguration="CurrencyConvertorSoap"
            //    contract="CurrencyExchange.CurrencyConvertorSoap" name="CurrencyConvertorSoap" />

            //http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml
            //Daily updated GetConversionRate rates
            var res = 0.00;
            var dict = new Dictionary<string, double>();
            
            string url = @"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            var xDoc = XDocument.Load(url);

            XNamespace ns = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

            var cubes = xDoc.Descendants(ns + "Cube")
                           .Where(x => x.Attribute("currency") != null)
                           .Select(x => new
                           {
                               Currency = (string)x.Attribute("currency"),
                               Rate = (double)x.Attribute("rate")
                           });

            foreach (var result in cubes)
            {
                Console.WriteLine(new { result.Currency, result.Rate });
                dict.Add(result.Currency, result.Rate);
            }


            //CurrencyExchange.CurrencyConvertorSoapClient exchangerate = new CurrencyExchange.CurrencyConvertorSoapClient();
            //double exchangevalue;
            //exchangerate.Open();
            //exchangevalue = exchangerate.ConversionRate(CurrencyExchange.Currency.GBP, CurrencyExchange.Currency.EUR);
            //exchangerate.Close();

            return dict.AsResult();
        }

    }
}
