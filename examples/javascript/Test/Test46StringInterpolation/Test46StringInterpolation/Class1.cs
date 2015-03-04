using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test46StringInterpolation
{
    public class Class1
    {
		// http://gigi.nullneuron.net/gigilabs/c-6-preview-changes-in-vs2015-ctp-5/
		static string name = "Chuck";
		static string surname = "Chuck";

		// this produces nice and clean IL
		static string foo () => $"The man is {name} {surname}";
	}
}
