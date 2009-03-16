using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.ActionScript.DOM
{
	partial class ExternalContext
	{


		#region ToExternalConverter
		public Converter<T0, T1> ToExternalConverter<T0, T1>(string a0, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + @") { " + code + @" };"
			);

			return (x0) => (T1)f.External(x0);
		}




		public Action<A0> ToExternal<A0>(A0 a0, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + @") { " + code + @" };"
			);

			return (x0) => f.External(x0);
		}

		public Action<A0, A1> ToExternal<A0, A1>(A0 a0, A1 a1, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + ", " + a1 + @") { " + code + @" };"
			);

			return (x0, x1) => f.External(x0, x1);
		}

		public Action<A0, A1, A2> ToExternal<A0, A1, A2>(string a0, string a1, string a2, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + @") { " + code + @" };"
			);

			return (x0, x1, x2) => f.External(x0, x1, x2);
		}

		public Action<A0, A1, A2, A3> ToExternal<A0, A1, A2, A3>(string a0, string a1, string a2, string a3, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + ", " + a3 + @") { " + code + @" };"
			);

			return (x0, x1, x2, x3) => f.External(x0, x1, x2, x3);
		}

		public Converter<A0, A1, A2, A3, R> ToExternalConverter<A0, A1, A2, A3, R>(string a0, string a1, string a2, string a3, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + ", " + a3 + @") { " + code + @" };"
			);

			return (x0, x1, x2, x3) => (R)f.External(x0, x1, x2, x3);
		}

		#endregion


		[Script]
		public delegate T Converter<A, B, C, D, T>(A a, B b, C c, D d);
	}

}
