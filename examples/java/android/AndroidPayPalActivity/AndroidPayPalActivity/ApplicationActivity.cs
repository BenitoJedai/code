using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidPayPalActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using android.view;
using java.lang;
using com.paypal.android.MECL;

namespace AndroidPayPalActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index
        // inspired by "Y:\opensource\github\SimplePayPalIntegration\src\com\paypal\Paypal.java"

        public static string ReferenceToken;
        public static View MainView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            MainView = ll;

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            //// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            //// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            //// http://developer.android.com/reference/android/webkit/WebView.html



            TextView title = new TextView(this);
            title.setText("JSC Shopping Cart 2");
            ll.addView(title);

            TextView namelabel1 = new TextView(this);
            namelabel1.setText("First Name:");
            ll.addView(namelabel1);


            EditText nameET = new EditText(this);
            nameET.setText("");
            ll.addView(nameET);


            TextView lastnamelabel1 = new TextView(this);
            lastnamelabel1.setText("Last Name:");
            ll.addView(lastnamelabel1);


            EditText lastnameET = new EditText(this);
            lastnameET.setText("");
            ll.addView(lastnameET);


            TextView pkglabel1 = new TextView(this);
            pkglabel1.setText("Select Package:");
            //ll.addView(pkglabel1);

            RadioButton personalRb = new RadioButton(this);
            personalRb.setText("Personal License ($200)");
            //personalRb.AttachTo(ll);

            RadioButton enterpriseRb = new RadioButton(this);
            enterpriseRb.setText("Enterprise License ($400)");
            //enterpriseRb.AttachTo(ll);

            RadioButton commercialRb = new RadioButton(this);
            commercialRb.setText("Commercial License ($600)");
            //commercialRb.AttachTo(ll);

            RadioGroup groupRb = new RadioGroup(this);
            groupRb.addView(personalRb);
            groupRb.addView(enterpriseRb);
            groupRb.addView(commercialRb);
            groupRb.AttachTo(ll);

            Button submitBtn = new Button(this);
            submitBtn.setText("Submit");
            submitBtn.setOnClickListener(new Listener(ll));
            ll.addView(submitBtn);

            this.setContentView(sv);

            this.ShowLongToast("http://jsc-solutions.net");


        }

        	//The reference token that we get from initializing the MECL library
	    public static String _deviceReferenceToken;
	
	    // The PayPal server to be used - can also be ENV_NONE and ENV_LIVE
	    private static int server = PayPal.ENV_SANDBOX;

	    // The ID of your application that you received from PayPal
	    private static String appID = "APP-80W284485P519543T";
	
	    public static string build = "11.01.04.8174";
	
	    //The possible results from initializing MECL
	    protected static int INITIALIZE_SUCCESS = 0;
	    protected static int INITIALIZE_FAILURE = 1;


        public static void  initLibrary(View view)
        {
            View v = view;

            if (v == null)
                v = ApplicationActivity.MainView;

            if (v == null)
            {                
                ConsoleMessage c = new ConsoleMessage("ERROR in initLibrary: MainView is null","ERROR",0,ConsoleMessage.MessageLevel.ERROR);
                return;
            }

            // This is the main initialization call that takes in your Context, the Application ID, the server you would like to connect to, and your PayPalListener
            PayPal.fetchDeviceReferenceTokenWithAppID(v.getContext(), appID, server, new ResultDelegate());



            // -- These are required settings.
            PayPal.getInstance().setLanguage("en_US"); // Sets the language for the library.
            // --
        }


        void onSubmit()
        {

        }


    }

    public class ResultDelegate : PayPalListener
    {

        public void couldNotFetchDeviceReferenceToken()
        {
            //Initialization failed and we didn't get a token
            ApplicationActivity.ReferenceToken = null;
            new MessageBox(null, "ReferenceToken NOT Fetched.");

        }

        public void receivedDeviceReferenceToken(string value)
        {
            //Initialization was successful
            ApplicationActivity.ReferenceToken = value;

            new MessageBox(null, "ReferenceToken received:"+value);
        }

    }


    public class Listener : View.OnClickListener
    {
        View view;
        public Listener(View view)
        {
            this.view = view;
        }

        public void onClick(View value)
        {
            new MessageBox(this.view, "clicked 2!");

            ApplicationActivity.initLibrary(value);
            
        }

       

    }

    public class MessageBox
    {
        public MessageBox(View view, string message)
        {
            View v = view;

            if (v == null)
                v = ApplicationActivity.MainView;

            bool err = (v == null);

            if (!err)
                err = (message == null);

            if(err)
            {
                 ConsoleMessage c = new ConsoleMessage("ERROR in MessageBox: MainView is null", "ERROR", 0, ConsoleMessage.MessageLevel.ERROR);
                 return;
             }

            java.lang.StringBuilder sb = new java.lang.StringBuilder(message);
            Toast.makeText(view.getContext(), sb, Toast.LENGTH_SHORT).show();
        }
    }


}
