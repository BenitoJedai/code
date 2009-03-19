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
		

		public Tuple<A0> ToInbound<A0>(Action<A0> h)
		{
			var Token = this.CreateToken();

			Token.External(h);

			return new Tuple<A0> { Method = h, Token = Token };
		}


		public string ToExternal<A0>(Action<A0> h)
		{
			var Token = this.CreateToken();

			Token.External(h);

			return Token;
		}

		public string ToExternal(Action h)
		{
			var Token = this.CreateToken();

			Token.External(h);

			return Token;
		}

		public string ToExternalConverter<T0, T1>(Converter<T0, T1> h)
		{
			var Token = this.CreateToken();

			Token.External(h);

			return Token;
		}

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




		public Action<A0> ToExternal<A0>(string a0, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + @") { " + code + @" };"
			);

			return (x0) => f.External(x0);
		}

		[Script]
		public class Tuple<A0>
		{
			public Action<A0> Method;
			public string Token;
		}

		public Tuple<A0> ToOutbound<A0>(string a0, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] =  function (" + a0 + @") { " + code + @" };"
			);

			return new Tuple<A0> { Method = (x0) => f.External(x0), Token = f };
		}


		public Action<A0, A1> ToExternal<A0, A1>(string a0, string a1, string code)
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

		public Action<A0, A1, A2, A3, A4> ToExternal<A0, A1, A2, A3, A4>(string a0, string a1, string a2, string a3, string a4, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + ", " + a2 + ", " + a3 + ", " + a4 + @") { " + code + @" };"
			);

			return (x0, x1, x2, x3, x4) => f.External(x0, x1, x2, x3, x4);
		}

		public Converter<A0, A1, R> ToExternalConverter<A0, A1, R>(string a0, string a1, string code)
		{
			var f = CreateToken();

			RaiseTrace(f + ": " + code);

			1.ExternalAtDelay(
				"window['" + f + @"'] = function (" + a0 + ", " + a1 + @") { " + code + @" };"
			);

			return (x0, x1) => (R)f.External(x0, x1);
		}

		#endregion


		[Script]
		public delegate void Action<A, B, C, D, E>(A a, B b, C c, D d, E e);


		[Script]
		public delegate T Converter<A, B, C, D, T>(A a, B b, C c, D d);

		[Script]
		public delegate T Converter<A, B, T>(A a, B b);
	}

}
