using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.DOM
{
	/// <summary>
	/// http://www.devguru.com/Technologies/xmldom/quickref/obj_httpRequest.html
	/// </summary>
	[Script(InternalConstructor = true)]
	public class IXMLHttpRequest //: ISink
	{
		// This type was extracted from ScriptCoreLib.Net
		
		#region constructors
		[Script(HasNoPrototype = true, ExternalTarget = "XMLHttpRequest")]
		private class InternalXMLHttpRequest { }

		public IXMLHttpRequest() { }
		public IXMLHttpRequest(HTTPMethodEnum method, string url, bool @async) { }
		public IXMLHttpRequest(string url, Action<IXMLHttpRequest> handler) { }
		public IXMLHttpRequest(string url, Action<IXMLHttpRequest> handler, bool @async) { }
		public IXMLHttpRequest(string url, string data, Action<IXMLHttpRequest> handler) { }
		public IXMLHttpRequest(string url, string data, Action<IXMLHttpRequest> handler, bool @async) { }
		public IXMLHttpRequest(string url, IXMLDocument data, Action<IXMLHttpRequest> handler) { }
		public IXMLHttpRequest(string url, IXMLDocument data, Action<IXMLHttpRequest> handler, bool @async) { }

		/// <summary>
		/// creates object, opens connection
		/// </summary>
		/// <param name="method"></param>
		/// <param name="url"></param>
		/// <param name="handler"></param>
		public IXMLHttpRequest(HTTPMethodEnum method, string url, Action<IXMLHttpRequest> handler) { }

		private static IXMLHttpRequest InternalConstructor()
		{
			IXMLHttpRequest n = null;

			try
			{
				n = (IXMLHttpRequest)((object)new InternalXMLHttpRequest());
			}
			catch
			{
				n = (IXMLHttpRequest)((object)new IActiveX("Msxml2.XMLHTTP.3.0", "Microsoft.XMLHTTP"));
			}


			return n;
		}


		private static IXMLHttpRequest InternalConstructor(HTTPMethodEnum method, string url, bool @async)
		{
			IXMLHttpRequest n = InternalConstructor();

			n.open(method, url, @async);

			return n;
		}

		private static IXMLHttpRequest InternalConstructor(string url, string data, Action<IXMLHttpRequest> handler)
		{
			return InternalConstructor(url, data, handler, true);
		}

		private static IXMLHttpRequest InternalConstructor(string url, string data, Action<IXMLHttpRequest> handler, bool @async)
		{
			IXMLHttpRequest req = new IXMLHttpRequest(HTTPMethodEnum.POST, url, @async);

			req.send(data);

			req.InvokeOnComplete(handler, @async);

			return req;
		}

		private static IXMLHttpRequest InternalConstructor(string url, Action<IXMLHttpRequest> handler)
		{
			return InternalConstructor(url, handler, true);
		}

		/// <summary>
		/// sends no data, with head method
		/// </summary>
		/// <param name="url"></param>
		/// <param name="handler"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		private static IXMLHttpRequest InternalConstructor(string url, Action<IXMLHttpRequest> handler, bool @async)
		{
			IXMLHttpRequest req = InternalConstructor(HTTPMethodEnum.HEAD, url, @async);

			req.send();

			req.InvokeOnComplete(handler, @async);

			return req;

		}

		private static IXMLHttpRequest InternalConstructor(string url, IXMLDocument data, Action<IXMLHttpRequest> handler)
		{
			return InternalConstructor(url, data, handler, true);
		}

		/// <summary>
		/// sends data with POST method
		/// </summary>
		/// <param name="url"></param>
		/// <param name="data"></param>
		/// <param name="handler"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		private static IXMLHttpRequest InternalConstructor(string url, IXMLDocument data, Action<IXMLHttpRequest> handler, bool @async)
		{
			IXMLHttpRequest req = new IXMLHttpRequest(HTTPMethodEnum.POST, url, @async);

			req.send(data);

			req.InvokeOnComplete(handler, @async);

			return req;
		}

		/// <summary>
		/// sends http request, with no data
		/// </summary>
		/// <param name="method"></param>
		/// <param name="url"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		private static IXMLHttpRequest InternalConstructor(HTTPMethodEnum method, string url, Action<IXMLHttpRequest> handler)
		{
			IXMLHttpRequest req = new IXMLHttpRequest(method, url, true);

			req.send();

			req.InvokeOnComplete(handler);

			return req;
		}

		#endregion


		// http://jibbering.com/2002/4/httprequest.html
		// http://www.xulplanet.com/references/objref/XMLHttpRequest.html#method_getAllResponseHeaders




		public string ETag
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return getResponseHeader("ETag");
			}
		}

		public void open(HTTPMethodEnum method, string url)
		{
		}

		public void open(HTTPMethodEnum method, string url, bool @async)
		{
		}

		public void open(HTTPMethodEnum method, string url, bool @async, string name, string pass)
		{
		}

		public void setRequestHeader(string key, string value)
		{
		}

		public string getResponseHeader(string header)
		{
			return default(string);
		}

		public string getAllResponseHeaders()
		{
			return default(string);
		}

		public int BytesIn
		{
			[Script(DefineAsStatic = true)]
			get
			{
				if (readyState > 2)
				{
					return responseText.Length;
				}

				return 0;
			}
		}

		[Script(DefineAsStatic = true)]
		public void send()
		{
			send<object>(null);
		}

		// we probably could get away with object
		public void send<T>(T data)
		{
		}

		public void abort()
		{
		}


		public bool complete
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return this.readyState == 4;
			}
		}

		[Script(DefineAsStatic = true)]
		public void InvokeOnComplete(Action<IXMLHttpRequest> e, bool @async)
		{
			if (e == null)
				return;

			if (@async)
				InvokeOnComplete(e);
			else
			{
				e(this);
			}
		}

		public const int DefaultInterval = 500;

		[Script(DefineAsStatic = true)]
		public void InvokeOnComplete(Action<IXMLHttpRequest> e)
		{
			InvokeOnComplete(e, DefaultInterval);
		}

		[Script(DefineAsStatic = true)]
		public void InvokeOnComplete(Action<IXMLHttpRequest> e, int interval)
		{
			if (e == null)
				return;

			Timer t = new Timer();

			t.Tick +=
				 delegate
				 {
					 // fixme: logging via switch.


					 //string dbg = "#" + t.Counter;

					 //try { dbg += " state " + this.readyState; }
					 //catch { }


					 if (this.complete)
					 {
						 //Console.Log("ready " + dbg);


						 t.Stop();
						 e(this);
					 }
					 else
					 {
						 //Console.Log("not ready " + dbg);
					 }
				 };

			t.StartInterval(interval);
		}

		public readonly int readyState;
		public readonly string responseText;

		/// <summary>
		/// parses the xml
		/// 
		/// Content-type: application/xhtml+xml
		/// -OR-
		/// Content-type: text/xml
		/// &lt;?xml version='1.0' encoding='utf-8' standalone='yes'?&gt;
		/// </summary>
		public IXMLDocument responseXML
		{
			[Script(DefineAsStatic = true)]
			get
			{
				// note: IE in no-Addon mode will fail
				return IXMLDocument.Parse(responseText);
			}
		}

		public static implicit operator IXMLDocument(IXMLHttpRequest m)
		{
			return m.responseXML;
		}

		/// <summary>
		/// http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.1.1
		/// </summary>
		public enum HTTPStatusCodes
		{
			ServiceUnavailable = 503,
			NotFound = 404,
			OK = 200,
			NoContent = 204,
			/// <summary>
			/// 202 - ok to read xml
			/// </summary>
			Accepted = 202
		}

		public bool IsOK
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return status == HTTPStatusCodes.OK;
			}
		}

		public bool IsNoContent
		{
			[Script(DefineAsStatic = true)]
			get
			{
				int s = (int)status;

				if (s == 204) return true;
				// IE may return 1223 instead of 204
				if (s == 1223) return true;

				return false;
			}
		}

		public bool IsOffline
		{
			[Script(DefineAsStatic = true)]
			get
			{
				int s = (int)status;

				if (s == 0) return true;
				if (s == 12029) return true;

				return false;
			}
		}

		public readonly HTTPStatusCodes status;
		public readonly string statusText;







		[Script(DefineAsStatic = true)]
		public TType ToJSON<TType>()
		{
			return Expando.FromJSONProtocolString(responseText).To<TType>();
		}
	}

}
