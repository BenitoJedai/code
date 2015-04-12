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

			var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms);

			var u = new UserControl1();

			u.AttachTo(this);
		}


	}


}

