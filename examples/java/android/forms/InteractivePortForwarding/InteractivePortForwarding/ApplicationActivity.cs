using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using ScriptCoreLib.Extensions.Android;
using android.content;
using android.net.wifi;

namespace InteractivePortForwarding.Activities
{
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // unavailable for android 2.4
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
	public class ApplicationActivity : Activity
	{
        // http://www.samsung.com/us/support/owners/product/GT-I9250TSGGEN

		protected override void onCreate(Bundle savedInstanceState)
		{
            ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext = this;


			base.onCreate(savedInstanceState);

            ((WifiManager)this.getSystemService(Context.WIFI_SERVICE)).createWifiLock(
                WifiManager.WIFI_MODE_FULL_HIGH_PERF,
                "InteractivePortForwarding"
                ).acquire();

			var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms);

			var u = new UserControl1();

			u.AttachTo(this);
		}


	}


}


//I/System.Console(25237): { text = 306 UDP > { Length = 103, RemoteEndPoint = 24.77.172.104:35202 } UDP < { replyCounter = 1, Length = 268, RemoteEndPoint = 192.168.43.10:8080 } }
//I/StatusBarPolicy(  207): onDataActivity-D:3
//I/System.Console(25237): { text = 307 UDP > { Length = 103, RemoteEndPoint = 24.77.172.104:35202 } UDP < { replyCounter = 1, Length = 268, RemoteEndPoint = 192.168.43.10:8080 } }
//I/StatusBarPolicy(  207): onSignalStrengthsChanged
//I/System.Console(25237): { text = 308 UDP > { Length = 103, RemoteEndPoint = 24.77.172.104:35202 } UDP < { replyCounter = 1, Length = 268, RemoteEndPoint = 192.168.43.10:8080 } }