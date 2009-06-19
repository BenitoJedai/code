using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public abstract class Applet
	{
		/// <summary>
		///  This method is used by the applet to register this applet instance with the Java Card runtime environment and to assign the Java Card platform name of the applet as its instance AID bytes.
		/// </summary>
		protected void register()
		{
		}

		/// <summary>
		/// This member does not exist in javacard API but helps to emulate selectingApplet behavior
		/// </summary>
		public APDU apdu;

		public abstract void process(APDU apdu);


		/// <summary>
		/// This method is used by the applet process() method to distinguish the SELECT APDU command which selected this applet, from all other other SELECT APDU commands which may relate to file or internal applet state selection.
		/// </summary>
		/// <returns></returns>
		protected bool selectingApplet()
		{
			var a = new AIDAttribute.Info(this.GetType()).ToSelectApplet();
			var x = true;
			var y = this.apdu.getBuffer();

			for (int i = 0; i < a.Length; i++)
			{
				if (y[i] != (sbyte)a[i])
				{
					x = false;
					break;
				}
			}

			return x;
		}

		

	}
}
