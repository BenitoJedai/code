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
using xavalon.net;
using android.media;
using android.content.res;

namespace AndroidLegacyCraftActivity.Activities
{
    public class ApplicationActivity : WebServiceServerActivity
    {
        protected override void onCreate(Bundle savedInstanceState)
        {
            this.ApplicationFile = "index.htm";

            base.onCreate(savedInstanceState);

            //try
            //{
            //    AssetFileDescriptor afd = getAssets().openFd("war-gfx/intro.mid");
            //    var player = new MediaPlayer();
            //    player.setDataSource(afd.getFileDescriptor(), afd.getStartOffset(), afd.getLength());
            //    player.prepare();
            //    player.start();

            //    this.ShowLongToast("music");
            //}
            //catch (System.Exception e)
            //{
            //    this.ShowLongToast("error " + ((object)e).ToString());
            //}
        }
    }


}
