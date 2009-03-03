using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard
{
	public sealed class AIDAttribute : Attribute
	{
		public ulong Value;

		public AIDAttribute(ulong Value)
		{
			this.Value = Value;
		}

		public class Info
		{
			public readonly List<byte> PackageAIDBytes = new List<byte>();

			public readonly string PackageAID = "";
			public readonly string AppletAID = "";

			public Info(Type Target)
			{
				// 0xA0:0xA1:0xA2:0xA3:0xA4:0x00:0x02
				var PACKAGE_AID = (Target.Assembly.GetCustomAttributes(typeof(AIDAttribute), false) as AIDAttribute[]).FirstOrDefault() ?? new AIDAttribute(0);

				for (int i = 0; i < 7; i++)
				{
					if (i > 0)
						this.PackageAID += ":";

					var value = (byte)((PACKAGE_AID.Value >> (8 * (6 - i))) & 0xff);

					this.PackageAIDBytes.Add(value);

					this.PackageAID += string.Format("0x{0:x2}", value);

				}

				
				// 0xA0:0xA1:0xA2:0xA3:0xA4:0x00:0x02:0x01
				var APPLET_AID = (Target.GetCustomAttributes(typeof(AIDAttribute), false) as AIDAttribute[]).FirstOrDefault() ?? new AIDAttribute(0);


				this.AppletAID = this.PackageAID + ":";

				for (int i = 0; i < 1; i++)
				{
					if (i > 0)
						this.AppletAID += ":";

					this.AppletAID += string.Format("0x{0:x2}", (APPLET_AID.Value >> (8 * (0 - i))) & 0xff);

				}
			}
		}
	}
}
