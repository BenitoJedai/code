using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace ScriptCoreLib.Ultra.WebService
{
	public class InternalWebMethodInfo
	{
		// whats up with const support jsc.meta?

		public static string QueryKey = "WebMethod";

		public string Name;
		public string TypeFullName;

		public string MetadataToken;


		public InternalWebMethodParameterInfo[] Parameters;

		public ArrayList InternalParameters;

		public static void AddParameter(InternalWebMethodInfo that, string Name, string Value)
		{
			if (that.InternalParameters == null)
				that.InternalParameters = new ArrayList();

			var n = new InternalWebMethodParameterInfo
			{
				Name = Name,
				Value = Value
			};

			that.InternalParameters.Add(n);

			that.Parameters = (InternalWebMethodParameterInfo[])that.InternalParameters.ToArray(typeof(InternalWebMethodParameterInfo));
		}

		public InternalWebMethodInfo[] Results;

		public string ToQueryString()
		{
			return "?" + QueryKey + "=" + MetadataToken;
		}

		public static InternalWebMethodInfo First(InternalWebMethodInfo[] e, string MetadataToken)
		{
			var k = default(InternalWebMethodInfo);

			if (!string.IsNullOrEmpty(MetadataToken))
				foreach (var item in e)
				{
					if (item.MetadataToken == MetadataToken)
					{
						k = item;
						break;
					}
				}

			return k;
		}

		public static string GetParameterValue(InternalWebMethodInfo that, string name)
		{
			var r = default(string);

			//Console.WriteLine("GetParameterValue: name: " + name);


			foreach (var item in that.Parameters)
			{
				//Console.WriteLine("GetParameterValue: item.name: " + item.Name);

				if (item.Name == name)
				{
					//Console.WriteLine("GetParameterValue: item.value: " + item.Value);

					r = item.Value;
					break;
				}
			}

			return r;
		}

		public void LoadParameters(HttpContext c)
		{
			foreach (var Parameter in this.Parameters)
			{
				if (Parameter.IsDelegate)
				{
				}
				else
				{
					//WriteFormKeysToConsole(c);

					// do we support null parameters?
					var value = "";

					//Console.WriteLine("LoadParameters: name: " + Parameter.Name);

					var key = "_" + this.MetadataToken + "_" + Parameter.Name;

					//Console.WriteLine("LoadParameters: key: " + key);
					var value_Form = c.Request.Form[key];

					if (null != value_Form)
						value = value_Form;


					//Console.WriteLine("LoadParameters: value: " + value);

					Parameter.Value = value.FromXMLString();
				}
			}
		}

		private static void WriteFormKeysToConsole(HttpContext c)
		{
			foreach (var item in c.Request.Form.AllKeys)
			{
				Console.WriteLine("WriteFormKeysToConsole: existing key: " + item);
			}
		}
	}

}
