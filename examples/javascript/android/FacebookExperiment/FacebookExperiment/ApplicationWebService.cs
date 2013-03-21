using android.widget;
using com.facebook.widget;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace FacebookExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {


            if (__crazy_workaround == null)
            {
                Console.WriteLine("__crazy_workaround");
                __crazy_workaround = new __InitializeAndroidActivity();
            }
        }

        static __InitializeAndroidActivity __crazy_workaround;

    }


    class __InitializeAndroidActivity
    {
        static com.facebook.LoginActivity INeedThisActivity;

        static __InitializeAndroidActivity()
        {
            Console.WriteLine("StaticInvoke");

            //  Exception Ljava/lang/RuntimeException; thrown while initializing LTryHideActionbarExperiment/StaticInvoke;
            try
            {


                // https://groups.google.com/forum/?fromgroups=#!topic/android-developers/suLMCWiG0D8
                var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;


                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as ScriptCoreLib.Android.CoreAndroidWebServiceActivity).runOnUiThread(
                    a =>
                    {
                        //var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;



                        // http://stackoverflow.com/questions/4451641/change-android-layout-programatically

                        var sv = new ScrollView(a);
                        var ll = new LinearLayout(a);
                        //ll.setOrientation(LinearLayout.VERTICAL);
                        sv.addView(ll);

                        var b = new Button(a).AttachTo(ll);



                        b.WithText("before AtClick");
                        b.AtClick(
                            v =>
                            {
                                b.setText("AtClick");
                            }
                        );

                        var b2 = new Button(a);
                        b2.setText("The other button!");
                        ll.addView(b2);


                        //Caused by: android.content.res.Resources$NotFoundException: String resource ID #0x7f050002
                        //       at android.content.res.Resources.getText(Resources.java:230)
                        //       at android.content.res.Resources.getString(Resources.java:314)
                        //       at com.facebook.widget.LoginButton.setButtonText(LoginButton.java:532)
                        //       at com.facebook.widget.LoginButton.finishInit(LoginButton.java:472)
                        //       at com.facebook.widget.LoginButton.<init>(LoginButton.java:189)

                        var login = new LoginButton(a);

                        // You have disabled Facebook Login in your app, but you must still specify Package Name or Key Hashes.
                        //login.loginText = "loginText";
                        login.setApplicationId("625051627510580");

                        //                        FATAL EXCEPTION: main
                        //java.lang.NullPointerException: Argument 'applicationId' cannot be null
                        //       at com.facebook.internal.Validate.notNull(Validate.java:29)
                        //       at com.facebook.Session.<init>(Session.java:224)
                        //       at com.facebook.Session.<init>(Session.java:213)
                        //       at com.facebook.Session$Builder.build(Session.java:1454)
                        //       at com.facebook.widget.LoginButton$LoginClickListener.onClick(LoginButton.java:621)


                        //                        FATAL EXCEPTION: main
                        //com.facebook.FacebookException: Cannot use SessionLoginBehavior SSO_WITH_FALLBACK when com.facebook.LoginActivity is not declared as an activity in AndroidManifest.xml
                        //       at com.facebook.Session.validateLoginBehavior(Session.java:992)
                        //       at com.facebook.Session.open(Session.java:915)
                        //       at com.facebook.Session.openForRead(Session.java:385)
                        //       at com.facebook.widget.LoginButton$LoginClickListener.onClick(LoginButton.java:641)

                        //                 Caused by: android.content.res.Resources$NotFoundException: Resource ID #0x7f030001
                        //at android.content.res.Resources.getValue(Resources.java:1014)
                        //at android.content.res.Resources.loadXmlResourceParser(Resources.java:2139)
                        //at android.content.res.Resources.getLayout(Resources.java:853)
                        //at android.view.LayoutInflater.inflate(LayoutInflater.java:394)
                        //at android.view.LayoutInflater.inflate(LayoutInflater.java:352)
                        //at com.android.internal.policy.impl.PhoneWindow.setContentView(PhoneWindow.java:270)
                        //at android.app.Activity.setContentView(Activity.java:1881)
                        //at com.facebook.LoginActivity.onCreate(LoginActivity.java:55)

                        login.setSessionStatusCallback(
                            new XStatusCallback
                            {
                                yield = (arg0, arg1, arg2) =>
                                {
                                    var AccessToken = arg0.getAccessToken();
                                    Console.WriteLine(new { AccessToken, arg0, arg1, arg2 });

                                }
                            }
                        );

                        login.setUserInfoChangedCallback(
                            new XUserInfoChangedCallback
                            {

                                yield = u =>
                                {
                                    var id = u.getId();
                                    var name = u.getName();

                                    Console.WriteLine(new { name, id });

                                    b2.WithText(new { name, id }.ToString());
                                }

                            }
                        );

                        login.AttachTo(ll);

                        a.setContentView(sv);
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }
        }
    }

    //Create Partial Type: com.facebook.widget.LoginButton+LoginButtonCallback
    //Create Partial Type: com.facebook.widget.LoginButton+LoginButtonProperties
    //Create Partial Type: com.facebook.widget.LoginButton+LoginClickListener
    //Create Partial Type: com.facebook.Session+Builder
    //Create Partial Type: com.facebook.Session+AutoPublishAsyncTask
    //error: System.TypeLoadException: Method 'doInBackground' in type 'AutoPublishAsyncTask' from assembly 'FacebookExperiment.ApplicationWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.

    //V:\src\FacebookExperiment\XUserInfoChangedCallback.java:14: com.facebook.Session.AutoPublishAsyncTask has private access in com.facebook.Session
    //public Session.AutoPublishAsyncTask ref1;

    class XUserInfoChangedCallback : com.facebook.widget.LoginButton.UserInfoChangedCallback
    {
        public com.facebook.Session ref0;
        public com.facebook.Session.AutoPublishAsyncTask ref1;

        public Action<com.facebook.model.GraphUser> yield;

        public void onUserInfoFetched(com.facebook.model.GraphUser value)
        {
            yield(value);
        }
    }

    class XStatusCallback : com.facebook.Session.StatusCallback
    {
        public com.facebook.Session ref0;
        public com.facebook.Session.AutoPublishAsyncTask ref1;

        public Action<com.facebook.Session, com.facebook.SessionState, java.lang.Exception> yield;



        public void call(com.facebook.Session arg0, com.facebook.SessionState arg1, java.lang.Exception arg2)
        {
            yield(arg0, arg1, arg2);
        }
    }
}

