using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Android.Extensions;
using wei.mark.standout;
using android.webkit;
using android.content;
using wei.mark.standout.constants;
using android.graphics;
using android.graphics.drawable.shapes;

namespace wei.mark.example
{
    public class ApplicationActivity :
        //StandOutExampleActivity
    Activity
    {
        // U:\bin\AndroidManifest.xml:5: error: Error: No resource found that matches the given name (at 'label' with value '@string/app_name').

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        /*
Y:\opensource\github\StandOut\library\src\wei\mark\standout\StandOutWindow.java:2112: cannot find symbol
symbol  : class FrameLayout
location: class wei.mark.standout.StandOutWindow
        public class Window extends FrameLayout {         
         
         * Y:\opensource\github\StandOut\library\src\wei\mark\standout\StandOutWindow.java:1137: package R does not exist
                                        R.layout.drop_down_list_item, null);
         * 
         * "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\github\StandOut\library\res -M "Y:\opensource\github\StandOut\library\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J y:\jsc.svn\examples\java\android\StandOutActivity\StandOutActivity\

         * Y:\opensource\github\StandOut\example\src\wei\mark\example\MultiWindow.java:54: package R does not exist
                View view = inflater.inflate(R.layout.body, frame, true);
         * 
         * "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\github\StandOut\example\res -M "Y:\opensource\github\StandOut\example\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J y:\jsc.svn\examples\java\android\StandOutActivity\StandOutActivity\

W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.SimpleWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.MultiWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.WidgetsWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.SimpleWindow (has extras) }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.MultiWindow (has extras) }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.WidgetsWindow (has extras) }: not found
         * 
         * 
E/AndroidRuntime( 2974): Caused by: java.lang.NullPointerException
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow$Window.getSystemDecorations(StandOutWindow.java:2447)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow$Window.<init>(StandOutWindow.java:2181)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow.show(StandOutWindow.java:1416)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow.onStartCommand(StandOutWindow.java:716)
E/AndroidRuntime( 2974):        at android.app.ActivityThread.handleServiceArgs(ActivityThread.java:2490) 
         */

        protected
            //public
            override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);



            StandOutWindow.closeAll(this, typeof(XWidgetsWindow).ToClass());
            StandOutWindow.show(this, typeof(XWidgetsWindow).ToClass(), StandOutWindow.DEFAULT_ID);

            this.finish();
        }


    }

    public class XWidgetsWindow : SimpleWindow
    {
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

            var lltab = new LinearLayout(
              frame.getContext()
            );
            lltab.AttachTo(ll);


            var caption = new TextView(frame.getContext());

            caption.setText(" XWidgetsWindow");
            // http://stackoverflow.com/questions/3297437/shadow-effect-for-a-text-in-android
            caption.setShadowLayer(1, 0, 0, Color.WHITE);
            caption.setTextColor(Color.WHITE);
            caption.setBackgroundColor(Color.BLACK);

            caption.AttachTo(lltab);


            var close = new TextView(frame.getContext());

            close.setText("  x  ");
            // http://stackoverflow.com/questions/3297437/shadow-effect-for-a-text-in-android
            close.setShadowLayer(1, 0, 0, Color.RED);
            close.setTextColor(Color.RED);
            close.setBackgroundColor(Color.BLACK);

            close.AttachTo(lltab);
            close.setClickable(true);

            close.AtClick(
                delegate
                {

                    this.close(id);
                }
            );

            #region WebView
            var webview = new WebView(frame.getContext());
            webview.setAlpha(0.8f);

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


            webview.AttachTo(rr);

            var resizer = new Button(frame.getContext());

            resizer.setText(".:");
            resizer.setAlpha(0.4f);

            resizer.setWidth(72);
            resizer.setHeight(72);


            // http://stackoverflow.com/questions/8397152/androidlayout-alignparentbottom-by-code
            // http://stackoverflow.com/questions/8397152/androidlayout-alignparentbottom-by-code




            resizer.AttachTo(rr);

            RelativeLayout.LayoutParams p = new RelativeLayout.LayoutParams(
               RelativeLayout.LayoutParams.WRAP_CONTENT, RelativeLayout.LayoutParams.WRAP_CONTENT
             );

            p.setMargins(128 + 64, 128 + 64, 0, 0);
            resizer.setLayoutParams(p);



            resizer.setOnTouchListener(
                new __OnTouchListener
                {
                    yield = (view, e) =>
                    {
                        wei.mark.standout.ui.Window ww = getWindow(id);

                        // handle dragging to move
                        var consumed = this.onTouchHandleResize(id,
                            ww,
                            view,
                            e
                        );

                        {
                            p.setMargins(ww.getWidth() - 64 + 8, ww.getHeight() - 64 - 24, 0, 0);
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

        // public abstract StandOutWindow.StandOutLayoutParams getParams(int arg0, wei.mark.standout.ui.Window arg1);

        //        N:\src\wei\mark\example\XWidgetsWindow.java:54: cannot find symbol
        //symbol  : constructor StandOutLayoutParams(wei.mark.example.XWidgetsWindow,int,int,int,int,int)
        //location: class wei.mark.standout.StandOutWindow.StandOutLayoutParams
        //        return  new StandOutWindow.StandOutLayoutParams(this, id, 250, 450, 2147483647, 2147483647);

        //public override StandOutWindow.StandOutLayoutParams getParams(int id, wei.mark.standout.ui.Window window)
        //{
        //    return new StandOutLayoutParams(
        //        // explicit in C#, implicit in java 
        //        this,
        //        id, 250, 450,
        //            StandOutLayoutParams.RIGHT, StandOutLayoutParams.BOTTOM);
        //}

        public override String getAppName()
        {
            return "XWidgetWindow";
        }


        //public override int getThemeStyle()
        //{
        //    return android.R.style.Theme_Light;
        //}

        // move the window by dragging the view
        //public override int getFlags(int id)
        //{

        //    return base.getFlags(id) | StandOutFlags.FLAG_BODY_MOVE_ENABLE
        //            | StandOutFlags.FLAG_WINDOW_FOCUSABLE_DISABLE;
        //}


        public override String getPersistentNotificationMessage(int id)
        {
            return "Click to close the XWidgetWindow";
        }

        public override Intent getPersistentNotificationIntent(int id)
        {
            return StandOutWindow.getCloseIntent(this, typeof(SimpleWindow).ToClass(), id);
        }

        public override int getAppIcon()
        {
            return android.R.drawable.star_on;
        }
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
