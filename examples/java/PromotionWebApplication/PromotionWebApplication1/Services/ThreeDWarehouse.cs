using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Collections;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace PromotionWebApplication1.Services
{
	class ThreeDWarehouse
	{
		const string Source = "http://sketchup.google.com/3dwarehouse/";

		// http://sketchup.google.com/3dwarehouse/download?mid=39829c375e4ade832fa1d644cdac5f&rtyp=st&ctyp=other
		// http://sketchup.google.com/3dwarehouse/details?mid=74544132a520acd4af304f2ecb647df&ct=hppm

		public readonly ArrayList Items = new ArrayList();

		public ThreeDWarehouse()
		{
			var c = new WebClient();

			var data = c.DownloadString(new Uri(Source));

			data.AtIndecies("/details?mid=",
				p =>
				{
					var mid = data.Substring(p.i + p.target.Length).TakeUntilIfAny("&amp;");

					Items.Add(mid);
				}
			);
		}

		public XElement ToXElement()
		{
			var x = new XElement("mid");

			AddItemsTo(x);

			return x;
		}

		private void AddItemsTo(XElement x)
		{
			foreach (string y in this.Items)
			{
				x.Add(new XElement("value", y));
			}
		}
	}
}
