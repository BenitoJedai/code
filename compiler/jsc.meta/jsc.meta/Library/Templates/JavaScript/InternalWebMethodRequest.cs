﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Library.Templates.JavaScript
{
	public abstract class InternalWebMethodRequest
	{
		public string MetadataToken;

		public string Data;

		public InternalWebMethodRequest()
		{
			// look, a ctor!
		}

		public static void AddParameter(InternalWebMethodRequest that, string name, string value)
		{
			if (null == value)
				return;

			if (string.IsNullOrEmpty(that.Data))
			{
				that.Data = "";
			}
			else
			{
				that.Data += "&";
			}

			// http://stackoverflow.com/questions/81991/a-potentially-dangerous-request-form-value-was-detected-from-the-client
			var __value = value.ToXMLString();

			that.Data += "_" + that.MetadataToken + "_" + name + "=" + Native.Window.escape(__value);
		}

		public static void Invoke(InternalWebMethodRequest that)
		{
			var x = new IXMLHttpRequest();

			// we are using
			// "xml" as path
			// "WebMethod" as method selector
			// we could hide those details into post.

			var Target = "/xml?WebMethod=" + that.MetadataToken;

			x.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, Target);
			x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

			x.send(that.Data);

			x.InvokeOnComplete(that.Complete, 50);

		}

		public void Complete(IXMLHttpRequest r)
		{
			var xml = r.responseXML;

			foreach (var item in xml.documentElement.childNodes)
			{
				//Debugger.Break();

				//Native.Window.alert("callback: " + item.nodeName);

				InvokeCallback(item.nodeName,
					x =>
					{
						//Native.Window.alert("parameter: " + x);

						var u = default(string);

						foreach (var p in item.childNodes)
						{
							if (p.nodeName == x)
							{
								u = p.text;
								break;
							}
						}

						return u;
					}
				);

				//new IHTMLDiv { innerText = "callback: " + item.nodeName }.AttachToDocument();

				//foreach (var p in item.childNodes)
				//{
				//    new IHTMLDiv { innerText = "parameter: " + p.nodeName + " = " + p.text }.AttachToDocument();

				//}
			}
		}

		public delegate string ParameterLookup(string parameter);

		public virtual void InvokeCallback(string name, ParameterLookup lookup)
		{
			throw new Exception("InvokeCallback");
		}
	}

}
