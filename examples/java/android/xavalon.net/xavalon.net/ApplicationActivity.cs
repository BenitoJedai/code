using System;
using System.Collections.Generic;
using System.Linq;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using java.lang;
using android.content;
using java.net;
using java.io;
using java.util;
using android.webkit;
using android.util;

namespace xavalon.net.Activities
{

    public class ApplicationActivity : WebServiceServerActivity
    {
        protected override void onCreate(Bundle value)
        {
            this.ApplicationFile = "index.htm";

            base.onCreate(value);

            this.ToNotification("xavalon.net", uri, 0, uri: uri);

        }
    }

  


}
