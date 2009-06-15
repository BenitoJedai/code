using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLibJavaCard
{
	public sealed class AIDAttribute : Attribute
	{
		public byte[] Bytes;


		public AIDAttribute(params byte[] Bytes)
		{
			this.Bytes = Bytes;
		}



		public class Info
		{
			public readonly List<byte> PackageAIDBytes = new List<byte>();
			public readonly List<byte> AppletAIDBytes = new List<byte>();

			public readonly string PackageAID = "";
			public readonly string AppletAID = "";

			public Info(Type Target)
			{
				// 0xA0:0xA1:0xA2:0xA3:0xA4:0x00:0x02
				var PACKAGE_AID = (Target.Assembly.GetCustomAttributes(typeof(AIDAttribute), false) as AIDAttribute[]).FirstOrDefault() ?? new AIDAttribute(0);

				for (int i = 0; i < PACKAGE_AID.Bytes.Length; i++)
				{
					if (i > 0)
						this.PackageAID += ":";

					var value = PACKAGE_AID.Bytes[i];

					this.PackageAIDBytes.Add(value);

					this.PackageAID += string.Format("0x{0:x2}", value);

				}

				
				// 0xA0:0xA1:0xA2:0xA3:0xA4:0x00:0x02:0x01
				var APPLET_AID = (Target.GetCustomAttributes(typeof(AIDAttribute), false) as AIDAttribute[]).FirstOrDefault() ?? new AIDAttribute(0);


				this.AppletAID = this.PackageAID + ":";

				for (int i = 0; i < APPLET_AID.Bytes.Length; i++)
				{
					if (i > 0)
						this.AppletAID += ":";

					var value = APPLET_AID.Bytes[i];

					this.AppletAIDBytes.Add(value);
					this.AppletAID += string.Format("0x{0:x2}", value);

				}
			}

			public byte[] ToArray()
			{
				return this.PackageAIDBytes.Concat(this.AppletAIDBytes).ToArray();
			}
		}
	}
}
