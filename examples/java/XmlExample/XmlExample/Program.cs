using System.Threading;
using System;

using ScriptCoreLib;
using System.IO;
using org.w3c.dom;


namespace XmlExample
{
	[Script]
	public class Program
	{


		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			// doubleclicking on the jar will not show the console

			var text = File.ReadAllText("XMLFile1.xml");
 
			Console.WriteLine(text);

			var doc = text.ToDocument();

			if (doc == null)
			{
				Console.WriteLine("doc is null");
				return;
			}

			var items =  doc.getElementsByTagName("data");

			for (int i = 0; i < items.getLength(); i++)
			{
				var item = items.item(i);

				if (item is Element)
				{
					var element = (Element)item;

					

					Console.WriteLine("element: " + element.getFirstChild().getNodeValue());
				}
				else
				{
					Console.WriteLine("node");

				}
			}
		}
	}
}
