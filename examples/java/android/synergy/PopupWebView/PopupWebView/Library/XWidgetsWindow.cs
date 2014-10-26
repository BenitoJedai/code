using android.content;
using android.graphics;
using android.view;
using android.webkit;
using android.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wei.mark.example;
using wei.mark.standout;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Android.Extensions;
using PopupWebView.Library;
using PopupWebView.Library.re;
using android.view.animation;

namespace PopupWebView.Activities
{


    public class XWidgetsWindow : XStandOutWindow
    {
        public override int getAppIcon()
        {
            return android.R.drawable.ic_menu_close_clear_cancel;
        }

        public override string getPersistentNotificationMessage(int value)
        {
            return getAppName();
        }

        public override string getPersistentNotificationTitle(int value)
        {
            return "Close";
        }

        public override string getAppName()
        {
            return "XWidgetWindow";
        }


        public override int getFlags(int id)
        {
            return base.getFlags(id) | XStandOutFlags.FLAG_BODY_MOVE_ENABLE
            | XStandOutFlags.FLAG_WINDOW_FOCUSABLE_DISABLE
            | XStandOutFlags.FLAG_WINDOW_EDGE_LIMITS_ENABLE;
        }
        public override XStandOutWindow.StandOutLayoutParams getParams(int id, XWindow window)
        {
            return new XStandOutWindow.StandOutLayoutParams(this, id,
                400, 250,
            StandOutLayoutParams.CENTER, StandOutLayoutParams.CENTER);
        }

        public override Intent getPersistentNotificationIntent(int id)
        {
            return XStandOutWindow.getCloseIntent(this, GetType().ToClass(), id);

        }



        public const int DATA_CHANGED_TEXT = 0;

        public override void createAndAttachView(int id, FrameLayout frame)
        {
            // http://stackoverflow.com/questions/2761577/android-start-an-intent-into-a-framelayout
            // http://gamma-point.com/content/android-how-have-multiple-activities-under-single-tab-tabactivity#comment-37
            // http://stackoverflow.com/questions/4882776/start-another-activity-inside-the-framelayout-of-tabactivity

            //     Caused by: java.lang.RuntimeException: You must attach your view to the given frame in createAndAttachView()
            //at wei.mark.standout.ui.Window.<init>(Window.java:154)
            //at wei.mark.standout.StandOutWindow.show(StandOutWindow.java:1078)
            //at wei.mark.standout.StandOutWindow.onStartCommand(StandOutWindow.java:381)
            //at android.app.ActivityThread.handleServiceArgs(ActivityThread.java:2656)

            var ll = new LinearLayout(
               frame.getContext()
            );

            //ll.setAlpha(0.8f);

            ll.setOrientation(LinearLayout.VERTICAL);

            #region lltab
            var lltab = new LinearLayout(
              frame.getContext()
            );
            lltab.AttachTo(ll);


            var captionpadding = new TextView(frame.getContext());

            captionpadding.setText("    ");
            // http://stackoverflow.com/questions/3297437/shadow-effect-for-a-text-in-android
            captionpadding.setShadowLayer(1, 0, 0, Color.WHITE);
            captionpadding.setTextColor(Color.WHITE);
            captionpadding.setBackgroundColor(Color.argb(0x7F, 0, 0, 0));

            captionpadding.AttachTo(lltab);

            var caption = new TextView(frame.getContext());

            caption.setText("XWidgetsWindow");
            // http://stackoverflow.com/questions/3297437/shadow-effect-for-a-text-in-android
            caption.setShadowLayer(1, 0, 0, Color.WHITE);
            caption.setTextColor(Color.WHITE);
            caption.setBackgroundColor(Color.argb(0x7F, 0, 0, 0));

            caption.AttachTo(lltab);


            var close = new TextView(frame.getContext());

            close.setText("    x    ");
            // http://stackoverflow.com/questions/3297437/shadow-effect-for-a-text-in-android
            close.setShadowLayer(1, 0, 0, Color.RED);
            close.setTextColor(Color.RED);
            close.setBackgroundColor(Color.argb(0x7F, 0, 0, 0));

            close.AttachTo(lltab);
            close.setClickable(true);

            close.AtClick(
                delegate
                {

                    this.close(id);
                }
            );

            #endregion


            //wei.mark.standout.WindowCache r;

            #region WebView
            var webview = new WebView(frame.getContext());
            //webview.setAlpha(0.8f);

            //frame.startAnimation(new AlphaAnimation(1f, 0.7f));

            //     java.lang.NoSuchMethodError: android.webkit.WebView.setAlpha
            //at PopupWebView.Activities.XWidgetsWindow.createAndAttachView(XWidgetsWindow.java:83)
            //at wei.mark.standout.ui.Window.<init>(Window.java:142)
            //at wei.mark.standout.StandOutWindow.show(StandOutWindow.java:1026)
            //at wei.mark.standout.StandOutWindow.onStartCommand(StandOutWindow.java:381)
            //at android.app.ActivityThread.handleServiceArgs(ActivityThread.java:2043)
            //at android.app.ActivityThread.access$2800(ActivityThread.java:117)
            //at android.app.ActivityThread$H.handleMessage(ActivityThread.java:998)
            //at android.os.Handler.dispatchMessage(Handler.java:99)
            //at android.os.Looper.loop(Looper.java:123)
            //at android.app.ActivityThread.main(ActivityThread.java:3687)
            //at java.lang.reflect.Method.invokeNative(Native Method)
            //at java.lang.reflect.Method.invoke(Method.java:507)
            //at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:842)
            //at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:600)
            //at dalvik.system.NativeStart.main(Native Method)


            //getWindow().setFlags(
            //    WindowManager_LayoutParams.FLAG_HARDWARE_ACCELERATED,
            //    WindowManager_LayoutParams.FLAG_HARDWARE_ACCELERATED);

            //setContentView(webview);

            //webview.getSettings().setSupportZoom(true); 
            //webview.getSettings().setLoadsImagesAutomatically(true);
            webview.getSettings().setJavaScriptEnabled(true);
            //webview.getSettings().setBuiltInZoomControls(true);
            //webview.setInitialScale(1);

            webview.setWebViewClient(new MyWebViewClient
            {
                //__this = this 
            });

            webview.setWebChromeClient(
                new MyWebChromeClient
                {
                    yield_title =
                        // implicit version does not work?
                        value =>
                        {
                            caption.setText(value);

                            PersistentNotifications
                                .Where(k => k.id == id)
                                .WithEach(
                                n =>
                                {
                                    n.contentText = value;

                                    n.Notification.setLatestEventInfo(
                                        n.context,
                                        n.contentTitle,
                                        n.contentText,
                                        n.contentIntent
                                    );
                                    n.update();
                                }
                            );
                        }
                }
            );

            //webview.getSettings().setSupportZoom(true);
            //webview.setScrollBarStyle(WebView.SCROLLBARS_INSIDE_OVERLAY);

            //webview.getSettings().setJavaScriptEnabled(true);

            // no flash in emulator?
            // works on my phone!
            // no Flash since android 4.1.0!!!
            //webview.getSettings().setPluginsEnabled(true);
            //webview.getSettings().setPluginState(android.webkit.WebSettings.PluginState.ON);



            // OR, you can also load from an HTML string:
            //var summary = "<html><body>You scored <b>192</b> points.</body></html>";
            //webview.loadData(summary, "text/html", null);
            //Log.i(TAG, "loadUrl");

            //var uri = "http://cubiq.org/dropbox/3dcity/";
            var uri = "http://abstractatech.com";
            webview.loadUrl(uri);
            #endregion

            // http://forum.xda-developers.com/showthread.php?t=1688531

            var rr = new RelativeLayout(frame.getContext());

            rr.setBackgroundColor(Color.argb(0x1F, 255, 255, 255));

            webview.AttachTo(rr);

            var resizer = new Button(frame.getContext());

            resizer.setText(".:");
            //resizer.setAlpha(0.4f);

            resizer.setWidth(96);
            resizer.setHeight(96);
            resizer.setBackgroundColor(Color.argb(0x7F, 255, 255, 255));



            // http://stackoverflow.com/questions/8397152/androidlayout-alignparentbottom-by-code
            // http://stackoverflow.com/questions/8397152/androidlayout-alignparentbottom-by-code




            resizer.AttachTo(rr);

            RelativeLayout.LayoutParams p = new RelativeLayout.LayoutParams(
               RelativeLayout.LayoutParams.WRAP_CONTENT, RelativeLayout.LayoutParams.WRAP_CONTENT
             );

            //400, 250,
            p.setMargins(400 - 64, 250 - 64 - 32, 0, 0);
            resizer.setLayoutParams(p);



            resizer.setOnTouchListener(
                new __OnTouchListener
                {
                    yield = (view, e) =>
                    {
                        var ww = getWindow(id);

                        // handle dragging to move
                        var consumed = this.onTouchHandleResize(id,
                            ww,
                            view,
                            e
                        );

                        {
                            p.setMargins(ww.getWidth() - 64 + 10, ww.getHeight() - 64 - 20, 0, 0);
                            resizer.setLayoutParams(p);
                        }
                        return consumed;
                    }
                }
            );

            rr.AttachTo(ll);
            ll.AttachTo(frame);

            {
                //Caused by: java.lang.ClassCastException: android.widget.LinearLayout$LayoutParams cannot be cast to wei.mark.standout.StandOutWindow$StandOutLayoutParams
                //at wei.mark.example.XWidgetsWindow.createAndAttachView(XWidgetsWindow.java:101)
                //at wei.mark.standout.ui.Window.<init>(Window.java:150)
                //at wei.mark.standout.StandOutWindow.show(StandOutWindow.java:1078)
                //at wei.mark.standout.StandOutWindow.onStartCommand(StandOutWindow.java:381)
                //at android.app.ActivityThread.handleServiceArgs(ActivityThread.java:2656)

                //p.setMargins(ww.getWidth() - 64, ww.getHeight() - 64 - 32, 0, 0);
                //resizer.setLayoutParams(p);
            }
        }

        class __OnTouchListener : View.OnTouchListener
        {
            public Func<View, MotionEvent, bool> yield;

            public bool onTouch(View arg0, MotionEvent arg1)
            {
                return yield(arg0, arg1);
            }
        }

    }

    class MyWebChromeClient : WebChromeClient
    {
        public Action<string> yield_title = delegate { };

        public override void onReceivedTitle(WebView arg0, string arg1)
        {
            yield_title(arg1);
        }
        //public Action<ValueCallback<Uri>> AtopenFileChooser;

        //public override void openFileChooser(ValueCallback<Uri> uploadMsg)
        //{
        //    AtopenFileChooser(uploadMsg);
        //}
    }

    class MyWebViewClient : WebViewClient
    {
        //public AndroidWebViewActivity __this;

        public override bool shouldOverrideUrlLoading(WebView view, string url)
        {
            //Log.i(TAG, "Processing webview url click...");
            view.loadUrl(url);
            return true;
        }

        //public override void onPageFinished(WebView view, string url)
        //{
        //    //Log.i(TAG, "Finished loading URL: " + url);
        //    if (__this.progressBar.isShowing())
        //    {
        //        __this.progressBar.dismiss();
        //    }
        //}

        //public override void onReceivedError(WebView view, int errorCode, string description, string failingUrl)
        //{
        //    //Log.e(TAG, "Error: " + description);

        //    __this.ShowToast("Oh no! " + description);

        //    //Toast.makeText(__this, "Oh no! " + description, Toast.LENGTH_SHORT).show();
        //    //__this.alertDialog.setTitle((CharSequence)(object)"Error");
        //    //__this.alertDialog.setMessage(description);
        //    //__this.alertDialog.setButton((CharSequence)(object)"OK", new DialogInterface.OnClickListener() {
        //    //    public void onClick(DialogInterface dialog, int which) {
        //    //        return;
        //    //    }
        //    //});
        //    //__this.alertDialog.show();
        //}
    }

}
