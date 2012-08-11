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
        // will be gc'd otherwise
        MediaPlayer mediaPlayer;

        protected override void onCreate(Bundle savedInstanceState)
        {
            this.ApplicationFile = "index.htm";



            this.onpagefinished =
                delegate
                {
                    try
                    {
                        // http://www.aganazzar.com/midi.html
                        // http://www.lastown.com/public/musique/warcraft/list.html
                        // http://gamemusic.wordpress.com/2007/12/09/warcraft-1-orcs-and-humans/



                        if (mediaPlayer != null)
                            mediaPlayer.stop();

                        mediaPlayer = new MediaPlayer();

                        var n = "Warcraft1_TitleTheme.mid";

                        if (width > height)
                            n = "war-gfx/intro.mid";

                        var  assetFileDescritor = this.getAssets().openFd(n);

                        mediaPlayer.reset();

                        mediaPlayer.setDataSource(

                            //"IntroWarII.mp3");

                            assetFileDescritor.getFileDescriptor(),
                            assetFileDescritor.getStartOffset(),
                            assetFileDescritor.getLength()
                            );

                        //add this ? ---------------------------------------
                        //close the descriptor
                        assetFileDescritor.close();
                        //add this ? ---------------------------------------


                        //mediaPlayer.prepare();
                        mediaPlayer.setOnPreparedListener(
                            new _prepared { }
                        );
                        //mediaPlayer.setOnCompletionListener(
                        //    new _OnCompletionListener());

                        //mediaPlayer.setOnErrorListener(
                        //    new _OnErrorListener());


                        mediaPlayer.prepare();
                        mediaPlayer.setLooping(true);

                        this.ShowLongToast("music: " + n);
                    }
                    catch (System.Exception e)
                    {
                        this.ShowLongToast("error " + ((object)e).ToString());

                        //throw;
                    }
                };

            base.onCreate(savedInstanceState);

        }

        class _prepared : MediaPlayer.OnPreparedListener
        {
            public void onPrepared(MediaPlayer value)
            {
                value.start();
            }
        }

        protected override void onPause()
        {
            if (mediaPlayer != null)
                mediaPlayer.stop();

            base.onPause();
        }

        protected override void onResume()
        {
            if (mediaPlayer != null)
                mediaPlayer.start();

            base.onResume();
        }
        //class _OnCompletionListener : MediaPlayer.OnCompletionListener
        //{

        //    public void onCompletion(MediaPlayer value)
        //    {
        //        Log.wtf("jsc", "_OnCompletionListener");
        //    }
        //}

        //class _OnErrorListener : MediaPlayer.OnErrorListener
        //{


        //    public bool onError(MediaPlayer arg0, int arg1, int arg2)
        //    {
        //        Log.wtf("jsc", "_OnErrorListener");
        //        return true;
        //    }
        //}
    }


    [Script(
   HasNoPrototype = true,
  Implements = typeof(global::System.Exception),
  ImplementationType = typeof(java.lang.Throwable))]
    internal class __Exception
    {
        public __Exception() { }
        public __Exception(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }
}

