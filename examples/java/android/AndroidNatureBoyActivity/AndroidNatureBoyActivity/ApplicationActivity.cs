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
using android.media;

namespace AndroidNatureBoyActivity.Activities
{
    public class ApplicationActivity : xavalon.net.WebServiceServerActivity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        // will be gc'd otherwise
        MediaPlayer mediaPlayer;

        protected override void onCreate(Bundle savedInstanceState)
        {
            this.ApplicationFile = "Class5.htm";
            this.ApplicationScale = 200;


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

                        var n = "zak.mp3";

                        if (width < height)
                            n = "Zak McKracken (Main Titles).mid";

                        var assetFileDescritor = this.getAssets().openFd(n);

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

                        //this.ShowLongToast("music: " + n);
                    }
                    catch //(System.Exception e)
                    {
                        //this.ShowLongToast("error " + ((object)e).ToString());

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

    }


}
